using System.Linq;
using ClientManagement.DomainModel;
using System.Collections.Generic;
using ClientManagement.DataObject;
using Techhill.Framework.BusinessProcess;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using InvestorManagement.DomainModel;

namespace ClientManagement.BusinessLogic
{
    public class InvestorLogic : BaseBusinessObject<ClientManagement.DataObject.ClientManagementContext>
    {
        public InvestorLogic(string connection)
            : base(connection)
        {
        } 

        public InvestorModel CreateInvestorModelData(int InvestorId)
        {
            InvestorModel model = new InvestorModel();

            model = ContextDatabase.Database.SqlQuery<InvestorModel>("usp_GetInvestorInformationTrading").FirstOrDefault(f => f.InvestorId == InvestorId);

            List<CompanyDomainModel> CompanysList = new List<CompanyDomainModel>();
            var CompanysDetailList = ContextDatabase.Company.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Company, CompanyDomainModel>(CompanysDetailList, ref CompanysList);

            model.CompanyList = CompanysList.ToList();

            List<StatusModel> Statuslst = new List<StatusModel>();
            var ClientStatusDetailList = ContextDatabase.Status.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Status, StatusModel>(ClientStatusDetailList, ref Statuslst);
            model.StatusList = Statuslst.ToList();
           
            List<BankModel> BanksList = new List<BankModel>();
            var BanksDetailList = ContextDatabase.Bank.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Bank, BankModel>(BanksDetailList, ref BanksList);

            model.BankingDetailsInfo.BankList = BanksList.ToList();

            List<BankAccountTypeModel> BankAccountTypesList = new List<BankAccountTypeModel>();
            var BankAccountTypesDetailList = ContextDatabase.BankAccountType.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.BankAccountType, BankAccountTypeModel>(BankAccountTypesDetailList, ref BankAccountTypesList);

            model.BankingDetailsInfo.BankAccountTypeList = BankAccountTypesList.ToList();

            List<EntityModel> EntitysList = new List<EntityModel>();
            var EntitysDetailList = ContextDatabase.Entity.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Entity, EntityModel>(EntitysDetailList, ref EntitysList);

            return model;
        }

        public InvestorModel GetInvestorList(string Description)
        {
            InvestorModel model = new InvestorModel();
            model = ContextDatabase.Database.SqlQuery<InvestorModel>("usp_GetInvestorInformation").FirstOrDefault(f => f.Description == Description);

            List<InvestmentIndustriesModel> InvestmentIndustriesList = new List<InvestmentIndustriesModel>();
            var CompanysDetailList = ContextDatabase.InvestmentIndustries.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.InvestmentIndustries, InvestmentIndustriesModel>(CompanysDetailList, ref InvestmentIndustriesList);

            model.InvestmentIndustriesList = InvestmentIndustriesList.ToList();
            return model;
        }

        public InvestorModel GetInvestorInfo(int InvestorId)
        {
            InvestorModel model = new InvestorModel();
            model = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationSingle", InvestorId)).FirstOrDefault();

            List<CompanyDomainModel> CompanysList = new List<CompanyDomainModel>();
            var CompanysDetailList = ContextDatabase.Company.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Company, CompanyDomainModel>(CompanysDetailList, ref CompanysList);

            model.CompanyList = CompanysList.ToList();
            return model;
        }

        public InvestorModel GetInvestorAssertList(int InvestorId)
        {
            InvestorModel model = new InvestorModel();
            model = ContextDatabase.Database.SqlQuery<InvestorModel>("usp_GetInvestorAsserts").FirstOrDefault(f => f.InvestorId == InvestorId);

            return model;
        }

        public InvestorModel GetInvestorFinancialList(int InvestorId)
        {
            InvestorModel model = new InvestorModel();
            model = ContextDatabase.Database.SqlQuery<InvestorModel>("usp_GetInvestorFinancialPerformance").FirstOrDefault(f => f.InvestorId == InvestorId);

            return model;
        }

        public void DeleteEmployeeDocuments(int EmployeeDocumentsId)
        {
            var LeaveEntitlement = ContextDatabase.EmployeeDocuments.FirstOrDefault(f => f.EmployeeDocumentsId == EmployeeDocumentsId);
            LeaveEntitlement.IsDeleted = true;
            ContextDatabase.SaveChanges();
        }

        public void DeleteEmployeeLeave(int EmployeeLeaveId)
        {
            var LeaveEntitlement = ContextDatabase.EmployeeLeave.FirstOrDefault(f => f.EmployeeLeaveId == EmployeeLeaveId);
            LeaveEntitlement.IsDeleted = true;
            ContextDatabase.SaveChanges();
        }

