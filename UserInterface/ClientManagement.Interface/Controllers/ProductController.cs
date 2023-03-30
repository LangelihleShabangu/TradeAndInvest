using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace VIcocaApplication.UI.Controllers
{
    public class ProductController : BaseController<AdminLogic> 
    {
        public ActionResult Index()
        {
            return View("Index", BusinessObject.GetProductDetail());
        }

        public ActionResult CreateProductInfo()
        {
            return View("ProductCapture", new ProductsModel());
        }
        
        [HttpPost]
        public ActionResult CreateEditProductInfo(string submitbtn, int ProductsId)
        {
            switch (submitbtn)
            {
                case "Edit":
                    ProductsModel modelfilter = new ProductsModel();
                    modelfilter = BusinessObject.GetProductDetail(ProductsId);
                    return PartialView("ProductCapture", modelfilter);
                //case "Materials Covered":
                //    ProductReceiptModel ProductReceiptfilter = new ProductReceiptModel();
                //    ProductReceiptfilter = BusinessObject.GetProductReceiptDetailInfo(ProductInfoId);
                //    return PartialView("ProductsRecipeIndex", ProductReceiptfilter);
                case "Remove":
                    BusinessObject.DeleteProduct(ProductsId);  
                    return RedirectToAction("Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult CreateEditProductData(ProductsModel Product)
        {
            if (ModelState.IsValid)
            {
                BusinessObject.CreateProduct(Product);
                return RedirectToAction("Index");
            }
            else
            {
                Product = BusinessObject.GetProductDetail(Product.ProductsId);
                return PartialView("ProductCapture", Product);
            }
        }
    }
}

