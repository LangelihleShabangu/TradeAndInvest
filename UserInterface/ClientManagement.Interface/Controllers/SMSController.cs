using ClientManagement.BusinessLogic;
using System.Web.Mvc;
using ClientManagement.DomainModel;
using Techhill.Framework.Mvc.Controllers;

namespace ClientManagement.Interface.Controllers
{
    public class SMSController : BaseController<AdminLogic>
    {
        public ActionResult Index()
        {
            SMSModel model = new SMSModel();
            model.IsSent = false;
            model.accountActive = true;
            return View("Index", model);
        }

        public ActionResult MultipleIndex()
        {
            SMSModel model = new SMSModel();
            model.IsSent = false;
            model.accountActive = true;
            return View("MultipleIndex", model);
        }

        public ActionResult SendMessage(SMSModel model)
        {
            if (string.IsNullOrEmpty(model.Cellnumber))
            {
                BusinessObject.SendSMS(model.TextValue, model.Message);
            }
            else
            {
                //todo Sort this out
                new Messaging().SendSMS(model.Cellnumber, model.Message);
                GetBusinessObject<ClientLogic>().IncrementSMS();
            }
            model.IsSent = true;
            model.accountActive = true;
            return PartialView("Index", model);
        }
    }
}

