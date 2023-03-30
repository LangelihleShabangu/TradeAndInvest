using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class BusinessTypeController : BaseController<AdminLogic> 
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetBusinessTypeDetail());
        }

        public ActionResult CreateBusinessType()
        {
            return View("BusinessTypeCapture", new BusinessTypeModel());
        }

        [HttpPost]
        public ActionResult CreateEditBusinessType(string submitbtn, int BusinessTypeId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    BusinessTypeModel modelfilter = new BusinessTypeModel();
                    modelfilter = BusinessObject.GetBusinessTypeDetail(BusinessTypeId);
                    return PartialView("BusinessTypeCapture", modelfilter);
                case "Remove":
                    BusinessObject.DeleteBusinessType(BusinessTypeId);  
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditBusinessTypeData(BusinessTypeModel BusinessType)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.CreateBusinessType(BusinessType);

                return RedirectToAction("Index");
            }
            else
                return PartialView("BusinessTypeCapture", BusinessType);
        }
    }
}

