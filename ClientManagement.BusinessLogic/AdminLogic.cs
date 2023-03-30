using ClientManagement.DataObject;
using ClientManagement.DomainModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Techhill.Framework.BusinessProcess;

namespace ClientManagement.BusinessLogic
{
    public class AdminLogic : BaseBusinessObject<ClientManagementContext>
    {
        public AdminLogic(string connection)
            : base(connection)
        {
        }

        #region Facilitation
        public FacilitationModel CreateFacilitation(FacilitationModel model)
        {
            Guid CRMLeadId = Guid.Empty;
            Guid CRMOpportunityId = Guid.Empty;  
            var FacilitationInfo = ContextDatabase.Facilitation.FirstOrDefault(f => f.FacilitationId == model.FacilitationId);
            if (FacilitationInfo == null)
            {
                FacilitationInfo = new Facilitation();
                ContextDatabase.Facilitation.Add(FacilitationInfo);

                FacilitationInfo.CreatedDate = System.DateTime.Now;
                FacilitationInfo.CreatedBy = this.CurrentUser.UserProfileId;
                FacilitationInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                FacilitationInfo.ModifiedDate = System.DateTime.Now;
                FacilitationInfo.CRMLeadId = CRMLeadId;
                FacilitationInfo.CRMOpportunityId = CRMOpportunityId;
            }
            else
            {
                FacilitationInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                FacilitationInfo.ModifiedDate = System.DateTime.Now;
                CRMLeadId = FacilitationInfo.CRMLeadId.Value;
                CRMOpportunityId = FacilitationInfo.CRMOpportunityId.Value;
            }

            FacilitationInfo.ProjectName = model.ProjectName;
            FacilitationInfo.ProjectDescription = model.ProjectDescription;
            FacilitationInfo.ProjectChallangesConstraints = model.ProjectChallangesConstraints;
            FacilitationInfo.Expectations = model.Expectations;
            FacilitationInfo.ProjectStatus = model.ProjectStatus;
            FacilitationInfo.RelevantDepartments = model.RelevantDepartments;
            FacilitationInfo.FuturePlans = model.FuturePlans;

            FacilitationInfo.InvestmentValue = model.InvestmentValue;
            FacilitationInfo.JobsOpportunities = model.JobsOpportunities;
            FacilitationInfo.Permanent = model.Permanent;
            FacilitationInfo.Temporary = model.Temporary;
            FacilitationInfo.Totals = model.Temporary + model.Permanent;

            FacilitationInfo.ProjectManagerName = model.ProjectManagerName;
            FacilitationInfo.CellNumber = model.CellNumber;
            FacilitationInfo.PhoneNumber = model.PhoneNumber;
            FacilitationInfo.Email = model.Email;
            FacilitationInfo.CreatedDate = model.InceptionDate.Year == 0001 ? System.DateTime.Now : model.InceptionDate;

            FacilitationInfo.IsDeleted = false;
            ContextDatabase.SaveChanges();
            
            var UserProfile = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId);
            var UserProfileUserProfileAddressDetails = ContextDatabase.UserProfileUserProfileAddressDetails.FirstOrDefault(f => f.UserProfileId == UserProfile.UserProfileId && f.CRMContactId != Guid.Empty);

            if (UserProfileUserProfileAddressDetails != null)
            {
                model.CRMAccountId = UserProfileUserProfileAddressDetails.CRMAccountId;
                model.CRMContactId = UserProfileUserProfileAddressDetails.CRMContactId;
            }
            
            model.FacilitationId = FacilitationInfo.FacilitationId;
            model.FirstName = UserProfile.FirstName;
            model.LastName = UserProfile.LastName;
            model.Cellphone = UserProfile.Cellphone;            
            model.Email = UserProfile.Email;

            model.ProjectName = FacilitationInfo.ProjectName;
            model.AddressLine1 = UserProfileUserProfileAddressDetails.AddressDetails.AddressLine1;
            model.AddressLine2 = UserProfileUserProfileAddressDetails.AddressDetails.AddressLine2;
            model.AddressLine3 = UserProfileUserProfileAddressDetails.AddressDetails.AddressLine3;
            model.AddressLineCity = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCity;
            model.AddressLineState_Province = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineState_Province;
            model.AddressLinePostalCode = UserProfileUserProfileAddressDetails.AddressDetails.AddressLinePostalCode;
            model.AddressLineCountry = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCountry;

