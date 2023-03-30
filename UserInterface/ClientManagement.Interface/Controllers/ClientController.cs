using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using ClientManagement.Interface.Helper;
using Models;
using Models.Information;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class ClientController : BaseController<ClientLogic>
    {
        public ActionResult refresh(int ClientId)
        {
            return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientId));
        }
        public ActionResult CaptureInvoiceInformation(int ClientId)
        {
            OrderModel model = new OrderModel();           
            model = BusinessObject.GetDataListInvoice();
            model.ClientId = ClientId;
            return PartialView("OrderCapture", model);
        }

        public ActionResult CreateEditOrderData(OrderModel model)
        {
            BusinessObject.SaveOrder(model);
            return RedirectToAction("Index", "Order");
        }

        public ActionResult refreshGeneral(int ClientId)
        {
            return PartialView("ClientDetailsGeneral", BusinessObject.GetClientAccountRegistrationList(ClientId));
        }

        public ActionResult CapturePaymentInformation(int ClientAccountRegistrationId)
        {
            ClientPaymentModel mod = new ClientPaymentModel();
            var model = BusinessObject.GetClientPayment(ClientAccountRegistrationId);
            mod = model;
            mod.ClientAccountRegistrationId = ClientAccountRegistrationId;
            return PartialView("ClientPayment", model);
        }

        public ActionResult UploadDocuments(int ClientAccountRegistrationId)
        {
            int ClientidData = BusinessObject.GetClientId(ClientAccountRegistrationId);
            ClientDocumentModel clientDocument = new ClientDocumentModel();
            clientDocument.ClientId = ClientidData;
            clientDocument.ClientAccountRegistrationId = ClientAccountRegistrationId;
            clientDocument.DocumentTypeList = BusinessObject.GetDocumentTypeList();
            clientDocument.ClientDocumentList = BusinessObject.GetClientDocumentList(ClientidData, ClientAccountRegistrationId);
            return PartialView("ClientDocumentIndex", clientDocument);
        }
        public ActionResult DeleteClientDocument(int ClientDocumentId)
        {
            BusinessObject.DeleteClientDocument(ClientDocumentId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteClientPayment(int ClientPaymentId)
        {
            BusinessObject.DeleteClientPayment(ClientPaymentId);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditClientAccountRegistrations(string submitbtn, int ClientAccountRegistrationId)
        {
            int ClientidData = BusinessObject.GetClientId(ClientAccountRegistrationId);
            ClientDocumentModel clientDocument = new ClientDocumentModel();
            switch (submitbtn)
            {
                case "Upload":
                    clientDocument.ClientId = ClientidData;
                    clientDocument.ClientAccountRegistrationId = ClientAccountRegistrationId;
                    clientDocument.DocumentTypeList = BusinessObject.GetDocumentTypeList();
                    clientDocument.ClientDocumentList = BusinessObject.GetClientDocumentList(ClientidData, ClientAccountRegistrationId);
                    return PartialView("ClientDocumentUploadGeneral", clientDocument);
                case "Upload Documents":
                    clientDocument.ClientId = ClientidData;
                    clientDocument.ClientAccountRegistrationId = ClientAccountRegistrationId;
                    clientDocument.DocumentTypeList = BusinessObject.GetDocumentTypeList();
                    clientDocument.ClientDocumentList = BusinessObject.GetClientDocumentList(ClientidData, ClientAccountRegistrationId);
                    return PartialView("ClientDocumentUpload", clientDocument);
                case "Download Document":
                    clientDocument.ClientId = ClientidData;
                    clientDocument.ClientAccountRegistrationId = ClientAccountRegistrationId;
                    BusinessObject.DeleteClientAccountRegistration(ClientAccountRegistrationId);
                    return PartialView("ClientDocumentIndex", BusinessObject.GetClientAccountRegistrationList(ClientidData));
                case "Download":
                    clientDocument.ClientId = ClientidData;
                    clientDocument.ClientAccountRegistrationId = ClientAccountRegistrationId;
                    BusinessObject.DeleteClientAccountRegistration(ClientAccountRegistrationId);
                    return PartialView("ClientDocumentIndex", BusinessObject.GetClientAccountRegistrationList(ClientidData));
                case "Payment Schedule":
                    return DownloadPaymentSchedule(ClientAccountRegistrationId);
                case "DownloadDocument":
                    MemoryStream workStream = new MemoryStream();
                    byte[] byteInfo = null; //BusinessObject.GetClientDocumentFile(ClientAccountRegistrationId);
                    workStream.Write(byteInfo, 0, byteInfo.Length);
                    workStream.Position = 0;
                    Response.Buffer = true;
                    Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("PaymentSchedule_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".pdf"));
                    Response.ContentType = "APPLICATION/pdf";
                    Response.BinaryWrite(byteInfo);
                    return new FileStreamResult(workStream, "application/pdf");
                case "Remove":
                    BusinessObject.DeleteClientAccountRegistration(ClientAccountRegistrationId);
                    return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientidData));
            } 
            return View();
        }
        public ActionResult DownloadDocumentData(int ClientAccountRegistrationId)
        {
            int ClientidData = BusinessObject.GetClientId(ClientAccountRegistrationId);
            MemoryStream workStream = BusinessObject.GeneratePaymentPDFFile(ClientAccountRegistrationId);
            if (workStream != null)
            {
                byte[] byteInfo = workStream.ToArray();
                workStream.Write(byteInfo, 0, byteInfo.Length);
                workStream.Position = 0;
                Response.Buffer = true;
                Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("Payment_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".pdf"));
                Response.ContentType = "APPLICATION/pdf";
                Response.BinaryWrite(byteInfo);
                return new FileStreamResult(workStream, "application/pdf");
            }
            else
            {
                return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientidData));
            }
        }
        public ActionResult DownloadDocument(int ClientAccountRegistrationId)
        {
            MemoryStream workStream = new MemoryStream();
            byte[] byteInfo = null; //BusinessObject.GetClientDocumentFile(ClientAccountRegistrationId);
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("PaymentSchedule_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".pdf"));
            Response.ContentType = "APPLICATION/pdf";
            Response.BinaryWrite(byteInfo);
            return new FileStreamResult(workStream, "application/pdf");
        }

        [HttpPost]
        public ActionResult EditClientAccountRegistration(string submitbtn, int ClientAccountRegistrationId)
        {
            int ClientidData = BusinessObject.GetClientId(ClientAccountRegistrationId);
            ClientDocumentModel clientDocument = new ClientDocumentModel();
            switch (submitbtn)
            {
                case "Upload Documents":
                    clientDocument.ClientId = ClientidData;
                    clientDocument.ClientAccountRegistrationId = ClientAccountRegistrationId;
                    clientDocument.DocumentTypeList = BusinessObject.GetDocumentTypeList();
                    clientDocument.ClientDocumentList = BusinessObject.GetClientDocumentList(ClientidData, ClientAccountRegistrationId);
                    return PartialView("ClientDocumentUpload", clientDocument);
                case "Download Document":
                    clientDocument.ClientId = ClientidData;
                    clientDocument.ClientAccountRegistrationId = ClientAccountRegistrationId;
                    BusinessObject.DeleteClientAccountRegistration(ClientAccountRegistrationId);
                    return PartialView("ClientDocumentIndex", BusinessObject.GetClientAccountRegistrationList(ClientidData));
                case "DownloadDocumentData":
                    MemoryStream workStream = BusinessObject.CreateClientPaymentsPDF(ClientAccountRegistrationId);
                    if (workStream != null)
                    {
                        byte[] byteInfo = workStream.ToArray();
                        workStream.Write(byteInfo, 0, byteInfo.Length);
                        workStream.Position = 0;
                        Response.Buffer = true;
                        Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("Payment_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".pdf"));
                        Response.ContentType = "APPLICATION/pdf";
                        Response.BinaryWrite(byteInfo);
                        return new FileStreamResult(workStream, "application/pdf");
                    }
                    else
                    {
                        return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientidData));
                    }
                case "Payment Schedule":
                    return DownloadPaymentSchedule(ClientAccountRegistrationId);
                case "Remove":
                    BusinessObject.DeleteClientAccountRegistration(ClientAccountRegistrationId);
                    return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientidData));
            }
            return View();
        }
        public ActionResult EditRegisterInformation(int ClientAccountRegistrationId)
        {
            return PartialView("ClientRegistrationCapture", BusinessObject.GetClientAccountData(ClientAccountRegistrationId));
        }

        public ActionResult EditRegisterInformationGeneral(int ClientAccountRegistrationId)
        {
            return PartialView("ClientRegistrationCaptureGeneral", BusinessObject.GetClientAccountData(ClientAccountRegistrationId));
        }
        public ActionResult CaptureRegisterInformation(int ClientId)
        {
            ClientAccountRegistrationModel model = new ClientAccountRegistrationModel();
            model.ClientId = ClientId;
            model.AccountNumber = BusinessObject.GetCountAccountRegistration();
            return PartialView("ClientRegistrationCapture", model);
        }

        public ActionResult CaptureRegisterInformationGeneral(int ClientId)
        {
            ClientAccountRegistrationModel model = new ClientAccountRegistrationModel();
            model.ClientId = ClientId;
            model.AccountNumber = BusinessObject.GetCountAccountRegistration();
            return PartialView("ClientRegistrationCaptureGeneral", model);
        }       

        public ActionResult UploadFilesGeneral(ClientDocumentModel model)
        {

            if (Request.Files != null && Request.Files.Count > 0)
            {
                var files = Request.Files[0];
                if (files != null && files.ContentLength > 0)
                {
                    var content = new byte[files.ContentLength];
                    files.InputStream.Read(content, 0, files.ContentLength);
                    model.CurrentDocument = content;
                }
            }
            BusinessObject.SaveClientDocument(model);
            int ClientidData = BusinessObject.GetClientId(model.ClientAccountRegistrationId);
            model.ClientId = ClientidData;
            model.DocumentTypeList = BusinessObject.GetDocumentTypeList();
            model.ClientDocumentList = BusinessObject.GetClientDocumentList(ClientidData, model.ClientAccountRegistrationId);
            return PartialView("ClientDocumentUploadGeneral", model);
        }

        public ActionResult UploadFiles(ClientDocumentModel model)
        {

            if (Request.Files != null && Request.Files.Count > 0)
            {
                var files = Request.Files[0];
                if (files != null && files.ContentLength > 0)
                {
                    var content = new byte[files.ContentLength];
                    files.InputStream.Read(content, 0, files.ContentLength);
                    model.CurrentDocument = content;
                }
            }
            BusinessObject.SaveClientDocument(model);
            int ClientidData = BusinessObject.GetClientId(model.ClientAccountRegistrationId);
            model.ClientId = ClientidData;
            model.DocumentTypeList = BusinessObject.GetDocumentTypeList();
            model.ClientDocumentList = BusinessObject.GetClientDocumentList(ClientidData, model.ClientAccountRegistrationId);
            return PartialView("ClientDocumentUpload", model);
        }
        [HttpPost]
        public ActionResult CreateClientAccountRegistrationGeneral(ClientAccountRegistrationModel model)
        {
            model.ClientId = BusinessObject.SaveClientAccountRegistration(model);
            return PartialView("ClientDetailsGeneral", BusinessObject.GetClientAccountRegistrationList(model.ClientId));
        }
        [HttpPost]
        public ActionResult CreateClientAccountRegistration(ClientAccountRegistrationModel model)
        {
            model.ClientId = BusinessObject.SaveClientAccountRegistration(model);
            return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(model.ClientId));
        }

        [HttpGet]
        public ActionResult DownloadPayment(int ClientAccountRegistrationId)
        {
            var DocumentResults = new DetailModel();
            ClientAccountRegistrationModel model = BusinessObject.GetClientAccountData(ClientAccountRegistrationId);

            ReportData modelData = new ReportData();
            modelData = DocumentResults.CreateDocument(model.InitiationFee, model.Insurance, model.Rate, model.Amount, model.Instalment, model.PaymentDate, model.Name, model.IdNumber, model.Passport, model.Terms);
            BusinessObject.DeletePaymentProposal(ClientAccountRegistrationId, model.Amount);

            foreach (var item in modelData.PaymentProposalList)
            {
                item.ClientAccountRegistrationId = ClientAccountRegistrationId;
                item.ClientId = model.ClientId;
                item.PaymentDate = model.PaymentDate;
                item.Instalment = model.Instalment;                
                BusinessObject.SaveClientPaymentMonthly(item);
            }

            rptDocument.rptExcel Excel = new rptDocument.rptExcel(modelData.XMLData);
            string fileName = "PaymentSchedule_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".xlsx";
            return File(Excel.GetEPFile(), "Text/xml", fileName);
        }

        public ActionResult DownloadPaymentSchedule(int ClientAccountRegistrationId)
        {
            var DocumentResults = new DetailModel();
            ClientAccountRegistrationModel model = BusinessObject.GetClientAccountData(ClientAccountRegistrationId);

            ReportData modelData = new ReportData();
            modelData = DocumentResults.CreateDocument(model.InitiationFee, model.Insurance, model.Rate, model.Amount, model.Instalment, model.PaymentDate, model.Name, model.IdNumber, model.Passport, model.Terms);
            BusinessObject.DeletePaymentProposal(ClientAccountRegistrationId, model.Amount);
            foreach (var item in modelData.PaymentProposalList)
            {
                item.ClientAccountRegistrationId = ClientAccountRegistrationId;
                item.ClientId = model.ClientId;
                item.Instalment = model.Instalment;
                item.PaymentDate = model.PaymentDate;
                BusinessObject.SaveClientPaymentMonthly(item);
            }

            rptDocument.rptExcel Excel = new rptDocument.rptExcel(modelData.XMLData);
            string fileName = "PaymentSchedule_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".xlsx";
            return File(Excel.GetEPFile(), "Text/xml", fileName);
        }

        public ActionResult Index()
        {
            var models = new ClientModel();
            return View(BusinessObject.GetClientDetails());
        }
        public ActionResult SendAllInformationalSMS()
        {
            BusinessObject.SendAllClientSMS();
            return PartialView("Index", BusinessObject.GetClientDetails());
        }

        public ActionResult ClientIndex(int ClientId)
        {
            return PartialView("ClientMembersIndex", BusinessObject.GetClientModelDataInfo(ClientId));
        }
        [HttpPost]
        public ActionResult Edit(string submitbtn, int ClientId)
        {
            switch (submitbtn)
            {
                case "Send Informational SMS":
                    BusinessObject.SendSingleSMS(ClientId);
                    return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientId));
                case "Make Payments":
                    return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientId));
                case "Client Payments":
                    return PartialView("ClientDetailsGeneral", BusinessObject.GetClientAccountRegistrationList(ClientId));
                case "Client Details":
                    return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientId));
                case "Client Information":
                    return PartialView("ClientDetailsGeneral", BusinessObject.GetClientAccountRegistrationList(ClientId));
                case "Refresh":
                    return PartialView("ClientDetails", BusinessObject.GetClientAccountRegistrationList(ClientId));
                case "RefreshGeneral":
                    return PartialView("ClientDetailsGeneral", BusinessObject.GetClientAccountRegistrationList(ClientId));
                case "Edit":
                    return PartialView("Client", BusinessObject.GetClientModelDataInfo(ClientId));
                case "Remove":
                    BusinessObject.DeleteClient(ClientId);
                    return RedirectToAction("Index", "Client");
            }
            return View();
        }

        public ActionResult DownloadDocumentData1(int ClientAccountRegistrationId)
        {
            MemoryStream workStream = BusinessObject.CreateClientPaymentsPDF(ClientAccountRegistrationId);
            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;
            Response.Buffer = true;
            Response.AddHeader("Content-Disposition", "attachment; filename= " + Server.HtmlEncode("PaymentSchedule_" + System.DateTime.Now.ToString("dd MMMM yyyy") + ".pdf"));
            Response.ContentType = "APPLICATION/pdf";
            Response.BinaryWrite(byteInfo);
            return new FileStreamResult(workStream, "application/pdf");
        }

        public ActionResult DownloadDocuments(int ClientDocumentId)
        {
            MemoryStream workStream = new MemoryStream();
            ClientDocumentModel model = new ClientDocumentModel();
            model = BusinessObject.GetClientDocumentFile(ClientDocumentId);
            byte[] byteInfo = model.CurrentDocument;
            return File(
                byteInfo, System.Net.Mime.MediaTypeNames.Application.Pdf, model.Name);
        }

        public ActionResult CreateClient()
        {
            return View("Client", BusinessObject.CreateClientModelData());
        }

        [HttpPost]
        public ActionResult GetClientDetailsInfo(ClientModel client)
        {
            return PartialView("Index", BusinessObject.GetClientDetailsFilter(client));
        }

        [HttpPost]
        public ActionResult CreatePayment(ClientPaymentModel ClientPayment)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.SaveClientPayment(ClientPayment);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult CreateClient(ClientModel client)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.SaveClient(client);
                return RedirectToAction("Index");
            }
            else
            {
                var mod = BusinessObject.CreateClientModelData();
                client = mod;
                client.ClientId = mod.ClientId;
                client.AreaList = mod.AreaList;
                return PartialView("Client", client);
            }
        }
    }
}
