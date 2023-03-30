using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using ClientManagement.Interface.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    [Authorize]
    public class BudgetController : BaseController<AdminLogic>
    {
        public ActionResult ExpenceVSPaymentReport()
        {
            PaymentInfoReportModel model = new PaymentInfoReportModel();
            return PartialView("ExpenceVSPaymentReport", model);
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
            return PartialView("ExpenceVSPaymentReport", modeldata);
        }
        public ActionResult Index(BudgetModel model)
        {
            return View(BusinessObject.GetBudgetDetailInfo());
        }

        public ActionResult CreateBudget()
        {
            return View("ExpenceCapture", BusinessObject.GetBudgetDetail());
        }

        [HttpPost]
        public ActionResult CreateEditBudget(string submitbtn, int BudgetId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    BudgetModel modelfilter = new BudgetModel();
                    modelfilter = BusinessObject.GetBudgetDetail(BudgetId);
                    return PartialView("ExpenceCapture", modelfilter);
                case "Remove":
                    BusinessObject.DeleteBudget(BudgetId);
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateBudgetData(BudgetModel Budget)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.CreateBudget(Budget);
                return RedirectToAction("Index");
            }
            else
                return PartialView("ExpenceCapture", Budget);
        }
    }
}