        public void ApproveDeclineLeave(bool IsApproveorDecline, int EmployeeLeaveId)
        {
            var LeaveEntitlement = ContextDatabase.EmployeeLeave.FirstOrDefault(f => f.EmployeeLeaveId == EmployeeLeaveId);
            LeaveEntitlement.IsApproved = IsApproveorDecline;
            ContextDatabase.SaveChanges();
        }

        public EmployeeDocumentsModel GetDocument(int EmployeeDocumentsId)
        {
            EmployeeDocumentsModel model = new EmployeeDocumentsModel();
            var ProductsDetailList = ContextDatabase.EmployeeDocuments.FirstOrDefault(f => !f.IsDeleted && f.EmployeeDocumentsId == EmployeeDocumentsId);
            Mapper.MapObjects(ProductsDetailList, ref model);
            return model;
        }

        public InvestorModel GetInvestorDocumentsList(int InvestorId)
        {
            InvestorModel model = new InvestorModel();
            model = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationSingle", InvestorId)).FirstOrDefault();

            List<EmployeeDocumentsModel> EmployeeDocumentssList = new List<EmployeeDocumentsModel>();
            var EmployeeDocumentssDetailList = ContextDatabase.EmployeeDocuments.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.EmployeeDocuments, EmployeeDocumentsModel>(EmployeeDocumentssDetailList, ref EmployeeDocumentssList);
            model.EmployeeDocumentsList = EmployeeDocumentssList.ToList();

            return model;
        }

        public InvestorModel GetInvestorLeaveList(int InvestorId)
        {
            InvestorModel model = new InvestorModel();
            model = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationSingle", InvestorId)).FirstOrDefault();

            model.EmployeeLeaveList = ContextDatabase.Database.SqlQuery<EmployeeLeaveModel>(string.Format("{0} {1}", "usp_GetEmployeeLeaveSingle", InvestorId)).ToList();

            model.EmployeeLeaveDisplayList = ContextDatabase.Database.SqlQuery<EmployeeLeaveModel>("usp_GetLeaveInformationDisplay").ToList();
            
            List<LeaveTypeModel> LeaveTypesList = new List<LeaveTypeModel>();
            var LeaveTypesDetailList = ContextDatabase.LeaveType.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.LeaveType, LeaveTypeModel>(LeaveTypesDetailList, ref LeaveTypesList);
            model.LeaveTypeList = LeaveTypesList.ToList();

            List<StatusModel> StatussList = new List<StatusModel>();
            var StatussDetailList = ContextDatabase.Status.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Status, StatusModel>(StatussDetailList, ref StatussList);
            model.StatusList = StatussList.ToList();

            return model;
        }

        public void SaveDocument(EmployeeDocumentsModel model)
        {
            var EmployeeLeaveInfo = ContextDatabase.EmployeeDocuments.FirstOrDefault(f => f.EmployeeDocumentsId == model.EmployeeDocumentsId);
            if (EmployeeLeaveInfo == null)
            {
                EmployeeLeaveInfo = new EmployeeDocuments();
                ContextDatabase.EmployeeDocuments.Add(EmployeeLeaveInfo);

                EmployeeLeaveInfo.CreatedDate = System.DateTime.Now;
                EmployeeLeaveInfo.CreatedBy = this.CurrentUser.UserId;
                EmployeeLeaveInfo.ModifiedBy = this.CurrentUser.UserId;
                EmployeeLeaveInfo.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                EmployeeLeaveInfo.ModifiedBy = this.CurrentUser.UserId;
                EmployeeLeaveInfo.ModifiedDate = System.DateTime.Now;
            }
            //EmployeeLeaveInfo.InvestorId = model.InvestorId;
            EmployeeLeaveInfo.DocumentName = model.DocumentName;
            EmployeeLeaveInfo.DocumentDescription = model.DocumentDescription;
            EmployeeLeaveInfo.DocumentImage = model.DocumentImage;
            EmployeeLeaveInfo.ContentType = model.ContentType;
            EmployeeLeaveInfo.IsDeleted = false;

            ContextDatabase.SaveChanges();            
        }
        
