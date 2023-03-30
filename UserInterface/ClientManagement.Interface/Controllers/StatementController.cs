using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class StatementController : BaseController<AdminLogic> 
    {
        //public ActionResult Index()
        //{
        //    return View("Index", BusinessObject.GetStatementDetail());
        //}

        public ActionResult Statement()
        {
            return View("StatementCapture", BusinessObject.getStatement());
        }

        //[HttpPost]
        //public ActionResult CreateEditStatement(string submitbtn, int StatementId)
        //{
        //    switch (submitbtn)
        //    {
        //        case "Edit":
        //            StatementModel modelfilter = new StatementModel();
        //            modelfilter = BusinessObject.GetStatementDetail(StatementId);
        //            return PartialView("StatementCapture", modelfilter);
        //        case "Remove":
        //            BusinessObject.DeleteStatement(StatementId);  
        //            return RedirectToAction("Index");
        //    }
        //    return View();
        //}

        [HttpPost]
        public ActionResult UploadFiles(StatementModel model)
        {
            if (Request.Files != null && Request.Files.Count > 0)
            {
                var files = Request.Files[0];
                if (files != null && files.ContentLength > 0)
                {
                    var content = new byte[files.ContentLength];
                    files.InputStream.Read(content, 0, files.ContentLength);
                    model.FileName = files.FileName;
                    model.ImportFile = content;
                }
            }
            model.ClientPaymentList = BusinessObject.ReadCSVFile(model);
            return View("StatementCapture", model);
        }
    }
}

