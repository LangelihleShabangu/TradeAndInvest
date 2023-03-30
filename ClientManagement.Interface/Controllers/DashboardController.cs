using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel.DashBoard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class DashboardController :  BaseController<DashboardData>
    {
        public ActionResult DynamicReportDetails()
        {
            return Json(BusinessObject.DynamicReportDetails(), JsonRequestBehavior.AllowGet);            
        }

        [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
        public ActionResult Index()
        {
            DashBoardDomainModel dashBoardDomainModel = new DashBoardDomainModel();
            dashBoardDomainModel = BusinessObject.GetDashBoardDetails(); 
            dashBoardDomainModel.Notes = BusinessObject.GetProfileDetails();
            return View(dashBoardDomainModel);
        }       

        private IEnumerable<Company> CreateCompaniesList()
        {
            List<Company> companies = new List<Company>();

            for (int i = 1; i <= DateTime.Now.Month; i++)
            {
                Company company = new Company() { Expense = 20, Salary = 30, Year = new DateTime(DateTime.Now.Year, i, 1).ToString("yyyy/MM") };
                companies.Add(company);
            }

            return companies;
        }        
    }
}
