using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using EmployeesManagement.DomainModel;
using System;
using System.IO;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class RegistrationController : BaseController<AdminLogic>  
    {
        [HttpGet]
        public ActionResult UpdateRegistration(
            int RegistrationId,
            int CompanyId,
            int AddressId,
            int ContactInformationId,
            string FirstName,
            string Surname,
            string IdNumber,
            string CellNumber,
            string PhoneNumber,
            string Email,
            string AddressLine1,
            string AddressLine2,
            string AddressLine3,
            string AddressLine4)
        {
            RegistrationModel model = new RegistrationModel();
            model.RegistrationId = RegistrationId;
            model.CompanyId = CompanyId;
            model.AddressId = AddressId;
            model.ContactInformationId = ContactInformationId;
            model.FirstName = FirstName;
            model.Surname = Surname;
            model.IdNumber = IdNumber;
            model.CellNumber = CellNumber;
            model.PhoneNumber = PhoneNumber;
            model.Email = Email;
            model.AddressLine1 = AddressLine1;
            model.AddressLine2 = AddressLine2;
            model.AddressLine3 = AddressLine3;
            model.AddressLine4 = AddressLine4;

            if (ModelState.IsValid)
            {
                //BusinessObject.UpdateEmployees(model);
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpGet]
        public ActionResult InsertAssessment(
            int SubscriptionsId,
            int AssessmentId,
            string Company_Based,
            string Exporter_Region,
            string Exporter_Country,
            string Industry_Sector,
            string Export_products,
            string Export_markets,
            decimal Companys_annual_turnover,
            int Number_of_years_you_have_exported,
            string Special_technical_support,
            string Government_export_assistance,
            string specify_location_Assistance,
            string Specify_Company_Location,
            string Country_Expand,
            string Regional_export_markets_future_expand,
            bool chkExporter_Education_Seminar,
            bool chkParticipate_at_international_Trade,
            bool chkMarket_Research,
            bool chkGuidance_on_export,
            bool chkFinancial_Assistance,
            bool chkConsultation_with_trade_specialist,
            bool chkInternational_Trade_Missions,
            bool chkTrade_Leads,
            bool chkShipping_Logistics_consulting,
            bool chkOther,
            string Additional_Comments,
            string Challenges_facing_Exporter,
            string Other_issues)
        {
            AssessmentModel model = new AssessmentModel();
            model.SubscriptionsId = SubscriptionsId;
            model.AssessmentId = AssessmentId;
            model.Exporter_Region = Exporter_Region;
            model.Exporter_Country = Exporter_Country;
            model.Industry_Sector = Industry_Sector;
            model.Export_products = Export_products;
            model.Export_markets = Export_markets;
            model.Companys_annual_turnover = Companys_annual_turnover;
            model.Number_of_years_you_have_exported = Number_of_years_you_have_exported;
            model.Special_technical_support = Special_technical_support;

            model.Government_export_assistance = Government_export_assistance;
            model.specify_location_Assistance = specify_location_Assistance;

            model.Specify_Company_Location = Specify_Company_Location;
            model.Country_Expand = Country_Expand;
            model.Regional_export_markets_future_expand = Regional_export_markets_future_expand;

            model.chkExporter_Education_Seminar = chkExporter_Education_Seminar;
            model.chkParticipate_at_international_Trade = chkParticipate_at_international_Trade;
            model.chkMarket_Research = chkMarket_Research;
            model.chkGuidance_on_export = chkGuidance_on_export;
            model.chkFinancial_Assistance = chkFinancial_Assistance;
            model.chkConsultation_with_trade_specialist = chkConsultation_with_trade_specialist;
            model.chkInternational_Trade_Missions = chkInternational_Trade_Missions;
            model.chkTrade_Leads = chkTrade_Leads;

            model.chkShipping_Logistics_consulting = chkShipping_Logistics_consulting;
            model.chkOther = chkOther;
            model.Additional_Comments = Additional_Comments;
            model.Challenges_facing_Exporter = Challenges_facing_Exporter;
            model.Other_issues = Other_issues;
            model.Company_Based = Company_Based;

            if (ModelState.IsValid)
            {
                model = BusinessObject.SaveAssessment(model);
                ClientManagement.Interface.SaveBusinessLogic saveBusinessLogic = new ClientManagement.Interface.SaveBusinessLogic();

                Guid CRMID = saveBusinessLogic.NewAssessmentDetails(
                    model.FirstName,
                    model.Surname,
                    model.Email_Address,
                    model.CRMContactId,
                    model.CompanyName,
                    model.Address_Line1,
                    model.Address_Line2,
                    model.Address_Line3,
                    model.AddressLineCity,
                    model.AddressLineState_Province,
                    model.AddressLinePostalCode,
                    model.AddressLineCountry,
                    model.CRMContactId,
                    model.Number_of_years_you_have_exported,
                    model.Number_of_Company_employees,
                    model.Companys_annual_turnover,
                    model.Export_products,
                    model.Export_markets,
                    model.Exporter_Region,
                    model.Exporter_Country,
                    model.Government_export_assistance,
                    model.Specify_Company_Location,
                    model.Additional_Comments,
                    model.Challenges_facing_Exporter,
                    model.Other_issues,
                    model.UserProfileId,
                    model.DocumentLocationURL,
                    model.chkExporter_Education_Seminar,
                    model.chkParticipate_at_international_Trade,
                    model.chkMarket_Research,
                    model.chkGuidance_on_export,
                    model.chkFinancial_Assistance,
                    model.chkConsultation_with_trade_specialist,
                    model.chkInternational_Trade_Missions,
                    model.chkTrade_Leads,
                    model.chkShipping_Logistics_consulting,
                    model.chkOther, model.Company_Based);

                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
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
        public ActionResult CreateRegistrations(SubscriptionsModel Registration)
        {            
            if (Request.Files != null && Request.Files.Count == 1)
            {
                var files = Request.Files[0];
                if (files != null && files.ContentLength > 0)
                {
                    var content = new byte[files.ContentLength];
                    files.InputStream.Read(content, 0, files.ContentLength);
                    Registration.Documentation = content;
                    Registration = BusinessObject.SaveSubscriptions(Registration);
                }
            }
            else
            {
                Registration = BusinessObject.SaveSubscriptions(Registration); 
            }

            Guid CRMID = new ClientManagement.Interface.SaveBusinessLogic().NewSubscription(
                Registration.FirstName,
                Registration.Surname,
                Registration.Email_Address,
                Registration.CRMContactId,
                Registration.CompanyName,
                Registration.Address_Line1,
                Registration.Address_Line2,
                Registration.Address_Line3,
                Registration.Address_Line4,
                Registration.AddressLineState_Province,
                Registration.AddressLinePostalCode,
                Registration.AddressLineCountry,
                Registration.CRMAccountId,
                Registration.Woman_Owned_Business == "Yes" ? true : false,
                10,
                Registration.Planning_to_Export == "Yes" ? true : false,
                Registration.Company_Based == "Yes" ? true : false,
                Registration.Which_Region,
                Registration.Which_Region,
                Registration.Which_Region,
                true,
                0,
                0,
                Convert.ToDecimal(0),
                string.Empty,
                string.Empty,
                string.Empty,
                string.Empty,
                false,
                string.Empty,
                string.Empty,
                string.Empty, string.Empty,
                Registration.UserProfileId,
                Registration.DocumentLocationURL,
                Registration.Phone_Number, Registration.Contact_Phone);

            BusinessObject.SaveSubscriptions(Registration.SubscriptionsId, CRMID.ToString());

            return Json(new { success = true });

            //return RedirectToAction("_AddRegistration");           
        }        

        public ActionResult GetRegistrationDetails()
        {
            return View("_AddRegistration", BusinessObject.GetRegistrationInfo());
        }

        public ActionResult CreateAssessment()
        {
            return View("_AddAssessment", BusinessObject.GetAssessmentInfo());
        }
        
        public ActionResult CreateRegistration()
        {
            return View("_AddRegistration", BusinessObject.GetRegistrationInfo());
        }

        [HttpPost]
        public ActionResult CreateEditRegistration(string submitbtn, int RegistrationId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    RegistrationModel modelfilter = new RegistrationModel();
                    //modelfilter = BusinessObject.GetRegistrationDetail(RegistrationId);
                    return PartialView("_AddRegistration", modelfilter);
                case "Remove":
                    //BusinessObject.DeleteRegistration(RegistrationId);  
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditRegistrationData(RegistrationModel Registration)
        {
            if (ModelState.IsValid)
            {
                //BusinessObject.CreateRegistration(Registration);
                return RedirectToAction("_AddRegistration");
            }
            else
                return PartialView("_AddRegistration", Registration);
        }
    }
}

