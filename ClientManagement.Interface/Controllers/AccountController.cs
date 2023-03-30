using System.Web.Mvc;
using System.Web.Security;
using Techhill.Framework.DomainTypes.Account;
using Techhill.Framework.Mvc.Controllers;

namespace ShabanguGroupApplication.UIInterface.Controllers
{
    public class AccountController : TechhillAccountController
    {
        public ActionResult Out()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOut");
        }

    }
}