using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using Models;
using Models.Information;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class GrantController : BaseController<GrantLogic>
    {
        public ActionResult Index()
        {
            var models = new FunderModel();
            return View(BusinessObject.GetFunderList());
        }
        
        //public ActionResult GrantAsserts(int GrantId)
        //{
        //    var models = new GrantModel();
        //    return PartialView("_AddGrantAsserts", BusinessObject.GetGrantAssertList(GrantId));
        //}

        public ActionResult SaveFarmerFunders(int FunderId)
        {
            var models = new GrantModel();
            return PartialView("_AddGrantFunder", BusinessObject.GetFunderDetails(FunderId));
        }          

        public ActionResult AddGrantFunder(int FunderId)
        {
            var models = new GrantModel();
            return PartialView("_AddGrantFunder", BusinessObject.GetFunderDetails(FunderId));
        }   

        public ActionResult GrantDetails(int FunderId)
        {
            var models = new GrantModel();
            return PartialView("_AddGrant", BusinessObject.GetFunderbyId(FunderId));
        }         

        public ActionResult GetFunder(string CompanyName)
        {
            var models = new GrantModel();
            return PartialView("_AddGrant", BusinessObject.GetFunder(CompanyName));
        }        

        public ActionResult AddGrant()
        {
            var models = new GrantModel();
            return PartialView("_AddGrant", models);
        }      

        [HttpGet]
        public ActionResult SaveFunderGrant(string Description, DateTime StartDate, DateTime EndDate,int FunderId, decimal TotalAmount)
        {
            var models = new FunderModel();
            models.Description = Description;
            models.StartDate = StartDate;
            models.EndDate = EndDate;
            models.FunderId = FunderId;
            models.TotalAmount = TotalAmount;

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFunderGrant(models);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("Grant");
            }
        }
        
        [HttpGet]
        public ActionResult SaveFarmerFunder(string Description, DateTime StartDate, DateTime EndDate, int FarmerId, decimal Amount, int FunderId)
        {
            var models = new FunderModel();
            models.Description = Description;
            models.StartDate = StartDate;
            models.EndDate = EndDate;
            models.FunderId = FunderId;
            models.FarmerId = FarmerId;
            models.Amount = Amount;

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFarmerFunder(models);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("Grant");
            }
        }

        [HttpGet]
        public ActionResult SaveFunder(string CompanyName, string ContactNumber, int GrantId)
        {
            var models = new FunderModel();
            models.CompanyName = CompanyName;
            models.ContactNumber = ContactNumber;
            models.GrantId = GrantId;

            if (ModelState.IsValid)
            {
                BusinessObject.SaveFunder(models);
                return RedirectToAction("Index");
            }
            else
            {
                return PartialView("Grant");
            }
        }
    }
}