        public int CreateEmployeeLeave(EmployeeLeaveModel model)
        {
            var EmployeeLeaveInfo = ContextDatabase.EmployeeLeave.FirstOrDefault(f => f.EmployeeLeaveId == model.EmployeeLeaveId);
            if (EmployeeLeaveInfo == null)
            {
                EmployeeLeaveInfo = new EmployeeLeave();
                ContextDatabase.EmployeeLeave.Add(EmployeeLeaveInfo);

                EmployeeLeaveInfo.CreatedDate = System.DateTime.Now;
                EmployeeLeaveInfo.CreatedBy = this.CurrentUser.UserId;
                EmployeeLeaveInfo.ModifiedBy = this.CurrentUser.UserId;
                EmployeeLeaveInfo.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                EmployeeLeaveInfo.ModifiedBy = this.CurrentUser.UserId;
                EmployeeLeaveInfo.ModifiedDate = System.DateTime.Now;
            }
            //EmployeeLeaveInfo.InvestorId = model.InvestorId;
            EmployeeLeaveInfo.LeaveTypeId = model.LeaveTypeId;
            EmployeeLeaveInfo.StartDate = model.StartDate;
            EmployeeLeaveInfo.EndDate = model.EndDate;
            EmployeeLeaveInfo.IsApproved = false;
            EmployeeLeaveInfo.Days = model.Days;
            EmployeeLeaveInfo.IsDeleted = false;

            ContextDatabase.SaveChanges();
            return EmployeeLeaveInfo.EmployeeLeaveId;
        }


        public InvestorModel GetInvestorDetails(int InvestorId)
        {
            InvestorModel model = new InvestorModel();

            var ListModel = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationSingle", InvestorId));
            foreach (var item in ListModel)
            {
                Mapper.MapObjects(item, ref model);
            }            

            var EmploymentDetails = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetEmploymentDetailsSingle", InvestorId)).FirstOrDefault(f => f.InvestorId == InvestorId);

            if (EmploymentDetails != null)
            {
                model.LineManager = EmploymentDetails.LineManager;
                model.EmploymentDetailsId = EmploymentDetails.EmploymentDetailsId;
                model.InvestorId = EmploymentDetails.InvestorId;
                model.StatusId = EmploymentDetails.StatusId;
                model.DepartmentId = EmploymentDetails.DepartmentId;
                model.PositionId = EmploymentDetails.PositionId;
                model.LineManager = EmploymentDetails.LineManager;
                model.EmploymentDate = EmploymentDetails.EmploymentDate;
                model.TaxNumber = EmploymentDetails.TaxNumber;
                model.Salary = EmploymentDetails.Salary;
            }           

            List<DepartmentModel> DepartmentsList = new List<DepartmentModel>();
            var DepartmentsDetailList = ContextDatabase.Department.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Department, DepartmentModel>(DepartmentsDetailList, ref DepartmentsList);
            model.DepartmentList = DepartmentsList.ToList();

            List<PositionModel> PositionsList = new List<PositionModel>();
            var PositionsDetailList = ContextDatabase.Position.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Position, PositionModel>(PositionsDetailList, ref PositionsList);
            model.PositionList = PositionsList.ToList();

            List<StatusModel> StatussList = new List<StatusModel>();
            var StatussDetailList = ContextDatabase.Status.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Status, StatusModel>(StatussDetailList, ref StatussList);
            model.StatusList = StatussList.ToList();

            model.InvestorList = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationFilter", this.CurrentUser.UserId)).ToList();

            return model;
        }

        public InvestorModel GetInvestorLeave(int InvestorId)
        {
            InvestorModel model = new InvestorModel();

            var ListModel = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationSingle", InvestorId));
            foreach (var item in ListModel)
            {
                Mapper.MapObjects(item, ref model);
            }

            model.EmployeeLeaveList = ContextDatabase.Database.SqlQuery<EmployeeLeaveModel>(string.Format("{0} {1}", "usp_GetEmployeeLeaveSingle", InvestorId)).ToList();
          
            return model;
        }

        public InvestorModel GetInvestorInformation(int InvestorId)
        {
            InvestorModel model = new InvestorModel();

            var ListModel = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationSingle", InvestorId));
            foreach(var item in ListModel)
            {
                Mapper.MapObjects(item, ref model);            
            }

            List<InvestmentIndustriesModel> InvestmentIndustriesList = new List<InvestmentIndustriesModel>();
            var CompanysDetailList = ContextDatabase.InvestmentIndustries.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.InvestmentIndustries, InvestmentIndustriesModel>(CompanysDetailList, ref InvestmentIndustriesList);

            model.InvestmentIndustriesList = InvestmentIndustriesList.ToList();

            return model;
        }

