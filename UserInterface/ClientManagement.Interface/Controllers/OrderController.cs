using ClientManagement.BusinessLogic;
using ClientManagement.DataObject;
using ClientManagement.DomainModel;
using ClientManagement.DomainModel.DashBoard;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class OrderController : BaseController<OrderLogic>
    {
        public ActionResult EmailAllAttachment(int OrderId)
        {
            PaymentInfoReportModel model = new PaymentInfoReportModel();
            BusinessObject.SendMail();
            return PartialView("PaymentInfoReportIndex", model);
        }

        [HttpPost]
        public ActionResult GenerateExpenceVSPaymentReport(PaymentInfoReportModel model)
        {
            if (model.StartDate.Year == 00001 || model.EndDate.Year == 00001)
            {
                model.StartDate = DateTime.Now;
                model.EndDate = DateTime.Now;
            }
            var modeldata = BusinessObject.GetBudgetInfoReportDetails(model);
            return PartialView("PaymentInfoReportIndex", modeldata);
        }
        public ActionResult PaymentInfoReportIndex()
        {
            PaymentInfoReportModel model = new PaymentInfoReportModel();
            return PartialView("PaymentInfoReportIndex", model);
        }

        [HttpPost]
        public ActionResult GeneratePaymentInfoReport(PaymentInfoReportModel model)
        {
            if (model.StartDate.Year == 00001 || model.EndDate.Year == 00001)
            {
                model.StartDate = DateTime.Now;
                model.EndDate = DateTime.Now;
            }
            var modeldata = BusinessObject.GetPaymentInfoReportDetails(model);
            return PartialView("PaymentInfoReportIndex", modeldata);
        }
        public ActionResult ManageIndex()
        {
            return View("Index", BusinessObject.GetOrderDetails(false));
        }
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetOrderDetails(true));
        }

        public ActionResult CheckoutOrder(int OrderId, bool flag)
        {
            BusinessObject.UpdateStatus(OrderId, flag);
            //GetBusinessObject<ClientLogic>().SendOrderSMS(OrderId);
            return RedirectToAction("Index");
        }
        public ActionResult AddFarmOrderItem(int OrderItemsId)
        {
            ClientOrderItemsModel model = new ClientOrderItemsModel();

            model = BusinessObject.GetClientOrderItemsDetails(OrderItemsId);
            return PartialView("OrderFarmItems", model);
        }

        public class OrderSlectedModel
        {
            public int OrderId { get; set; }
            public int FarmerId { get; set; }
            public int ProductInfoId { get; set; }
            public int Quantity { get; set; }
        }

        [HttpPost]
        public JsonResult SaveOrderSelected(string orderList)
        {
            IList<OrderSlectedModel> orderItems = new JavaScriptSerializer().Deserialize<IList<OrderSlectedModel>>(orderList);

            foreach (var order in orderItems)
            {
                //BusinessObject.SaveClientOrderItems(order.OrderId, order.FarmerId, order.Quantity, order.ProductInfoId);
            }
            return Json(new { success = true });
        }

        [HttpPost]
        public JsonResult ProcessInfoSelected(int OrderId, string[] ProductInfoIds)
        {
            foreach (var item in ProductInfoIds)
            {
                BusinessObject.SaveOrderItems(OrderId, Convert.ToInt32(item));
            }
            return Json(new { success = true });
        }

        public ActionResult CreateOrder()
        {
            return View("OrderCapture", BusinessObject.OrderDetails());
        }

        [HttpGet]
        public ActionResult Refresh(int OrderId)
        {
            OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
            OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(OrderId);
            OrderProductInfoFilter.ProductList = BusinessObject.GetProductList();
            return PartialView("OrderIndex2", OrderProductInfoFilter);
        }

        public ActionResult AddOrderItem(int OrderId, int ProductInfoId)
        {
            BusinessObject.SaveOrderItems(OrderId, ProductInfoId);
            OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
            OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(OrderId);
            OrderProductInfoFilter.ProductList = BusinessObject.GetProductList();
            return PartialView("OrderIndex", OrderProductInfoFilter);
        }

        [HttpPost]
        public ActionResult AddQuantity(OrderProductInfoFilterModel model, int OrderItemsId, int OrderId)
        {

            BusinessObject.UpdateQuantity(model);

            OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
            OrderProductInfoFilter.ProductList = BusinessObject.GetProductList();
            OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(model.OrderId);
            return PartialView("OrderIndex", OrderProductInfoFilter);
        }

        [HttpPost]
        public ActionResult Finalize(OrderProductInfoFilterModel model, int OrderItemsId, int OrderId)
        {
            BusinessObject.Finalize(OrderItemsId, OrderId, model.Quantity);

            OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
            OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(OrderId);
            OrderProductInfoFilter.ProductList = BusinessObject.GetProductList();
            return PartialView("OrderIndex", OrderProductInfoFilter);
        }

        public ActionResult GerOrderItemsGraph(int OrderId)
        {
            return Json(BusinessObject.DynamicOrderDetails(OrderId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult CreateEditFarmerOrder(string submitbtn, int FarmerId)
        {
            OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
            OrderProductInfoFilter = BusinessObject.GetOrderFarmerProductInfoDetails(FarmerId);
            OrderProductInfoFilter.ProductList = BusinessObject.GetProductList();
            OrderProductInfoFilter.IsView = true;
            return PartialView("OrderIndex", OrderProductInfoFilter);
        }

        public JsonResult InsertItems(List<OrderProductInfoFilterModel> InformationList)
        {
            int OrderId = InformationList[0].OrderId;
            if (InformationList != null)
            {
                BusinessObject.RemoveOrderItems(InformationList[0].OrderId);
                foreach (var item in InformationList)
                {
                    BusinessObject.SaveOrderItemName(item.OrderId, item.Product, (decimal)item.Quantity);
                }
                BusinessObject.SaveOrderTotal(OrderId);
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddStockItem(int OrderId)
        {
            var model = BusinessObject.GetProducts();
            model.OrderId = OrderId;
            return PartialView("IndexStock", model);
        }

        [HttpPost]
        public ActionResult CreateEditOrder(string submitbtn, int OrderId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    OrderModel modelfilter = new OrderModel();
                    modelfilter = BusinessObject.GetOrderDetailsData(OrderId);
                    return PartialView("OrderCapture", modelfilter);

                case "Checkout Order":
                    return RedirectToAction("CheckoutOrder", new { OrderId = OrderId, flag = true });

                case "Add Items":
                    OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
                    OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(OrderId);
                    OrderProductInfoFilter.ProductList = BusinessObject.GetProductList();
                    OrderProductInfoFilter.IsView = false;
                    return PartialView("OrderIndex", OrderProductInfoFilter);
                case "View Items":
                    OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(OrderId);
                    OrderProductInfoFilter.IsView = true;
                    return PartialView("OrderIndex", OrderProductInfoFilter);
                case "Generate PDF":
                    MemoryStream workStream = BusinessObject.CreatePDF(OrderId);
                    byte[] byteInfo = workStream.ToArray();
                    workStream.Write(byteInfo, 0, byteInfo.Length);
                    workStream.Position = 0;
                    Response.Buffer = true;
                    Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("Invoice_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".pdf"));
                    Response.ContentType = "APPLICATION/pdf";
                    Response.BinaryWrite(byteInfo);
                    return new FileStreamResult(workStream, "application/pdf");
                case "Mark as Active":
                    BusinessObject.UpdateStatus(OrderId, false);
                    return RedirectToAction("Index");
                case "Remove":
                    BusinessObject.DeleteOrder(OrderId);
                    return RedirectToAction("Index");
                case "Send Email":
                    BusinessObject.SendSingleMail(OrderId);
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult DeleteOrderItems(int OrderId, int OrderItemsId)
        {
            BusinessObject.DeleteOrderItem(OrderItemsId); 
            OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
            OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(OrderId);
            OrderProductInfoFilter.ProductList = BusinessObject.GetProductList();
            return PartialView("OrderIndex", OrderProductInfoFilter);
        }

        public ActionResult GenerateInvoice(int OrderId)
        {
            MemoryStream workStream = BusinessObject.CreatePDF(OrderId);
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("Invoice_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".pdf"));
            Response.ContentType = "APPLICATION/pdf";
            Response.BinaryWrite(byteInfo);
            return new FileStreamResult(workStream, "application/pdf");
        }

        [HttpPost]
        public ActionResult Delete(string submitbtn, int OrderProductInfoId, int OrderId)
        {
            BusinessObject.DeleteOrderProductInfo(OrderProductInfoId);
            OrderProductInfoFilterModel OrderProductInfoFilter = new OrderProductInfoFilterModel();
            OrderProductInfoFilter = BusinessObject.GetOrderProductInfoDetails(OrderId);
            return PartialView("OrderIndex", OrderProductInfoFilter);
        }

        [HttpPost]
        public ActionResult CreatePayment(ClientPaymentModel clientPaymentModel)
        {
            OrderPaymentsModel model = new OrderPaymentsModel();
            model.Amount = clientPaymentModel.Amount;
            model.OrderId = clientPaymentModel.OrderId;
            BusinessObject.CreateOrderPayments(model);
            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult CreateEditOrderData(OrderModel Order)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.SaveOrder(Order);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("OrderCapture", Order);
            }
        }

        public ActionResult DeleteOrderPayment(int OrderPaymentsId)
        {
            BusinessObject.DeleteOrderPayments(OrderPaymentsId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ProcessLogistics(int OrderId, string[] ProductInfoIds)
        {
            foreach (var item in ProductInfoIds)
            {
                BusinessObject.SaveOrderItems(OrderId, Convert.ToInt32(item));
            }
            return Json(new { success = true });
        }


        public ActionResult CapturePaymentInformation(int OrderId)
        {
            ClientPaymentModel mod = new ClientPaymentModel();
            var ordermodel = BusinessObject.GetOrderInformationbyOrderId(OrderId);
            mod.Amount = ordermodel.Amount;
            mod.OrderId = OrderId;
            mod.PaymentTypeList = ordermodel.PaymentTypeList;
            mod.OrderPaymentsList = BusinessObject.GetOrderPaymentsData(OrderId);
            return PartialView("OrderPayment", mod);
        }

    }
}

