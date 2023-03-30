using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class TransporterController : BaseController<Administration>
    {
        public class OrderSlectedModel
        {
            public int OrderId { get; set; }
            public int TransporterId { get; set; }
            public int ProductInfoId { get; set; }
            public string Quantity { get; set; }
            public int OrderItemsId { get; set; }
        }

        [HttpPost]
        public JsonResult SaveDeliverySelected(string orderList)
        {
            IList<OrderSlectedModel> orderItems = new JavaScriptSerializer().Deserialize<IList<OrderSlectedModel>>(orderList);

            foreach (var item in orderItems)
            {                
                BusinessObject.SaveDeliveryInfo(item.OrderId, item.ProductInfoId, item.OrderItemsId,  item.Quantity, item.TransporterId);
            }
            return Json(new { success = true });
        }

        public ActionResult AddLogisticsItem(int OrderId)
        {
            TransporterModel model = new TransporterModel();
            model = BusinessObject.GetTransporterDetails(OrderId);
            model.OrderId = OrderId;

            return PartialView("OrderLogistics", model);
        }

        public ActionResult Refresh(int TransporterId)
        {
            return Json(BusinessObject.GetTransporterVehicleData(TransporterId), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            var models = new TransporterModel();
            return View(BusinessObject.GetTransporterDetails());
        }

        public ActionResult TransporterIndex(int TransporterId)
        {
            return PartialView("TransporterIndex", BusinessObject.GetTransporterModelDataInfo(TransporterId));
        }

        [HttpPost]
        public ActionResult Edit(string submitbtn, int OrderId, int TransporterId = 0)
        {
            switch (submitbtn)
            {
                case "Edit":
                    return PartialView("Transporter", BusinessObject.GetTransporterModelDataInfo(TransporterId));
                case "Transport Details":
                    return PartialView("TransporterIndex", BusinessObject.GetTransporterVehicleData(TransporterId));
                case "Ready for Client Delivery":
                    BusinessObject.UpdateStatus(TransporterId, OrderId);
                    return PartialView("TransporterIndex", BusinessObject.GetTransporterVehicleData(TransporterId));
                case "Generate Invoice PDF":
                    MemoryStream workStream = BusinessObject.CreateLogisticsPDF(TransporterId, OrderId, 0);
                    byte[] byteInfo = workStream.ToArray();
                    workStream.Write(byteInfo, 0, byteInfo.Length);
                    workStream.Position = 0;
                    Response.Buffer = true;
                    Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("LogisticInvoice" + ".pdf"));
                    Response.ContentType = "APPLICATION/pdf";
                    Response.BinaryWrite(byteInfo);
                    return new FileStreamResult(workStream, "application/pdf");
                case "Remove":
                    BusinessObject.DeleteTransporter(TransporterId);
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult TransporterInvoice(TransporterModel model, int OrderId, int TransporterId = 0)
        {
            BusinessObject.UpdateAmount(OrderId, TransporterId, model.Amount);
            MemoryStream workStream = BusinessObject.CreateLogisticsPDF(TransporterId, OrderId, model.Amount);
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("LogisticInvoice" + ".pdf"));
            Response.ContentType = "APPLICATION/pdf";
            Response.BinaryWrite(byteInfo);
            return new FileStreamResult(workStream, "application/pdf");
        }

        public ActionResult VehicleCaptureInformation(int TransporterId)
        {
            VehicleModel model = new VehicleModel();
            model.TransporterId = TransporterId;
            return PartialView("TransporterVehicleCapture", model);
        }

        public ActionResult VehicleCapture(int TransporterId)
        {
            VehicleModel model = new VehicleModel();

            model = new ClientManagement.BusinessLogic.VehicleLogic(CurrentUser.ConnectionString).CreateVehicleModelDataInfo(TransporterId);

            model.TransporterId = TransporterId;
            return PartialView("TransporterVehicleCapture", model);
        }

        public ActionResult CreateTransporter()
        {
            return View("Transporter", BusinessObject.CreateTransporterModelData());
        }

        [HttpPost]
        public ActionResult GetTransporterDetailsInfo(TransporterModel Transporter)
        {
            return PartialView("Index", BusinessObject.GetTransporterDetails());
        }

        [HttpPost]
        public ActionResult CreateVehicleData(VehicleModel vehicle)
        {
            new ClientManagement.BusinessLogic.VehicleLogic(CurrentUser.ConnectionString).SaveVehicle(vehicle);
            return PartialView("TransporterIndex", BusinessObject.GetTransporterVehicleData(vehicle.TransporterId));
        }

        [HttpPost]
        public ActionResult CreateTransporter(TransporterModel Transporter)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.SaveTransporter(Transporter);
                return RedirectToAction("Index");
            }
            else
            {
                var mod = BusinessObject.CreateTransporterModelData();
                Transporter = mod;
                Transporter.TransporterId = mod.TransporterId;
                Transporter.AreaList = mod.AreaList;
                return PartialView("Transporter", Transporter);
            }

        }
    }
}
