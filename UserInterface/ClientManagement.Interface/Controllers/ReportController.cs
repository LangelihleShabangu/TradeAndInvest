using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace ClientManagement.Interface.Controllers
{
    public class ReportController : BaseController<ReportLogic> 
    {        
        public ActionResult ClientAccountRegistrationIndex()
        {
            return PartialView("LoanReportIndex", BusinessObject.GetClientAccountRegistrationList());
        }

        public ActionResult MaterialReportIndex()
        {
            MaterialInfoReportModel model = new MaterialInfoReportModel();
            return View(model);
        }

        public ActionResult PaymentReportIndex()
        {
            PaymentInfoReportModel model = new PaymentInfoReportModel();                      
            return View(model);
        }

        public ActionResult PaymentInfoReportIndex()
        {
            PaymentInfoReportModel model = new PaymentInfoReportModel();
            return PartialView("PaymentInfoReportIndex", BusinessObject.CreatePaymentReport());
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

        [HttpPost]
        public ActionResult GenerateMaterialReport(MaterialInfoReportModel model)
        {
            var modeldata = BusinessObject.GetMaterialReportDetails(model);
            return PartialView("MaterialReportIndex", modeldata);
        }

        [HttpPost]
        public ActionResult GeneratePaymentReport(PaymentInfoReportModel model)
        {
            if (model.StartDate.Year == 00001 || model.EndDate.Year == 00001)
            {
                model.StartDate = DateTime.Now;
                model.EndDate = DateTime.Now;
            }

            var modeldata = BusinessObject.GetPaymentReportDetails(model);
            return PartialView("PaymentReportIndex", modeldata);
        }

        [HttpPost]
        public ActionResult GenerateReport(ReportModel model)
        {
            List<string> bts = new List<string>();
            string output = string.Empty;
            int i = 1;

            var parameters = new object[2] { model.StartDate, model.EndDate };
            string docXML = string.Empty;

            Dictionary<string, object> parameter = new Dictionary<string, object>();
            foreach (object item in parameters)
            {
                parameter.Add(i.ToString(), item.ToString());
                i++;
            }

            string executeData = "exec usp_Report_GetPayments " + "{0}" + " , " + "{0}";

            StringBuilder xmlStrings = new StringBuilder();
            xmlStrings.Append(executeData);

            xmlStrings = BusinessObject.ReportContextInfo("usp_Report_GetPayments", parameter);

            return new DownloadHelper()
            {
                FileName = "Report.xls",
                FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                //todo:Brian File = new ACRApplication.Admin.Interface.Helpers.rptReport(xmlStrings.ToString()).GetFile(DateTime.Now)
            };
        }

        [HttpPost]
        public ActionResult GenerateSalesReport(PaymentInfoReportModel model, string submitbtn)
        {
            if (submitbtn == "Generate Excel Report")
            {
                List<string> bts = new List<string>();
                string output = string.Empty;
                int i = 1;

                if (model.StartDate.Year == 00001 || model.EndDate.Year == 00001)
                {
                    model.StartDate = DateTime.Now;
                    model.EndDate = DateTime.Now;
                }

                var parameters = new object[2] { model.StartDate, model.EndDate };
                string docXML = string.Empty;

                Dictionary<string, object> parameter = new Dictionary<string, object>();
                foreach (object item in parameters)
                {
                    parameter.Add(i.ToString(), item.ToString());
                    i++;
                }

                string executeData = "exec usp_Report_GetPayments " + "{0}" + " , " + "{0}";

                StringBuilder xmlStrings = new StringBuilder();
                xmlStrings.Append(executeData);

                xmlStrings = BusinessObject.ReportContextInfo("usp_Report_GetPayments", parameter);

                return new DownloadHelper()
                {
                    FileName = "Report.xls",
                    FileType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                   //todo:Brian File = new ACRApplication.Admin.Interface.Helpers.rptSalesReport(xmlStrings.ToString()).GetFile(DateTime.Now)
                };
            }
            else
            {
                var modeldata = BusinessObject.GetPaymentReportDetails(model);
                return PartialView("PaymentReportIndex", modeldata);
            }
        }

        public ActionResult AuditBookingReportIndex()
        {
            PaymentInfoReportModel model = new PaymentInfoReportModel();
            return View(model);
        }      

        [HttpPost]
        public ActionResult GetBookingAuditReportDetails(PaymentInfoReportModel model)
        {
            var modeldata = BusinessObject.GetBookingAuditReportDetails(model);
            return PartialView("AuditBookingReportIndex", modeldata);
        }
    }
}
