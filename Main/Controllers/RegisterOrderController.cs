using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.RegisterOrder;

namespace Controllers
{
    public class RegisterOrderController : Infrastructure.BaseController
    {
        // GET: RegisterOrder
        [HttpGet]
        public ActionResult Check(string jsonOrder)
        {
            if(User.Identity.IsAuthenticated) 
            {
                TempData["ListOrder"] = JsonConvert.DeserializeObject<List<RegisterOrderViewModel>>(jsonOrder);
                return RedirectToAction("Index");
            }
            else
            {
               return RedirectToAction("Create", "Users");
            }
      
        }
        public ActionResult Index()
        {
            List<RegisterOrderViewModel> List = TempData["ListOrder"] as List<RegisterOrderViewModel>;
            TempData.Keep("ListOrder");
            return View(List);
        }

    }
}