        public InvestorModel GetInvestorList(int InvestorId)
        {
            InvestorModel model = new InvestorModel();

            var ListModel = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationSingle", InvestorId)).ToList();
            foreach (var item in ListModel)
            {
                Mapper.MapObjects(item, ref model);
            }

            model.EmployeeLeaveList = ContextDatabase.Database.SqlQuery<EmployeeLeaveModel>(string.Format("{0} {1}","usp_GetEmployeeLeaveSingle", InvestorId)).ToList();

            var EmploymentDetails = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}","usp_GetEmploymentDetailsSingle", InvestorId)).FirstOrDefault(f => f.InvestorId == InvestorId);

            if (EmploymentDetails != null)
            {
                model.LineManager = EmploymentDetails.LineManager;
                model.EmploymentDetailsId = EmploymentDetails.EmploymentDetailsId;
                model.InvestorId = EmploymentDetails.InvestorId;
                model.StatusId = EmploymentDetails.StatusId;
                model.DepartmentId = EmploymentDetails.DepartmentId;
                model.PositionId = EmploymentDetails.PositionId;
                model.LineManager = EmploymentDetails.LineManager;
                model.EmploymentDate = EmploymentDetails.EmploymentDate;
                model.TaxNumber = EmploymentDetails.TaxNumber;
                model.Salary = EmploymentDetails.Salary; 
            }

            List<CompanyDomainModel> CompanysList = new List<CompanyDomainModel>();
            var CompanysDetailList = ContextDatabase.Company.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Company, CompanyDomainModel>(CompanysDetailList, ref CompanysList);

            model.CompanyList = CompanysList.ToList();

            List<DepartmentModel> DepartmentsList = new List<DepartmentModel>();
            var DepartmentsDetailList = ContextDatabase.Department.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Department, DepartmentModel>(DepartmentsDetailList, ref DepartmentsList);
            model.DepartmentList = DepartmentsList.ToList();

            List<PositionModel> PositionsList = new List<PositionModel>();
            var PositionsDetailList = ContextDatabase.Position.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Position, PositionModel>(PositionsDetailList, ref PositionsList);
            model.PositionList = PositionsList.ToList();

            List<StatusModel> StatussList = new List<StatusModel>();
            var StatussDetailList = ContextDatabase.Status.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.Status, StatusModel>(StatussDetailList, ref StatussList);
            model.StatusList = StatussList.ToList();

            //model.InvestorList = ContextDatabase.Database.SqlQuery<InvestorModel>("usp_GetInvestorInformation").ToList();

            return model;
        }

        public InvestorModel GetInvestorList()
        {
            InvestorModel model = new InvestorModel();
            model.InvestorList = ContextDatabase.Database.SqlQuery<InvestorModel>(string.Format("{0} {1}", "usp_GetInvestorInformationFilter", this.CurrentUser.UserId)).ToList();

            List<InvestmentIndustriesModel> InvestmentIndustriesList = new List<InvestmentIndustriesModel>();
            var CompanysDetailList = ContextDatabase.InvestmentIndustries.Where(f => !f.IsDeleted).ToList();
            Mapper.MapObjects<DataObject.InvestmentIndustries, InvestmentIndustriesModel>(CompanysDetailList, ref InvestmentIndustriesList);

            model.InvestmentIndustriesList = InvestmentIndustriesList.ToList();

            return model;
        }
        
        public void SaveInvestorImage(InvestorModel model)
        {
            var InvestorData = ContextDatabase.Investor.FirstOrDefault(f => !f.IsDeleted && f.InvestorId == model.InvestorId);
            if (InvestorData == null)
            {
                InvestorData = new Investor();
                ContextDatabase.Investor.Add(InvestorData);

                InvestorData.CreatedDate = System.DateTime.Now;
                InvestorData.CreatedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }            
            ContextDatabase.SaveChanges();
        }

