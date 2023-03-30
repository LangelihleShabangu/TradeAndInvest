using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class AreaController : BaseController<AdminLogic>  
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetAreaDetail());
        }

        public ActionResult CreateArea()
        {
            return View("AreaCapture", new AreaModel());
        }

        [HttpPost]
        public ActionResult CreateEditArea(string submitbtn, int AreaId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    AreaModel modelfilter = new AreaModel();
                    modelfilter = BusinessObject.GetAreaDetail(AreaId);
                    return PartialView("AreaCapture", modelfilter);
                case "Remove":
                    BusinessObject.DeleteArea(AreaId);  
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditAreaData(AreaModel Area)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.CreateArea(Area);
                return RedirectToAction("Index");
            }
            else
                return PartialView("AreaCapture", Area);
        }
    }
}

