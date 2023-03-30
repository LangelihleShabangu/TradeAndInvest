using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class DoctorController : BaseController<AdminLogic>  
    {
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult CreateDoctor()
        {
            return View("DoctorCapture", new DoctorModel());
        }

        [HttpPost]
        public ActionResult CreateEditDoctor(string submitbtn, int DoctorId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    DoctorModel modelfilter = new DoctorModel();                   
                    return PartialView("DoctorCapture", modelfilter);
                case "Remove":                    
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditDoctorData(DoctorModel Doctor)
        {
            if (ModelState.IsValid)
            {               
                return RedirectToAction("Index");
            }
            else
                return PartialView("DoctorCapture", Doctor);
        }
    }
}

