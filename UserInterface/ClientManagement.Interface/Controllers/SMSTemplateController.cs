using ClientManagement.BusinessLogic;
using System.Web.Mvc;
using ClientManagement.DomainModel;
using Techhill.Framework.Mvc.Controllers;

namespace ClientManagement.Interface.Controllers
{
    public class SMSTemplateController : BaseController<AdminLogic>
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetSMSTemplateData());
        }

        public ActionResult CreateSMSTemplate()
        {
            SMSTemplateModel SMSTemplatefilter = new SMSTemplateModel();
            var SMSTemplateReceiptfilter = BusinessObject.GetSMSTemplateDetail(0);
            return View("SMSTemplateCapture", SMSTemplateReceiptfilter);
        }

        public ActionResult CreateEditSMSTemplateData(int SMSTemplatesId)
        {
            SMSTemplateModel SMSTemplatefilter = new SMSTemplateModel();
            var SMSTemplateReceiptfilter = BusinessObject.GetSMSTemplateDetail(SMSTemplatesId);
            return PartialView("Index", SMSTemplateReceiptfilter);
        }

        [HttpPost]
        public ActionResult CreateEditSMSTemplate(string submitbtn, int SMSTemplateId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    SMSTemplateModel modelfilter = new SMSTemplateModel();
                    modelfilter = BusinessObject.GetSMSTemplateDetail(SMSTemplateId);
                    return PartialView("SMSTemplateCapture", modelfilter);
                case "Recipe":
                    SMSTemplateModel SMSTemplateReceiptfilter = new SMSTemplateModel();
                    SMSTemplateReceiptfilter = BusinessObject.GetSMSTemplateDetail(SMSTemplateId);
                    return PartialView("SMSTemplatesRecipeIndex", SMSTemplateReceiptfilter);
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditSMSTemplateData(SMSTemplateModel SMSTemplate)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.CreateSMSTemplate(SMSTemplate);

                return RedirectToAction("Index");
            }
            else
                return PartialView("SMSTemplateCapture", SMSTemplate);
        }
    }
}