            //if (CRMOpportunityId != Guid.Empty)
            //{
            //    new ServiceDetails.SyncDataInformation().NewOpportunity(
            //    FacilitationInfo.ProjectName,
            //    FacilitationInfo.ProjectDescription,
            //    FacilitationInfo.ProjectChallangesConstraints,
            //    FacilitationInfo.ProjectStatus,
            //    FacilitationInfo.Expectations,
            //    FacilitationInfo.FuturePlans,
            //    FacilitationInfo.Totals,
            //    FacilitationInfo.JobsOpportunities,
            //    FacilitationInfo.Temporary,
            //    FacilitationInfo.Permanent,
            //    FacilitationInfo.CRMOpportunityId.Value,
            //    UserProfileUserProfileAddressDetails.CRMAccountId,                
            //    UserProfileUserProfileAddressDetails.CRMContactId);                
            //}

            //if (CRMLeadId == Guid.Empty)
            //{
            //    new ServiceDetails.SyncDataInformation().NewLead(
            //    FacilitationInfo.ProjectName,
            //    UserProfile.FirstName,
            //    UserProfile.LastName,
            //    UserProfile.Cellphone,
            //    UserProfile.Cellphone,
            //    UserProfile.Email,
            //    FacilitationInfo.ProjectName,
            //    UserProfileUserProfileAddressDetails.AddressDetails.AddressLine1,
            //    UserProfileUserProfileAddressDetails.AddressDetails.AddressLine2,
            //    UserProfileUserProfileAddressDetails.AddressDetails.AddressLine3,
            //    UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCity,
            //    UserProfileUserProfileAddressDetails.AddressDetails.AddressLineState_Province,
            //    UserProfileUserProfileAddressDetails.AddressDetails.AddressLinePostalCode,
            //    UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCountry,
            //    FacilitationInfo.ProjectDescription,
            //    FacilitationInfo.ProjectChallangesConstraints,
            //    FacilitationInfo.ProjectStatus,
            //    FacilitationInfo.Expectations,
            //    FacilitationInfo.FuturePlans,
            //    FacilitationInfo.Totals,
            //    FacilitationInfo.JobsOpportunities,
            //    FacilitationInfo.Temporary,
            //    FacilitationInfo.Permanent,
            //    CRMLeadId,
            //    CRMOpportunityId,
            //    UserProfileUserProfileAddressDetails.CRMAccountId,
            //    UserProfileUserProfileAddressDetails.CRMContactId,
            //    FacilitationInfo.FacilitationId);
            //}

            return model;
        }

        public void UpdateFacilitation(Guid guid, int FacilitationId)
        {
            Guid CRMLeadId = Guid.Empty;
            Guid CRMOpportunityId = Guid.Empty;
            var FacilitationInfo = ContextDatabase.Facilitation.FirstOrDefault(f => f.FacilitationId == FacilitationId);
            if (FacilitationInfo == null)
            {
                FacilitationInfo = new Facilitation();
                ContextDatabase.Facilitation.Add(FacilitationInfo);

                FacilitationInfo.CreatedDate = System.DateTime.Now;
                FacilitationInfo.CreatedBy = this.CurrentUser.UserProfileId;
                FacilitationInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                FacilitationInfo.ModifiedDate = System.DateTime.Now;
                FacilitationInfo.CRMLeadId = CRMLeadId;
                FacilitationInfo.CRMOpportunityId = CRMOpportunityId;
            }
            else
            {
                FacilitationInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                FacilitationInfo.ModifiedDate = System.DateTime.Now;
                CRMLeadId = FacilitationInfo.CRMLeadId.Value;
                CRMOpportunityId = FacilitationInfo.CRMOpportunityId.Value;
            }

            FacilitationInfo.CRMLeadId = guid;
            ContextDatabase.SaveChanges();
        }

            public FacilitationModel GetFacilitationDetail(int FacilitationId)
        {
            FacilitationModel model = new FacilitationModel();
            var FacilitationDetail = ContextDatabase.Facilitation.FirstOrDefault(f => f.FacilitationId == FacilitationId && f.CreatedBy == this.CurrentUser.UserProfileId);
            Mapper.MapObjects(FacilitationDetail, ref model);

            //List<FacilitationModel> FacilitationList = new List<FacilitationModel>();
            //var FacilitationDetailList = ContextDatabase.Facilitation.Where(f => !f.IsDeleted).ToList();
            //Mapper.MapObjects<DataObject.Facilitation, FacilitationModel>(FacilitationDetailList, ref FacilitationList);

            //model.FacilitationList = FacilitationList.ToList();

            return model;
        }

        public FacilitationRequestModel GetFacilitationRequestRequest()
        {
            FacilitationRequestModel model = new FacilitationRequestModel();

            List<FacilitationRequestModel> FacilitationRequestList = new List<FacilitationRequestModel>();
            var FacilitationRequestDetailList = ContextDatabase.FacilitationRequest.Where(f => !f.IsDeleted && f.CreatedBy == this.CurrentUser.UserProfileId).ToList();
            Mapper.MapObjects<DataObject.FacilitationRequest, FacilitationRequestModel>(FacilitationRequestDetailList, ref FacilitationRequestList);
            model.FacilitationRequestList = FacilitationRequestList.ToList();

            model.Notes = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId).Notes;