        public void UpdateInvestor(InvestorModel model)
        {
            var InvestorData = ContextDatabase.Investor.FirstOrDefault(f => !f.IsDeleted && f.InvestorId == model.InvestorId);
            if (InvestorData == null)
            {
                InvestorData = new Investor();
                ContextDatabase.Investor.Add(InvestorData);

                InvestorData.CreatedDate = System.DateTime.Now;
                InvestorData.CreatedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }

            model.ContactInformationDetails.ContactInformationId = model.ContactInformationId;
            model.ContactInformationDetails.EmailAddress = model.Email ?? "None";
            model.ContactInformationDetails.FirstName = model.FirstName ?? "None";
            model.ContactInformationDetails.PhoneNumber = model.PhoneNumber ?? "None";
            model.ContactInformationDetails.CellNumber = model.CellNumber ?? "None";
            model.ContactInformationDetails.Surname = model.Surname ?? "None";
            model.ContactInformationDetails.IdNumber = model.IdNumber ?? "None";
            InvestorData.InvestmentIndustriesId = model.InvestmentIndustriesId == 0 ? 1 : model.InvestmentIndustriesId;
            InvestorData.Description = model.Description ?? "None";

            if (InvestorData.ContactInformation != null)
            {
                InvestorData.ContactInformation.Address.AddressId = model.AddressId;
                model.ContactInformationDetails.AddressId = model.AddressId;
                model.ContactInformationDetails.AddressDetails.AddressLine1 = model.AddressLine1;
                model.ContactInformationDetails.AddressDetails.AddressLine2 = model.AddressLine2;
                model.ContactInformationDetails.AddressDetails.AddressLine3 = model.AddressLine3;
                model.ContactInformationDetails.AddressDetails.AreaId = 1;
                model.ContactInformationDetails.AddressDetails.AddressLine4 = model.AddressLine4;
            }

            InvestorData.ContactInformationId = new ContactInformationLogic(ConnectionString).SaveContactInformation(model.ContactInformationDetails);
            InvestorData.IsDeleted = false;
            ContextDatabase.SaveChanges();
        }

        public void UpdateEmployeeDetails(InvestorModel model)
        {
            var InvestorData = ContextDatabase.EmploymentDetails.FirstOrDefault(f => !f.IsDeleted && f.EmploymentDetailsId == model.EmploymentDetailsId);
            if (InvestorData == null)
            {
                InvestorData = new EmploymentDetails();
                ContextDatabase.EmploymentDetails.Add(InvestorData);

                InvestorData.CreatedDate = System.DateTime.Now;
                InvestorData.CreatedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }

            InvestorData.StatusId = model.StatusId;
            InvestorData.DepartmentId = model.DepartmentId;
            InvestorData.PositionId = model.PositionId;
            //InvestorData.InvestorId = model.InvestorId;
            InvestorData.LineManager = model.LineManager;
            InvestorData.Description = string.IsNullOrEmpty(model.Description) ? "None" : model.Description;
            InvestorData.EmploymentDate = model.EmploymentDate;
            InvestorData.TaxNumber = model.TaxNumber;
            InvestorData.Salary = model.Salary;
            InvestorData.IsDeleted = false;
            ContextDatabase.SaveChanges();
        }

        public void SaveInvestor(InvestorModel model)
        {
            var InvestorData = ContextDatabase.Investor.FirstOrDefault(f => !f.IsDeleted && f.InvestorId == model.InvestorId);
            if (InvestorData == null)
            {
                InvestorData = new Investor();
                ContextDatabase.Investor.Add(InvestorData);

                InvestorData.CreatedDate = System.DateTime.Now;
                InvestorData.CreatedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }
            else
            {
                InvestorData.ModifiedBy = this.CurrentUser.UserId;
                InvestorData.ModifiedDate = System.DateTime.Now;
            }

            model.ContactInformationDetails.EmailAddress = model.Email;
            model.ContactInformationDetails.FirstName = model.FirstName;
            model.ContactInformationDetails.Surname = model.Surname;
            model.ContactInformationDetails.IdNumber = model.IdNumber;
            
            InvestorData.Description = string.IsNullOrEmpty(model.Description) ? "None" : model.Description;
            InvestorData.StatusId = model.StatusId == 0 ? 1 : model.StatusId;
            InvestorData.InvestmentIndustriesId = model.CompanyId == 0 ? 1 : model.InvestmentIndustriesId;

            if (InvestorData.ContactInformation != null && InvestorData.ContactInformation.Address != null)
            {
                InvestorData.ContactInformation.Address.AddressId = model.AddressDetails.AddressId;
                InvestorData.ContactInformation.Address.AddressLine1 = model.AddressDetails.AddressLine1;
                InvestorData.ContactInformation.Address.AddressLine2 = model.AddressDetails.AddressLine2;
                InvestorData.ContactInformation.Address.AddressLine3 = model.AddressDetails.AddressLine3;
                InvestorData.ContactInformation.Address.AreaId = model.AddressDetails.AreaId;
                InvestorData.ContactInformation.Address.AddressLine4 = model.AddressDetails.AddressLine4;
            }

            InvestorData.ContactInformationId = new ContactInformationLogic(ConnectionString).SaveContactInformation(model.ContactInformationDetails);
            InvestorData.IsDeleted = false;
            ContextDatabase.SaveChanges();
        }       
    }
}
