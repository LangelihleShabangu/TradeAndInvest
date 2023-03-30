using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class CreditorController : BaseController<AdminLogic> 
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetCreditorDetail());
        }

        public ActionResult CreateCreditor()
        {
            return View("CreditorCapture", BusinessObject.GetCreditorDetail(0));
        }

        [HttpPost]
        public ActionResult CreateEditCreditor(string submitbtn, int CreditorId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    CreditorModel modelfilter = new CreditorModel();
                    modelfilter = BusinessObject.GetCreditorDetail(CreditorId);
                    return PartialView("CreditorCapture", modelfilter);
                case "Remove":
                    BusinessObject.DeleteCreditor(CreditorId);  
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditCreditorData(CreditorModel Creditor, string color)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files != null)
                {
                    if (Request.Files != null && Request.Files.Count > 0)
                    {
                        var files = Request.Files[0];
                        if (files != null && files.ContentLength > 0)
                        {
                            var content = new byte[files.ContentLength];
                            files.InputStream.Read(content, 0, files.ContentLength);
                            Creditor.CreditorImage = content;
                        }
                    }
                }
                Creditor.Color = color;
                BusinessObject.CreateCreditor(Creditor);
                return RedirectToAction("Index");
            }
            else
                return PartialView("CreditorCapture", Creditor);
        }
    }
}