            return model;
        }

        public FacilitationModel GetFacilitations()
        {
            FacilitationModel model = new FacilitationModel();

            List<FacilitationModel> FacilitationList = new List<FacilitationModel>();
            var FacilitationDetailList = ContextDatabase.Facilitation.Where(f => !f.IsDeleted && f.CreatedBy == this.CurrentUser.UserProfileId).ToList();
            Mapper.MapObjects<DataObject.Facilitation, FacilitationModel>(FacilitationDetailList, ref FacilitationList);
            model.FacilitationsList = FacilitationList.ToList();

            model.Notes = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId).Notes;

            return model;
        }

        public FacilitationModel GetFacilitationInfo(int FacilitationId)
        {
            FacilitationModel model = new FacilitationModel();
            var FacilitationDetail = ContextDatabase.Facilitation.FirstOrDefault(f => f.FacilitationId == FacilitationId);
            Mapper.MapObjects(FacilitationDetail, ref model);

            model.Notes = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId).Notes;
            return model;
        }

        public FacilitationModel GetFacilitationInfo()
        {
            FacilitationModel model = new FacilitationModel();
            var FacilitationDetail = ContextDatabase.Facilitation.FirstOrDefault(f => f.CreatedBy == this.CurrentUser.UserProfileId);
            Mapper.MapObjects(FacilitationDetail, ref model);
            model.Notes = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId).Notes;
            return model;
        }

        public FacilitationModel GetFacilitationDetail()
        {
            FacilitationModel model = new FacilitationModel();

            List<FacilitationModel> FacilitationList = new List<FacilitationModel>();
            var FacilitationDetailList = ContextDatabase.Facilitation.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Facilitation, FacilitationModel>(FacilitationDetailList, ref FacilitationList);

            model.Notes = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId).Notes;

            return model;
        }

        public void DeleteFacilitation(int FacilitationId)
        {
            var Facilitation = ContextDatabase.Facilitation.FirstOrDefault(f => f.FacilitationId == FacilitationId);
            Facilitation.IsDeleted = true;
            ContextDatabase.SaveChanges();
        }
        #endregion

        #region Facilitation
        public SubscriptionsModel GetRegistrationInfo()
        {
            SubscriptionsModel model = new SubscriptionsModel();

            List<AreaModel> AreaList = new List<AreaModel>();
            var AreaLST = ContextDatabase.Area.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Area, AreaModel>(AreaLST, ref AreaList);

            var userProfile = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId);
            var AccountData = ContextDatabase.UserProfileUserProfileAddressDetails.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId);

            if (AccountData != null)
            {
                model.Address_Line1 = AccountData.AddressDetails.AddressLine1;
                model.Address_Line2 = AccountData.AddressDetails.AddressLine2;
                model.Address_Line3 = AccountData.AddressDetails.AddressLine3;
                model.Address_Line4 = AccountData.AddressDetails.AddressLinePostalCode;
            }

            model.Notes = userProfile.Notes;
            model.AreaList = AreaList.ToList();

            return model;
        }

        public AssessmentModel GetAssessmentInfo()
        {
            AssessmentModel model = new AssessmentModel();

            model.AssessmentList = ContextDatabase.Database.SqlQuery<AssessmentModel>(string.Format("{0} {1}", "usp_GetAssessmentInfo", this.CurrentUser.UserProfileId)).ToList();
            foreach (var item in model.AssessmentList)
            {
                model.AssessmentId = item.AssessmentId;
                model.SubscriptionsId = item.SubscriptionsId;
                model.FirstName = item.FirstName;
                model.CompanyName = item.CompanyName;
                model.Email_Address = item.Email_Address;
                model.Company_Based = item.Company_Based;
                model.Exporter = item.Exporter;

                model.Specify_Company_Location = item.Specify_Company_Location;
                model.Exporter_Region = item.Exporter_Region;
                model.Exporter_Country = item.Exporter_Country;
                model.Number_of_years_you_have_exported = item.Number_of_years_you_have_exported;
                model.Number_of_Company_employees = item.Number_of_Company_employees;
                model.Companys_annual_turnover = item.Companys_annual_turnover;
                model.Industry_Sector = item.Industry_Sector;
                model.Export_products = item.Export_products;
                model.Export_markets = item.Export_markets;
                model.Regional_export_markets_future_expand = item.Regional_export_markets_future_expand;
                model.Country_Expand = item.Country_Expand;
                model.Technical_Support = item.Technical_Support;
                model.Government_export_assistance = item.Government_export_assistance;
                model.specify_location_Assistance = item.specify_location_Assistance;
                model.Special_technical_support = item.Special_technical_support;

                model.chkExporter_Education_Seminar = item.chkExporter_Education_Seminar;
                model.chkParticipate_at_international_Trade = item.chkParticipate_at_international_Trade;
                model.chkMarket_Research = item.chkMarket_Research;
                model.chkGuidance_on_export = item.chkGuidance_on_export;
                model.chkFinancial_Assistance = item.chkFinancial_Assistance;
                model.chkConsultation_with_trade_specialist = item.chkConsultation_with_trade_specialist;
                model.chkInternational_Trade_Missions = item.chkInternational_Trade_Missions;
                model.chkTrade_Leads = item.chkTrade_Leads;
                model.chkShipping_Logistics_consulting = item.chkShipping_Logistics_consulting;
                model.chkOther = item.chkOther;
                model.Additional_Comments = item.Additional_Comments;
                model.Challenges_facing_Exporter = item.Challenges_facing_Exporter;
                model.Other_issues = item.Other_issues;
            }

            List<AreaModel> AreaList = new List<AreaModel>();
            var AreaLST = ContextDatabase.Area.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Area, AreaModel>(AreaLST, ref AreaList);

            List<IndustryModel> IndustryList = new List<IndustryModel>();
            var IndustryLST = ContextDatabase.Industry.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Industry, IndustryModel>(IndustryLST, ref IndustryList);

            model.AreaList = AreaList.ToList();

            model.IndustryList = IndustryList.ToList();

            model.Notes = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId).Notes;

            return model;
        }

        #endregion

        public string GetUserNotes()
        {
            return ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId).Notes;
        }

        #region DocumentManagement       

        public int CreateDocumentManagement(DocumentManagementModel DocumentManagementIdDomainModel)
        {
            var DocumentManagementInfo = ContextDatabase.DocumentManagementFolder.FirstOrDefault(f => f.DocumentManagementFolderId == DocumentManagementIdDomainModel.DocumentManagementId);
            if (DocumentManagementInfo == null)
            {
                DocumentManagementInfo = new DocumentManagementFolder();
                ContextDatabase.DocumentManagementFolder.Add(DocumentManagementInfo);
            }
            DocumentManagementInfo.Name = DocumentManagementIdDomainModel.Name;
            DocumentManagementInfo.Description = DocumentManagementIdDomainModel.Description;
            DocumentManagementInfo.IsDeleted = false;

            ContextDatabase.SaveChanges();
            return DocumentManagementInfo.DocumentManagementFolderId;
        }

        public DocumentManagementModel GetDocumentManagementDetail(int DocumentManagementId)
        {
            DocumentManagementModel model = new DocumentManagementModel();
            var DocumentManagementDetail = ContextDatabase.DocumentManagement.FirstOrDefault(f => f.DocumentManagementId == DocumentManagementId);
            Mapper.MapObjects(DocumentManagementDetail, ref model);

            return model;
        }

        public SubscriptionsModel GetSubscriptionsDetail(int SubscriptionsId)
        {
            SubscriptionsModel model = new SubscriptionsModel();
            var DocumentManagementDetail = ContextDatabase.Subscriptions.FirstOrDefault(f => f.SubscriptionsId == SubscriptionsId);
            model.CompanyName = DocumentManagementDetail.CompanyName;
            model.Documentation = DocumentManagementDetail.Company_Profile;

            return model;
        }

        public int DeleteDocumentManagement(int DocumentManagementId)
        {
            var DocumentManagement = ContextDatabase.DocumentManagement.FirstOrDefault(f => f.DocumentManagementId == DocumentManagementId);
            DocumentManagement.IsDeleted = true;
            ContextDatabase.SaveChanges();
            return DocumentManagement.DocumentManagementId;
        }

        public bool CanDelete(int UserId)
        {
            return ContextDatabase.UserProfile.Any(f => f.PasswordChangeRequired && f.UserProfileId == UserId);
        }

        public int DeleteDocumentManagementFolder(int DocumentManagementFolderId)
        {
            var DocumentManagement = ContextDatabase.DocumentManagementFolder.FirstOrDefault(f => f.DocumentManagementFolderId == DocumentManagementFolderId);
            DocumentManagement.IsDeleted = true;
            ContextDatabase.SaveChanges();
            return DocumentManagement.DocumentManagementFolderId;
        }

        public void SaveSubscriptions(int SubscriptionsId, string guid)
        {
            var DocsInfo = ContextDatabase.Subscriptions.FirstOrDefault(f => f.SubscriptionsId == SubscriptionsId);
            if (DocsInfo == null)
            {
                DocsInfo = new Subscriptions();
                ContextDatabase.Subscriptions.Add(DocsInfo);

                DocsInfo.CreatedDate = System.DateTime.Now;
                DocsInfo.CreatedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
            }
            DocsInfo.Company_Based = guid.ToString();
            ContextDatabase.SaveChanges();
        }

            public SubscriptionsModel SaveSubscriptions(SubscriptionsModel models)
        {
            var DocsInfo = ContextDatabase.Subscriptions.FirstOrDefault(f => f.SubscriptionsId == models.SubscriptionsId);
            if (DocsInfo == null)
            {
                DocsInfo = new Subscriptions();
                ContextDatabase.Subscriptions.Add(DocsInfo);

                DocsInfo.CreatedDate = System.DateTime.Now;
                DocsInfo.CreatedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
            }

            DocsInfo.FirstName = models.FirstName;
            DocsInfo.Surname = models.Surname;
            DocsInfo.Woman_Owned_Business = models.Woman_Owned_Business;
            DocsInfo.CompanyName = models.CompanyName;
            //DocsInfo.Product_Services        = models.Product_Services;
            DocsInfo.Years_in_Operation = models.Years_in_Operation;
            DocsInfo.Exporter = models.Exporter;
            DocsInfo.Planning_to_Export = models.Planning_to_Export;
            DocsInfo.Which_Region = models.Which_Region;
            DocsInfo.Country = models.Country == null ? "None" : models.Country;
            DocsInfo.Contact_Phone = models.Contact_Phone;
            DocsInfo.Phone_Number = models.Phone_Number;
            DocsInfo.Email_Address = models.Email_Address;
            DocsInfo.Address_Line1 = models.Address_Line1;
            DocsInfo.Address_Line2 = models.Address_Line2;
            DocsInfo.Address_Line3 = models.Address_Line3;
            DocsInfo.Address_Line4 = models.Address_Line4;
            DocsInfo.Company_Based = models.Company_Based;
            DocsInfo.Company_Profile = models.Documentation;
            ContextDatabase.SaveChanges();

            string DocumentLocationURL = string.Format("{0}/Facilitation/DownloadSubscriptions?SubscriptionsId={1}", System.Configuration.ConfigurationManager.AppSettings["url"].ToString(), DocsInfo.SubscriptionsId);

            var UserProfile = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId);

            var UserProfileUserProfileAddressDetails = ContextDatabase.UserProfileUserProfileAddressDetails.FirstOrDefault(f => f.UserProfileId == UserProfile.UserProfileId);

            models.UserProfileId = UserProfile.UserProfileId;
            models.SubscriptionsId = DocsInfo.SubscriptionsId;
            models.DocumentLocationURL = DocumentLocationURL;

            models.AddressLineCity = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCity;
            models.AddressLineState_Province = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineState_Province;
            models.AddressLinePostalCode = UserProfileUserProfileAddressDetails.AddressDetails.AddressLinePostalCode;
            models.AddressLineCountry = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCountry;
            models.CRMAccountId = UserProfileUserProfileAddressDetails.CRMAccountId;

            if (UserProfileUserProfileAddressDetails != null)
            {
                new ServiceDetails.SyncDataInformation().NewAccount(
                DocsInfo.FirstName,
                DocsInfo.Surname,
                DocsInfo.Email_Address,
                UserProfileUserProfileAddressDetails.CRMContactId,
                DocsInfo.CompanyName,
                DocsInfo.Address_Line1,
                DocsInfo.Address_Line2,
                DocsInfo.Address_Line3,
                UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCity,
                UserProfileUserProfileAddressDetails.AddressDetails.AddressLineState_Province,
                UserProfileUserProfileAddressDetails.AddressDetails.AddressLinePostalCode,
                UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCountry,
                UserProfileUserProfileAddressDetails.CRMAccountId,
                DocsInfo.Woman_Owned_Business == "Yes" ? true : false,
                10,
                DocsInfo.Planning_to_Export == "Yes" ? true : false,
                DocsInfo.Company_Based == "Yes" ? true : false,
                DocsInfo.Which_Region,
                DocsInfo.Which_Region,
                DocsInfo.Which_Region,
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
                UserProfile.UserProfileId,
                DocumentLocationURL);
            }
            return models;
        }

        public AssessmentModel SaveAssessment(AssessmentModel models)
        {
            var DocsInfo = ContextDatabase.Assessment.FirstOrDefault(f => f.SubscriptionsId == models.SubscriptionsId);
            if (DocsInfo == null)
            {
                DocsInfo = new Assessment();
                ContextDatabase.Assessment.Add(DocsInfo);

                DocsInfo.CreatedDate = System.DateTime.Now;
                DocsInfo.CreatedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
            }

            DocsInfo.SubscriptionsId = models.SubscriptionsId;
            DocsInfo.AssessmentId = models.AssessmentId;
            DocsInfo.Exporter_Region = models.Exporter_Region;
            DocsInfo.Exporter_Country = models.Exporter_Country;
            DocsInfo.Industry_Sector = models.Industry_Sector;
            DocsInfo.Export_products = models.Export_products;
            DocsInfo.Export_markets = models.Export_markets;
            DocsInfo.Companys_annual_turnover = models.Companys_annual_turnover;
            DocsInfo.Number_of_years_you_have_exported = models.Number_of_years_you_have_exported;

            DocsInfo.Specify_Company_Location = string.IsNullOrEmpty(models.Specify_Company_Location) ? "None" : models.Specify_Company_Location;
            DocsInfo.Country_Expand = models.Country_Expand;
            DocsInfo.Regional_export_markets_future_expand = models.Regional_export_markets_future_expand;

            DocsInfo.Special_technical_support = models.Special_technical_support;

            DocsInfo.Government_export_assistance = models.Government_export_assistance;
            DocsInfo.specify_location_Assistance = models.specify_location_Assistance;
            DocsInfo.chkExporter_Education_Seminar = models.chkExporter_Education_Seminar;
            DocsInfo.chkParticipate_at_international_Trade = models.chkParticipate_at_international_Trade;
            DocsInfo.chkMarket_Research = models.chkMarket_Research;
            DocsInfo.chkGuidance_on_export = models.chkGuidance_on_export;
            DocsInfo.chkFinancial_Assistance = models.chkFinancial_Assistance;
            DocsInfo.chkConsultation_with_trade_specialist = models.chkConsultation_with_trade_specialist;
            DocsInfo.chkInternational_Trade_Missions = models.chkInternational_Trade_Missions;
            DocsInfo.chkTrade_Leads = models.chkTrade_Leads;

            DocsInfo.chkShipping_Logistics_consulting = models.chkShipping_Logistics_consulting;
            DocsInfo.chkOther = models.chkOther;
            DocsInfo.Additional_Comments = models.Additional_Comments;
            DocsInfo.Challenges_facing_Exporter = models.Challenges_facing_Exporter;
            DocsInfo.Other_issues = models.Other_issues;

            ContextDatabase.SaveChanges();            

            var UserProfile = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId);

            var DocsInfoDocs = ContextDatabase.Subscriptions.FirstOrDefault(f => f.CreatedBy == this.CurrentUser.UserProfileId);
            string DocumentLocationURL = string.Format("{0}/Facilitation/DownloadSubscriptions?SubscriptionsId={1}", System.Configuration.ConfigurationManager.AppSettings["url"].ToString(), DocsInfo.SubscriptionsId);

            var UserProfileUserProfileAddressDetails = ContextDatabase.UserProfileUserProfileAddressDetails.FirstOrDefault(f => f.UserProfileId == UserProfile.UserProfileId);

            if (UserProfileUserProfileAddressDetails != null)
            {
                new ServiceDetails.SyncDataInformation().NewAssessmentDetails(
                    DocsInfoDocs.FirstName,
                    DocsInfoDocs.Surname,
                    DocsInfoDocs.Email_Address,
                    UserProfileUserProfileAddressDetails.CRMContactId,
                    DocsInfoDocs.CompanyName,
                    DocsInfoDocs.Address_Line1,
                    DocsInfoDocs.Address_Line2,
                    DocsInfoDocs.Address_Line3,
                    UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCity,
                    UserProfileUserProfileAddressDetails.AddressDetails.AddressLineState_Province,
                    UserProfileUserProfileAddressDetails.AddressDetails.AddressLinePostalCode,
                    UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCountry,
                    UserProfileUserProfileAddressDetails.CRMAccountId,
                    DocsInfo.Number_of_years_you_have_exported,
                    DocsInfo.Number_of_Company_employees,
                    DocsInfo.Companys_annual_turnover,
                    DocsInfo.Export_products,
                    DocsInfo.Export_markets,
                    DocsInfo.Exporter_Region,
                    DocsInfo.Exporter_Country,
                    DocsInfo.Government_export_assistance == "Yes" ? true : false,
                    DocsInfo.Specify_Company_Location,
                    DocsInfo.Additional_Comments,
                    DocsInfo.Challenges_facing_Exporter,
                    DocsInfo.Other_issues,
                    UserProfile.UserProfileId,
                    DocumentLocationURL,

                    DocsInfo.chkExporter_Education_Seminar,
                    DocsInfo.chkParticipate_at_international_Trade,
                    DocsInfo.chkMarket_Research,
                    DocsInfo.chkGuidance_on_export,
                    DocsInfo.chkFinancial_Assistance,
                    DocsInfo.chkConsultation_with_trade_specialist,
                    DocsInfo.chkInternational_Trade_Missions,
                    DocsInfo.chkTrade_Leads,
                    DocsInfo.chkShipping_Logistics_consulting,
                    DocsInfo.chkOther
                    );
            }

            models.FirstName = DocsInfoDocs.FirstName;
            models.Surname = DocsInfoDocs.Surname;
            models.Email_Address = DocsInfoDocs.Email_Address;

            models.CRMContactId = UserProfileUserProfileAddressDetails.CRMContactId;
            models.AddressLineCity = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCity;
            models.AddressLineState_Province = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineState_Province;
            models.AddressLinePostalCode = UserProfileUserProfileAddressDetails.AddressDetails.AddressLinePostalCode;
            models.AddressLineCountry = UserProfileUserProfileAddressDetails.AddressDetails.AddressLineCountry;
            models.CRMAccountId = UserProfileUserProfileAddressDetails.CRMAccountId;

            models.CompanyName = DocsInfoDocs.CompanyName;
            models.Address_Line1 = DocsInfoDocs.Address_Line1;
            models.Address_Line2 = DocsInfoDocs.Address_Line2;
            models.Address_Line3 = DocsInfoDocs.Address_Line3;

            models.Number_of_years_you_have_exported = DocsInfo.Number_of_years_you_have_exported;
            models.Number_of_Company_employees = DocsInfo.Number_of_Company_employees;
            models.Companys_annual_turnover = DocsInfo.Companys_annual_turnover;
            models.Export_products = DocsInfo.Export_products;
            models.Export_markets = DocsInfo.Export_markets;
            models.Exporter_Region = DocsInfo.Exporter_Region;
            models.Exporter_Country = DocsInfo.Exporter_Country;
            models.Government_export_assistance = DocsInfo.Government_export_assistance == "Yes" ? "Yes" : "No";
            models.Specify_Company_Location = DocsInfo.Specify_Company_Location;
            models.Additional_Comments = DocsInfo.Additional_Comments;
            models.Challenges_facing_Exporter = DocsInfo.Challenges_facing_Exporter;
            models.Other_issues = DocsInfo.Other_issues;
            models.UserProfileId = UserProfile.UserProfileId;
            models.DocumentLocationURL = DocumentLocationURL;

            models.chkExporter_Education_Seminar = DocsInfo.chkExporter_Education_Seminar;
            models.chkParticipate_at_international_Trade = DocsInfo.chkParticipate_at_international_Trade;
            models.chkMarket_Research = DocsInfo.chkMarket_Research;
            models.chkGuidance_on_export = DocsInfo.chkGuidance_on_export;
            models.chkFinancial_Assistance = DocsInfo.chkFinancial_Assistance;
            models.chkConsultation_with_trade_specialist = DocsInfo.chkConsultation_with_trade_specialist;
            models.chkInternational_Trade_Missions = DocsInfo.chkInternational_Trade_Missions;
            models.chkTrade_Leads = DocsInfo.chkTrade_Leads;
            models.chkShipping_Logistics_consulting = DocsInfo.chkShipping_Logistics_consulting;
            models.chkOther = DocsInfo.chkOther;

            return models;
        }

        public DocumentManagementModel GetDocumentTypes()
        {
            DocumentManagementModel model = new DocumentManagementModel();

            model.DocumentManagementList = ContextDatabase.Database.SqlQuery<DocumentManagementModel>(string.Format("{0} {1}", "GetDocumentManagement", this.CurrentUser.UserProfileId)).ToList();

            List<DocumentTypeModel> DocumentTypeList = new List<DocumentTypeModel>();
            var DocumentTypeDetailList = ContextDatabase.DocumentType.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.DocumentType, DocumentTypeModel>(DocumentTypeDetailList, ref DocumentTypeList);
            model.DocumentTypeList = DocumentTypeList.ToList();

            return model;
        }

        public void SaveFacilitationRequest(FacilitationRequestModel model)
        {
            Guid CRMFacilitationRequestId = Guid.Empty;
            var DocsInfo = ContextDatabase.FacilitationRequest.FirstOrDefault(f => f.FacilitationRequestId == model.FacilitationRequestId);
            if (DocsInfo == null)
            {
                DocsInfo = new FacilitationRequest();
                ContextDatabase.FacilitationRequest.Add(DocsInfo);

                DocsInfo.CreatedDate = System.DateTime.Now;
                DocsInfo.CreatedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
                DocsInfo.CRMFacilitationRequestId = CRMFacilitationRequestId;
            }
            else
            {
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
                DocsInfo.CRMFacilitationRequestId = model.CRMFacilitationRequestId;
            }

            DocsInfo.FacilitationId = model.FacilitationId;
            DocsInfo.Comments = model.Comments;
            DocsInfo.NeededAssistance = model.NeededAssistance;            
            DocsInfo.IsDeleted = false;
            ContextDatabase.SaveChanges();

            var UserProfile = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId);           

            var FacilitationInfo = ContextDatabase.Facilitation.FirstOrDefault(f => f.FacilitationId == model.FacilitationId);

            var UserProfileUserProfileAddressDetails = ContextDatabase.UserProfileUserProfileAddressDetails.FirstOrDefault(f => f.UserProfileId == UserProfile.UserProfileId);

            if (UserProfileUserProfileAddressDetails != null)
            {
                new ServiceDetails.SyncDataInformation().NewFacilitationRequest(
                    DocsInfo.Comments, 
                    DocsInfo.NeededAssistance, 
                    UserProfileUserProfileAddressDetails.CRMAccountId,
                    FacilitationInfo.CRMOpportunityId != null && FacilitationInfo.CRMOpportunityId != Guid.Empty ? FacilitationInfo.CRMOpportunityId.Value :  FacilitationInfo.CRMLeadId.Value,
                    UserProfileUserProfileAddressDetails.CRMContactId,
                    DocsInfo.FacilitationRequestId);
            }
        }

        public int DeleteFacilitationRequest(int FacilitationRequestId)
        {
            var DocumentManagement = ContextDatabase.FacilitationRequest.FirstOrDefault(f => f.FacilitationRequestId == FacilitationRequestId);
            DocumentManagement.IsDeleted = true;
            ContextDatabase.SaveChanges();
            return DocumentManagement.FacilitationRequestId;
        }

        public DocumentManagementModel SaveDocument(DocumentManagementModel model)
        {
            string DocumentLocationURLs = string.Format("{0}/Facilitation/DownloadDocuments?DocumentManagementId=", System.Configuration.ConfigurationManager.AppSettings["url"].ToString());

            Guid CRMDocumentManagementId = Guid.Empty;
            var DocsInfo = ContextDatabase.DocumentManagement.FirstOrDefault(f => f.DocumentManagementId == model.DocumentManagementId);
            if (DocsInfo == null)
            {
                DocsInfo = new DocumentManagement();
                ContextDatabase.DocumentManagement.Add(DocsInfo);

                DocsInfo.CreatedDate = System.DateTime.Now;
                DocsInfo.CreatedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
                DocsInfo.ModifiedDate = System.DateTime.Now;
                DocsInfo.CRMDocumentManagementId = CRMDocumentManagementId;
            }
            else
            {
                DocsInfo.ModifiedBy = this.CurrentUser.UserProfileId;
                DocsInfo.ModifiedDate = System.DateTime.Now;
                DocsInfo.CRMDocumentManagementId = DocsInfo.CRMDocumentManagementId;
            }
            DocsInfo.FacilitationId = model.FacilitationId;
            DocsInfo.DocumentName = model.DocumentName;
            DocsInfo.DocumentDescription = model.Description;
            DocsInfo.DocumentImage = model.DocumentImage;
            DocsInfo.DocumentTypeId = model.DocumentTypeId;
            DocsInfo.ContentType = model.ContentType;
            DocsInfo.DocumentPath = DocumentLocationURLs; 
            DocsInfo.IsDeleted = false;        
            ContextDatabase.SaveChanges();

            string DocumentLocationURL = string.Format("{0}/Facilitation/DownloadDocuments?DocumentManagementId={1}", System.Configuration.ConfigurationManager.AppSettings["url"].ToString(), DocsInfo.DocumentManagementId) ;
            
            var UserProfile = ContextDatabase.UserProfile.FirstOrDefault(f => f.UserProfileId == this.CurrentUser.UserProfileId);

            var FacilitationInfo = ContextDatabase.Facilitation.FirstOrDefault(f => f.FacilitationId == model.FacilitationId);

            var DocumentTypeInfo = ContextDatabase.DocumentType.FirstOrDefault(f => f.DocumentTypeId == model.DocumentTypeId);            

            var UserProfileUserProfileAddressDetails = ContextDatabase.UserProfileUserProfileAddressDetails.FirstOrDefault(f => f.UserProfileId == UserProfile.UserProfileId);

            if (UserProfileUserProfileAddressDetails != null)
            {
                new ServiceDetails.SyncDataInformation().NewFacilitationDocument(
                    DocsInfo.DocumentName,
                    DocsInfo.DocumentDescription,
                    DocumentLocationURL,
                    DocsInfo.CRMDocumentManagementId.Value,
                    FacilitationInfo.CRMOpportunityId.Value,
                    FacilitationInfo.CRMLeadId.Value,
                    UserProfileUserProfileAddressDetails.CRMContactId,
                    DocsInfo.DocumentManagementId,
                    DocumentTypeInfo.Description);
            }
            
            model.DocumentLocationURLs = DocumentLocationURL;            
            model.CRMContactId = UserProfileUserProfileAddressDetails.CRMContactId;
            model.CRMAccountId = UserProfileUserProfileAddressDetails.CRMAccountId;
            model.CRMLeadId = FacilitationInfo.CRMLeadId.Value;

            return model;
        }
        #endregion        
    }
}
