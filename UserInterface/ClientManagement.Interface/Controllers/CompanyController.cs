using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using ClientManagement.DomainModel.Config;
using System;
using System.Transactions;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class CompanyController : BaseController<Administration>
    {
        public ActionResult Index()
        {
            CompanyDomainModel CompanyFilterModel = new CompanyDomainModel();
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
            {
                CompanyFilterModel = BusinessObject.GetCompanyDetails();
                transactionScope.Complete();
            }
            return View(CompanyFilterModel);
        }

        public ActionResult Edit(string submitbtn, int companyId, int municipalityId, int locationId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    CompanyDomainModel CompanyFilterModel = new CompanyDomainModel();
                    using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
                    {
                        CompanyFilterModel = BusinessObject.GetCompanyDetailsData(companyId);

                        using (var locationLogic = GetBusinessObject<LocationLogic>())
                        {
                            if (municipalityId == 0)
                            {
                                municipalityId = CompanyFilterModel.MunicipalityId;
                            }

                            var munic = locationLogic.GetMunicipality(municipalityId);
                            var region = locationLogic.GetRegion(munic.RegionId);

                            CompanyFilterModel.Municipality = munic.Name;
                            CompanyFilterModel.Region = region.Name;

                            var province = locationLogic.GetProvince(region.ProvinceId);
                            CompanyFilterModel.Province = province.Name;
                        }

                        transactionScope.Complete();
                    }
                    return View(CompanyFilterModel);

                case "Remove":
                    using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
                    {
                        BusinessObject.DeleteCompany(companyId);
                        transactionScope.Complete();
                    }
                    return RedirectToAction("Index");
                case "Add":
                    {
                        CompanyDomainModel CompanyDomainModel = new CompanyDomainModel();
                        CompanyDomainModel.MunicipalityId = municipalityId;
                        using (var locationLogic = GetBusinessObject<LocationLogic>())
                        {
                            var munic = locationLogic.GetMunicipality(municipalityId);
                            var region = locationLogic.GetRegion(munic.RegionId);

                            CompanyDomainModel.Municipality = munic.Name;
                            CompanyDomainModel.Region = region.Name;

                            var province = locationLogic.GetProvince(region.ProvinceId);
                            CompanyDomainModel.Province = province.Name;
                        }
                        return View("Edit", CompanyDomainModel);
                    }

                case "AddArea":

                    using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
                    {
                        BusinessObject.SaveCompanyServiceArea(new CompanyServiceAreaDomainModel()
                        {
                            CompanyId = companyId,
                            MunicipalityId = locationId,
                        }); //todo
                        transactionScope.Complete();
                    }

                    return RedirectToAction("Edit", new { submitbtn = "Edit", CompanyId = companyId, municipalityId = municipalityId, locationId = locationId, tabId = "panel_tab2" });

            }
            return View();
        }

       
        public ActionResult SaveCompany(int CompanyId, int MunicipalityId,string RegisteredName,string CompanyRegistrationNumber, string ExpiaryDate,
             string EmailAddress, string ContactNumber, string CellphoneNumber, string Website )
        {
            CompanyDomainModel Company = new CompanyDomainModel();
            Company.MunicipalityId = MunicipalityId;
            Company.RegisteredName = RegisteredName;
            Company.CompanyRegistrationNumber = CompanyRegistrationNumber;
            Company.ExpiaryDate = Convert.ToDateTime(ExpiaryDate);
            Company.EmailAddress = EmailAddress;
            Company.ContactNumber = ContactNumber;
            Company.CellphoneNumber = CellphoneNumber;
            Company.Website = Website;

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
            {
                BusinessObject.SaveCompany(Company);
                transactionScope.Complete();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult CreateEditCompany(CompanyDomainModel Company)
        {
            if (ModelState.IsValid)
            {

                using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
                {
                    BusinessObject.SaveCompany(Company);
                    transactionScope.Complete();
                }

                return RedirectToAction("Index");
            }
            else
                return PartialView("Edit", Company);
        }

        public ActionResult CompanyServiceAreaIndex()
        {
            CompanyDomainModel CompanyFilterModel = new CompanyDomainModel();
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
            {
                CompanyFilterModel = BusinessObject.GetCompanyDetails();
                transactionScope.Complete();
            }
            return View(CompanyFilterModel);
        }

        public ActionResult DeleteCompanyServiceArea(int CompanyServiceAreaId, int CompanyId, int municipalityId, int locationId)
        {
            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, Config.DefaultTransactionOptions))
            {
                BusinessObject.DeleteCompanyServiceArea(CompanyServiceAreaId);
                transactionScope.Complete();
            }
            return RedirectToAction("Edit", new { submitbtn = "Edit", CompanyId = CompanyId, municipalityId = municipalityId, locationId = locationId, tabId = "panel_tab2" });
        }
    }
}
