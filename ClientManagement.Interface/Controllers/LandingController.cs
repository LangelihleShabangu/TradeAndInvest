using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class LandingController : Controller
    {
        public ActionResult Index()
        {
            return View("Index", new AssessmentModel());
        }        
    }
}

