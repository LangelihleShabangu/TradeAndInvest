using ClientManagement.ServiceDetails;
using System;
using System.Linq;
using System.Web.Mvc;
using Techhill.Framework.DomainTypes.Account;
using Techhill.Framework.Web.Configuration;

namespace ShabanguGroupApplication.UIInterface.Controllers
{
    public class RegisterController : Controller
    {
        public ActionResult ForgotPassword()
        {
            Techhill.Framework.DomainTypes.Account.UserProfileModel model = new UserProfileModel();
            return PartialView("ForgotPassword", model);
        }

        public ActionResult UpdateForgotPasswordOneSMS()
        {
            Techhill.Framework.DomainTypes.Account.UserProfileModel model = new UserProfileModel();
            return PartialView("UpdateForgotPasswordSMS", model);
        }

        public ActionResult UpdateForgotPasswordOneSMS22(UserProfileModel model)
        {
            var mod = new ClientManagement.Interface.SaveBusinessLogic().usp_GetUserInformationbyUsernameOTP(model.UserName).FirstOrDefault();
            if (model == null)
            {
                return Json(new { Success = false, Error = "Incorrect OTP" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                new ClientManagement.Interface.SaveBusinessLogic().UpdateForgotPassword(mod.UserProfileID, model.Password);
                return Json(new { Success = true, Error = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ForgotPasswordTwo(string UserName)
        {
            Techhill.Framework.DomainTypes.Account.UserProfileModel model = new UserProfileModel();
            model = new ClientManagement.Interface.SaveBusinessLogic().GetUSERInformationDetails(UserName).FirstOrDefault();
            if (model != null)
            {
                string LocationURLs = string.Format("{0}/Register/UpdateForgotPasswordOne?UserProfileID={1}", System.Configuration.ConfigurationManager.AppSettings["url"].ToString(), model.UserProfileID);
                //SendMail(LocationURLs, model.Email);
                string OTP = UserName + model.UserProfileID;
                new ClientManagement.BusinessLogic.Messaging().SendSMS(model.Cellphone, model.FirstName, model.LastName, OTP);
            }
            return Json(new { Success = true, Error = "" }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ForgotPassword1(string UserName)
        {
            ClientManagement.Interface.SaveBusinessLogic obj = new ClientManagement.Interface.SaveBusinessLogic();
            int UserId = obj.GetUSER_ID(UserName);
            if (UserId > 0)
            {
                obj.GetUSERInformation(UserId);
            }

            return PartialView("ForgotPassword1");
        }

        [HttpGet]
        public ActionResult EditUser1(
           string CompanyName,
           string CountryRegion,
           string NumberofEmployees,
           string CompanyContactPhone,
           string CompanyContactPerson,
           string Sector,
           string MainCompany,
           string SummaryProject,
           string RequestInformation,
           string Password,
           string EmailAddress,
           string Category,
           string AddressLine1,
           string AddressLine2,
           string AddressLine3,
           string AddressLineCity,
           string AddressLineState_Province,
           string AddressLinePostalCode,
           string AddressLineCountry)
        {

            CompanyName = string.IsNullOrEmpty(CompanyName) ? "None" : CompanyName;
            CountryRegion = string.IsNullOrEmpty(CountryRegion) ? "None" : CountryRegion;
            NumberofEmployees = string.IsNullOrEmpty(NumberofEmployees) ? "0" : NumberofEmployees;
            CompanyContactPhone = string.IsNullOrEmpty(CompanyContactPhone) ? "None" : CompanyContactPhone;
            CompanyContactPerson = string.IsNullOrEmpty(CompanyContactPerson) ? "None" : CompanyContactPerson;
            Sector = string.IsNullOrEmpty(Sector) ? "None" : Sector;
            MainCompany = string.IsNullOrEmpty(MainCompany) ? "None" : MainCompany;
            SummaryProject = string.IsNullOrEmpty(SummaryProject) ? "None" : SummaryProject;
            RequestInformation = string.IsNullOrEmpty(RequestInformation) ? "None" : RequestInformation;

            AddressLine1 = string.IsNullOrEmpty(AddressLine1) ? "None" : AddressLine1;
            AddressLine2 = string.IsNullOrEmpty(AddressLine2) ? "None" : AddressLine2;
            AddressLine3 = string.IsNullOrEmpty(AddressLine3) ? "None" : AddressLine3;
            AddressLineCity = string.IsNullOrEmpty(AddressLineCity) ? "None" : AddressLineCity;
            AddressLineState_Province = string.IsNullOrEmpty(AddressLineState_Province) ? "None" : AddressLineState_Province;
            AddressLinePostalCode = string.IsNullOrEmpty(AddressLinePostalCode) ? "None" : AddressLinePostalCode;
            AddressLineCountry = string.IsNullOrEmpty(AddressLineCountry) ? "None" : AddressLineCountry;

            var CRMUserProfileId = Guid.Empty;
            var CRMContactId = Guid.Empty;

            string LastName = "None";
            string Telephone = CompanyContactPhone;
            string Cellphone = CompanyContactPhone;
            string FirstName = CompanyContactPerson;

            ClientManagement.Interface.Models.UserProfileModel modelInfo = new ClientManagement.Interface.Models.UserProfileModel();
            modelInfo.UserName = EmailAddress;
            modelInfo.Password = Password;
            modelInfo.LastName = LastName;
            modelInfo.Telephone = Telephone;
            modelInfo.Cellphone = CompanyContactPhone;
            modelInfo.Email = EmailAddress;

            ClientManagement.Interface.SaveBusinessLogic obj = new ClientManagement.Interface.SaveBusinessLogic();
            var Passwords = Techhill.Framework.Tools.Encode(Password);

            //obj.RegisterUser(EmailAddress, Passwords);
            //obj.RegisterDatabaseUser(EmailAddress);
            //obj.Addsrvrolemember(EmailAddress);
            int UserProfileID = obj.SaveUserDetails(EmailAddress, Passwords, FirstName, LastName, Telephone, Cellphone, EmailAddress);
            obj.SaveUserRole(UserProfileID);
            obj.RegisterUserNew(UserProfileID, Password);

            modelInfo.UserProfileID = UserProfileID;
            modelInfo.UserName = string.Empty;
            modelInfo.Password = string.Empty;
            modelInfo.LastName = string.Empty;
            modelInfo.Telephone = string.Empty;
            modelInfo.Cellphone = string.Empty;
            modelInfo.Email = string.Empty;

            ClientManagement.Interface.Models.CompanyProfileModel companyProfileModel = new ClientManagement.Interface.Models.CompanyProfileModel();
            companyProfileModel.CompanyName = string.IsNullOrEmpty(CompanyName) ? "None" : CompanyName;
            companyProfileModel.CountryRegion = string.IsNullOrEmpty(CountryRegion) ? "None" : CountryRegion;
            companyProfileModel.NumberofEmployees = string.IsNullOrEmpty(NumberofEmployees) ? "0" : NumberofEmployees;
            companyProfileModel.CompanyContactPhone = string.IsNullOrEmpty(CompanyContactPhone) ? "None" : CompanyContactPhone;
            companyProfileModel.CompanyContactPerson = string.IsNullOrEmpty(CompanyContactPerson) ? "None" : CompanyContactPerson;
            companyProfileModel.Sector = string.IsNullOrEmpty(Sector) ? "None" : Sector;
            companyProfileModel.MainCompany = string.IsNullOrEmpty(MainCompany) ? "None" : MainCompany;
            companyProfileModel.SummaryProject = string.IsNullOrEmpty(SummaryProject) ? "None" : SummaryProject;
            companyProfileModel.RequestInformation = string.IsNullOrEmpty(RequestInformation) ? "None" : RequestInformation;
            companyProfileModel.Password = Password;
            companyProfileModel.EmailAddress = EmailAddress;

            AddressLine1 = string.IsNullOrEmpty(AddressLine1) ? "None" : AddressLine1;
            AddressLine2 = string.IsNullOrEmpty(AddressLine2) ? "None" : AddressLine2;
            AddressLine3 = string.IsNullOrEmpty(AddressLine3) ? "None" : AddressLine3;
            AddressLineCity = string.IsNullOrEmpty(AddressLineCity) ? "None" : AddressLineCity;
            AddressLineState_Province = string.IsNullOrEmpty(AddressLineState_Province) ? "None" : AddressLineState_Province;
            AddressLinePostalCode = string.IsNullOrEmpty(AddressLinePostalCode) ? "None" : AddressLinePostalCode;
            AddressLineCountry = string.IsNullOrEmpty(AddressLineCountry) ? "None" : AddressLineCountry;

            int UserProfileId = obj.RegisterCompany(UserProfileID, CompanyName, CountryRegion, NumberofEmployees, CompanyContactPhone, CompanyContactPerson, Sector, MainCompany, SummaryProject, RequestInformation, Passwords, EmailAddress, Category,
            AddressLine1, AddressLine2, AddressLine3, AddressLineCity, AddressLineState_Province, AddressLinePostalCode, AddressLineCountry, CRMUserProfileId, CRMContactId);

            ClientManagement.Interface.SaveBusinessLogic saveBusinessLogic = new ClientManagement.Interface.SaveBusinessLogic();

            Guid CRMID = saveBusinessLogic.CreateCRMQueryAccount(
                new ClientManagement.Interface.CrmQuery()
                {
                    ContactNumber = companyProfileModel.CompanyContactPhone,
                    Description = "Registration",
                    Email = companyProfileModel.EmailAddress,
                    FirstName = companyProfileModel.CompanyName,
                    LastName = CompanyContactPerson,
                    Message = "Informal",
                    Title = "Trader",
                    address1_line1 = AddressLine1,
                    address1_line2 = AddressLine2,
                    address1_line3 = AddressLine3,
                    address1_postalcode = AddressLinePostalCode,
                    addresslinecity = AddressLineCity,
                    addresslineprovince = AddressLineState_Province,
                });

            if (CRMID != Guid.Empty)
            {
                CRMUserProfileId = CRMID;
                CRMContactId = CRMID;
            }

            obj.RegisterCompany(UserProfileID, CompanyName, CountryRegion, NumberofEmployees, CompanyContactPhone, CompanyContactPerson, Sector, MainCompany, SummaryProject, RequestInformation, Passwords, EmailAddress, Category,
            AddressLine1, AddressLine2, AddressLine3, AddressLineCity, AddressLineState_Province, AddressLinePostalCode, AddressLineCountry, CRMUserProfileId, CRMContactId);

            //new SyncDataInformation().NewAccountInformation(
            //    FirstName, 
            //    LastName, 
            //    EmailAddress, Guid.Empty, Guid.Empty, 
            //    CompanyName, 
            //    AddressLine1, 
            //    AddressLine2, 
            //    AddressLine3, 
            //    AddressLineCity,
            //    AddressLineState_Province, 
            //    AddressLinePostalCode, 
            //    AddressLineCountry, 
            //    UserProfileId,
            //    Category);           

            return PartialView("Register", companyProfileModel);
        }
                
        public ActionResult RegisterInvestor()
        {
            ClientManagement.Interface.Models.CompanyProfileModel model = new ClientManagement.Interface.Models.CompanyProfileModel();
            return PartialView("RegisterInvestor", model);
        }

        public ActionResult Register()
        {
            ClientManagement.Interface.Models.CompanyProfileModel model = new ClientManagement.Interface.Models.CompanyProfileModel();
            return PartialView("Register", model);
        }
    }
}