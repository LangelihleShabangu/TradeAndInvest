using ClientManagement.BusinessLogic;
using ClientManagement.DomainModel;
using System.Web.Mvc;
using Techhill.Framework.Mvc.Controllers;

namespace ClientManagement.Interface.Controllers
{
    public class PaymentsController : BaseController<PaymentsLogic>
    {
    //    public ActionResult Index(int PaymentsId)
    //    {
    //        var models = new PaymentsModel();
    //        return View(BusinessObject.GetPaymentsDetails(PaymentsId));
    //    }

    //    [HttpPost]
    //    public ActionResult Edit(string submitbtn, int PaymentsId)
    //    {
    //        switch (submitbtn)
    //        {
    //            case "Edit":
    //                return PartialView("Payments", BusinessObject.GetSinglePaymentsDetails(PaymentsId));
    //            case "Remove":
    //                return RedirectToAction("ClientIndex", "Client", new { ClientId = BusinessObject.DeletePayments(PaymentsId) });
    //        }
    //        return View();
    //    }

    //    public ActionResult CreatePayments(int ClientId)
    //    {
    //        return View("Payments", BusinessObject.CreatePaymentsModelData(ClientId));
    //    }

    //    public ActionResult CreatePaymentsData(int ClientId)
    //    {
    //        return View("Payments", BusinessObject.CreatePaymentsModelDetails(ClientId));
    //    }

    //    [HttpPost]
    //    public ActionResult CreatePayments(PaymentsModel Payments)
    //    {

    //        if (ModelState.IsValid)
    //        {
    //            int ClientId = BusinessObject.SavePayments(Payments);
    //            return RedirectToAction("ClientIndex", "Client", new { ClientId = ClientId });
    //        }
    //        return RedirectToAction("ClientIndex", "Client", new { ClientId = Payments.ClientId });
    //    }
    }
}
