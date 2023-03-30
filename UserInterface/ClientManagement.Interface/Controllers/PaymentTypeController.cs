using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class PaymentTypeController : BaseController<AdminLogic> 
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetPaymentTypeDetail());
        }

        public ActionResult CreatePaymentType()
        {
            return View("PaymentTypeCapture",BusinessObject.GetPaymentTypeDetail());
        }

        [HttpPost]
        public ActionResult CreateEditPaymentType(string submitbtn, int PaymentTypeId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    PaymentTypeModel modelfilter = new PaymentTypeModel();
                    modelfilter = BusinessObject.GetPaymentTypeDetail(PaymentTypeId);
                    return PartialView("PaymentTypeCapture", modelfilter);                
                case "Remove":
                    BusinessObject.DeletePaymentType(PaymentTypeId);  
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditPaymentTypeData(PaymentTypeModel PaymentType)
        {
            if (ModelState.IsValid)
            {
               BusinessObject.CreatePaymentType(PaymentType);
               return RedirectToAction("Index");
            }
            else
                return PartialView("PaymentTypeCapture", PaymentType);
        }
    }
}

