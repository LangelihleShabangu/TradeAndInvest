using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System;
using System.Linq;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace ClientManagement.Interface.Controllers
{
    public class InventoryController : BaseController<ReportLogic>
    {
        public ActionResult Index()
        {
            SalesModel mod = new SalesModel();
            //mod = BusinessObject.GetSalesInformationDetails();
            mod.DynamicReport = BusinessObject.DynamicReportDetails().ToArray();
            mod.Flag = false;
            return View("Index", mod);
        }

        public ActionResult DynamicReportDetails(int ProductsId, DateTime StartDate, DateTime Enddate)
        {
            if (Enddate.Year == 0001 || StartDate.Year == 0001 || ProductsId == 0)
                return Json(BusinessObject.DynamicReportDetails(), JsonRequestBehavior.AllowGet);           
            else
                return Json(BusinessObject.DynamicReportDetailsFilter(ProductsId, StartDate, Enddate), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DynamicReportDetailsInvoice(int ProductsId, DateTime StartDate, DateTime Enddate)
        {
            if (Enddate.Year == 0001 || StartDate.Year == 0001 || ProductsId == 0)
                return Json(BusinessObject.DynamicReportDetailsInvoice(), JsonRequestBehavior.AllowGet);
            else
                return Json(BusinessObject.DynamicReportDetailsFilter(ProductsId, StartDate, Enddate), JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateInventory(int WeekNumber, int Actual)
        {
            return View("InventoryCapture", new InventoryModel() { WeekNumber = WeekNumber, Actual = Actual });
        }

        //public ActionResult InventoryEstimationSave(InventoryModel Filter)
        //{            
        //    SalesModel mod = new SalesModel();
        //    var model = new VicocaApplication.BusinessLogic.SalesLogic(CurrentUser.ConnectionString);
        //    var ProductInformation = new VicocaApplication.BusinessLogic.ReportLogic(CurrentUser.ConnectionString).ProductDetailsList(Filter.ProductsId);
        //    foreach(var item in ProductInformation)
        //    {
        //        if (item.TotalQuantity < Filter.Quantity)
        //        {   
        //            //ModelState.AddModelError(string.Empty,string.Format("Product : {0} with value of {1} less than {2} ", Filter.Products, item.TotalQuantity, Filter.Quantity));                    
        //        }
        //    }

        //    model.InventoryEstimationSave(Filter.Quantity, Filter.ProductsId); 
        //    mod.ProductsId = Filter.ProductsId;
        //    mod = model.GetSalesInformationDetails();
        //    mod.DynamicReport = new VicocaApplication.BusinessLogic.ReportLogic(CurrentUser.ConnectionString).DynamicReportDetailsFilter(Filter);
        //    mod.ProductsId = Filter.ProductsId;
        //    mod.Products = model.GetProduct(mod.ProductsId);
        //    mod.ProductDetailsList = new VicocaApplication.BusinessLogic.ReportLogic(CurrentUser.ConnectionString).ProductDetailsList(Filter.ProductsId);
        //    mod.StartDate = Filter.StartDate;
        //    mod.Enddate = Filter.Enddate;
        //    mod.Quantity = Filter.Quantity;
        //    mod.Flag = true;
        //    return View("Index", mod);
        //}        

        //public ActionResult InventorySearch(InventoryModel Filter)
        //{
        //    SalesModel mod = new SalesModel();
        //    var model = new VicocaApplication.BusinessLogic.SalesLogic(CurrentUser.ConnectionString);
        //    mod.ProductsId = Filter.ProductsId;
        //    mod = model.GetSalesInformationDetails();
        //    mod.DynamicReport = new VicocaApplication.BusinessLogic.ReportLogic(CurrentUser.ConnectionString).DynamicReportDetailsFilter(Filter);
        //    mod.ProductsId = Filter.ProductsId;
        //    mod.Products = model.GetProduct(mod.ProductsId);
        //    mod.StartDate = Filter.StartDate;
        //    mod.Enddate = Filter.Enddate;
        //    mod.ProductDetailsList = new VicocaApplication.BusinessLogic.ReportLogic(CurrentUser.ConnectionString).ProductDetailsList(Filter.ProductsId);
        //    mod.Flag = true;
        //    return View("Index", mod);
        //}

        //[HttpPost]
        //public ActionResult CreateEditInventory(InventoryModel inventory)
        //{
        //    var Adminclient = new SalesLogic(CurrentUser.ConnectionString);
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            Adminclient.SaveInventory(inventory);
        //            return RedirectToAction("Index");
        //        }
        //        else
        //        {
        //            //var mod = Adminclient.CreateClientModelData();
        //            //client.BusinessTypeList = mod.BusinessTypeList;
        //            //client.AreaList = mod.AreaList;
        //            return RedirectToAction("Index");
        //        }
        //    }
        //}
    }
}