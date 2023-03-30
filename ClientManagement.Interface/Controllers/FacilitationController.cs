using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class FacilitationController : BaseController<AdminLogic>
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetFacilitations());
        }

        [HttpGet]
        public ActionResult UpdateFacilitation(
            int FacilitationId,
            string ProjectName,
            string ProjectDescription,
            string ProjectChallangesConstraints,
            string Expectations,
            string ProjectStatus,
            string RelevantDepartments,
            string FuturePlans,
            decimal InvestmentValue,
            int JobsOpportunities,
            int Permanent,
            int Temporary,
            string ProjectManagerName,
            string CellNumber,
            string PhoneNumber,
            string Email,
            DateTime kt_datepicker_2)
        {
            FacilitationModel model = new FacilitationModel();
            model.FacilitationId = FacilitationId;
            model.ProjectName = ProjectName;
            model.ProjectDescription = ProjectDescription;
            model.ProjectChallangesConstraints = ProjectChallangesConstraints;
            model.Expectations = Expectations;
            model.ProjectStatus = ProjectStatus;
            model.RelevantDepartments = RelevantDepartments;
            model.FuturePlans = FuturePlans;

            model.InvestmentValue = InvestmentValue;
            model.JobsOpportunities = JobsOpportunities;
            model.Permanent = Permanent;
            model.Temporary = Temporary;

            model.ProjectManagerName = ProjectManagerName;
            model.CellNumber = CellNumber;
            model.PhoneNumber = PhoneNumber;
            model.Email = Email;
            model.InceptionDate = kt_datepicker_2;

            if (ModelState.IsValid)
            {
                model = BusinessObject.CreateFacilitation(model);

                Guid CRMID = new ClientManagement.Interface.SaveBusinessLogic().NewLead(
                model.ProjectName,
                model.FirstName,
                model.LastName,
                model.CellNumber,
                model.PhoneNumber,
                model.Email,
                model.ProjectName,
                model.AddressLine1,
                model.AddressLine2,
                model.AddressLine3,
                model.AddressLineCity,
                model.AddressLineState_Province,
                model.AddressLinePostalCode,
                model.AddressLineCountry,
                model.ProjectDescription,
                model.ProjectChallangesConstraints,
                model.ProjectStatus,
                model.Expectations,
                model.FuturePlans,
                model.Totals,
                model.JobsOpportunities,
                model.Temporary,
                model.Permanent,
                Guid.Empty,
                model.CRMOpportunityId,
                model.CRMAccountId,
                model.CRMContactId,
                model.FacilitationId);

                if (CRMID != Guid.Empty && model.FacilitationId != 0)
                {
                    BusinessObject.UpdateFacilitation(CRMID, model.FacilitationId);
                }

                //Guid CRMID = new ClientManagement.Interface.SaveBusinessLogic().NewOpportunity(
                //    model.ProjectName,
                //    model.ProjectDescription,
                //    model.ProjectChallangesConstraints,
                //    model.ProjectStatus,
                //    model.Expectations,
                //    model.FuturePlans,
                //    model.Totals,
                //    model.JobsOpportunities,
                //    model.Temporary,
                //    model.Permanent,
                //    model.CRMOpportunityId,
                //    model.CRMAccountId,
                //    model.CRMContactId);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetFacilitationDetails()
        {
            return View("_AddFacilitation", BusinessObject.GetFacilitationInfo());
        }

        public ActionResult CreateNewFacilitation(int FacilitationId)
        {
            return View("_AddFacilitation", BusinessObject.GetFacilitationInfo(FacilitationId));
        }

        public ActionResult CreateFacilitationRequest(FacilitationRequestModel model)
        {
            BusinessObject.SaveFacilitationRequest(model);

            return View("Index", BusinessObject.GetFacilitations());
        }

        public ActionResult CreateEditFacilitationRequest(int FacilitationId)
        {
            FacilitationRequestModel model = new FacilitationRequestModel();
            model = BusinessObject.GetFacilitationRequestRequest();
            model.FacilitationId = FacilitationId;

            return PartialView("FacilitationRequest", model);
        }

        public ActionResult CreateEditDocumentDataInfo1s(int FacilitationId)
        {
            DocumentManagementModel model = new DocumentManagementModel();
            model = BusinessObject.GetDocumentTypes();
            model.FacilitationId = FacilitationId;
            return PartialView("DocumentUploadDocs", model);
        }

        public ActionResult DeleteFacilitationRequest(int FacilitationRequestId)
        {
            BusinessObject.DeleteFacilitationRequest(FacilitationRequestId);
            return View("Index", BusinessObject.GetFacilitations());
        }

        public ActionResult RemoveDocument(int DocumentManagementId)
        {
            BusinessObject.DeleteDocumentManagement(DocumentManagementId);
            return View("Index", BusinessObject.GetFacilitations());
        }

        public ActionResult DownloadDocuments(int DocumentManagementId)
        {
            var data = BusinessObject.GetDocumentManagementDetail(DocumentManagementId);
            byte[] byteInfo = data.DocumentImage;

            Response.Clear();
            MemoryStream ms = new MemoryStream(byteInfo);
            Response.ContentType = data.ContentType;
            if (data.ContentType.Split('/')[1].ToString().Contains("excel"))
            {
                Response.AddHeader("content-disposition", "attachment;filename= " + data.DocumentName + "." + "xlx");
            }
            else
            {
                if (data.ContentType.Split('/')[1].ToString().Contains("spreadsheetml.sheet"))
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    Response.AddHeader("content-disposition", "attachment; filename=\"" + data.DocumentName + ".xlsx\"");
                }
                else
                    if (data.ContentType.Split('/')[1].ToString().Contains("wordprocessingml.document"))
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                    Response.AddHeader("content-disposition", "attachment; filename=\"" + data.DocumentName + ".docx\"");
                }
                else
                        if (data.ContentType.Split('/')[1].ToString().Contains("presentationml.presentation"))
                {
                    Response.ContentType = "application/vnd.openxmlformats-officedocument.presentationml.presentation";
                    Response.AddHeader("content-disposition", "attachment; filename=\"" + data.DocumentName + ".pptx\"");
                }
                else
                {
                    Response.AddHeader("content-disposition", "attachment;filename= " + data.DocumentName + "." + data.ContentType.Split('/')[1].ToString());
                }
            }
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
            return new FileStreamResult(ms, data.ContentType);
        }

        public ActionResult DownloadSubscriptions(int SubscriptionsId)
        {
            var data = BusinessObject.GetSubscriptionsDetail(SubscriptionsId);
            byte[] byteInfo = data.Documentation;

            Response.Clear();
            MemoryStream ms = new MemoryStream(byteInfo);
            Response.AddHeader("content-disposition", "attachment;filename= " + data.CompanyName + "." + "pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();
            return new FileStreamResult(ms, "pdf");
        }

        [HttpPost]
        public ActionResult CreateEditDocumentData(DocumentManagementModel model)
        {
            DocumentManagementModel models = new DocumentManagementModel();
            if (Request.Files != null && Request.Files.Count == 1)
            {
                var files = Request.Files[0];
                if (files != null && files.ContentLength > 0)
                {
                    var content = new byte[files.ContentLength];
                    files.InputStream.Read(content, 0, files.ContentLength);
                    model.DocumentPath = Request.Url.ToString();
                    models.ContentType = files.ContentType;
                    models.DocumentImage = content;
                    models.DocumentName = model.DocumentName;
                    models.Description = model.Description;
                    models.DocumentTypeId = model.DocumentTypeId;
                    models.FacilitationId = model.FacilitationId;
                    models = BusinessObject.SaveDocument(models);
                }

                Guid CRMID = new ClientManagement.Interface.SaveBusinessLogic().NewFacilitationRequest(
                    models.DocumentName,
                    models.Description,
                    models.DocumentLocationURLs,
                    model.CRMAccountId,Guid.Empty,
                    models.CRMContactId,
                    model.FacilitationId,
                    models.CRMLeadId);
            }
            return View("Index", BusinessObject.GetFacilitations());
        }

        public ActionResult CreateFacilitation()
        {
            var model = new FacilitationModel();
            model.Notes = BusinessObject.GetUserNotes();
            return View("_AddFacilitation", model);
        }

        [HttpPost]
        public ActionResult CreateEditFacilitation(string submitbtn, int FacilitationId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    FacilitationModel modelfilter = new FacilitationModel();
                    modelfilter.Notes = BusinessObject.GetUserNotes();
                    //modelfilter = BusinessObject.GetFacilitationDetail(FacilitationId);
                    return PartialView("_AddFacilitation", modelfilter);
                case "Remove":
                    //BusinessObject.DeleteFacilitation(FacilitationId);  
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditFacilitationData(FacilitationModel Facilitation)
        {
            if (ModelState.IsValid)
            {
                //BusinessObject.CreateFacilitation(Facilitation);
                return RedirectToAction("_AddFacilitation");
            }
            else
            {
                Facilitation.Notes = BusinessObject.GetUserNotes();
                return PartialView("_AddFacilitation", Facilitation);
            }
        }
    }
}

