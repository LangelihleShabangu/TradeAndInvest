using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace ClientManagement.Interface.Controllers
{
    public class DocumentController : BaseController<DocumentLogic> 
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetDocumentDetails());
        }

        public ActionResult CreateDocument()
        {
            return View("DocumentCapture", BusinessObject.AddDocument());
        }

        public ActionResult CreateDocumentInfo(int ClientId)
        {           
            return View("DocumentCapture", BusinessObject.AddDocument(ClientId));
        }

        [HttpPost]
        public ActionResult CreateEditDocument(string submitbtn, int DocumentId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    DocumentModel modelfilter = new DocumentModel();
                    modelfilter = BusinessObject.GetDocumentDetail(DocumentId);
                    return PartialView("DocumentCapture", modelfilter);                
                case "Remove":
                    BusinessObject.GetDocumentDetail(DocumentId);
                    return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult FileUpload(DocumentModel model)
        {
            if (Request.Files != null && Request.Files.Count == 1)
            {
                var files = Request.Files[0];
                if (files != null && files.ContentLength > 0)
                {
                    var content = new byte[files.ContentLength];
                    files.InputStream.Read(content, 0, files.ContentLength);
                    model.CurrentDocument = content;

                    BusinessObject.SaveDocument(model.DocumentId, model.CurrentDocument);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult ViewDocumentDetails(int DocumentId)
        {
            DocumentModel modelfilter = new DocumentModel();
            modelfilter = BusinessObject.GetDocumentDetail(DocumentId);
            return PartialView("Index", modelfilter);
        }

        [HttpPost]
        public ActionResult CreateEditDocumentData(DocumentModel Document)
        {
            if (ModelState.IsValid)
            {
                if (Request.Files != null && Request.Files.Count == 1)
                {
                    var files = Request.Files[0];
                    if (files != null && files.ContentLength > 0)
                    {
                        var content = new byte[files.ContentLength];
                        files.InputStream.Read(content, 0, files.ContentLength);
                        Document.CurrentDocument = content;
                    }
                }

               // BusinessObject.AddDocument(Document);
                return RedirectToAction("ClientIndex", "Client", new { ClientId = Document.ClientId });    
            }
            else
                return PartialView("Edit", Document);
        }
    }
}
