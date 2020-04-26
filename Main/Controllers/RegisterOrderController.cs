using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.RegisterOrder;


namespace Controllers
{
    public partial class RegisterOrderController : Infrastructure.BaseController

    {
        [HttpGet]
        public virtual ActionResult SecondCheck()
        {

            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }
        [HttpPost]
        public virtual ActionResult FirstCheck(string jsonOrder,string description)
        {
         
            TempData["description"] = description;
            TempData["ListOrder"] = JsonConvert.DeserializeObject<List<RegisterOrderViewModel>>(jsonOrder);
       
         
            return Json(true);
        }
        public virtual ActionResult Index()
        {
            List<RegisterOrderViewModel> List = TempData["ListOrder"] as List<RegisterOrderViewModel>;
            TempData.Keep("ListOrder");
            if (List==null)
            {
                List = new List<RegisterOrderViewModel>();
            }
            return View(List);
        }
        public virtual ActionResult RegisterFactor()
        {
            List<RegisterOrderViewModel> ListOrder = TempData["ListOrder"] as List<RegisterOrderViewModel>;
            if (ListOrder==null)
            {
               return RedirectToAction("Index", "OrderMe");
            }
            bool success = false;
            try
            {
                var customer = db.Users
                    .Where(C => C.Name == User.Identity.Name)
                    .FirstOrDefault();
                Factor oFactor = new Factor();

                oFactor.Date = DateTime.Now;
                oFactor.UserName = User.Identity.Name;
                oFactor.FactorDetails = new List<FactorDetail>();
                oFactor.Mobile = customer.Mobile;
                oFactor.Address = customer.Address;
                oFactor.Description = TempData["description"] as string;
                foreach (var item in ListOrder)
                {
                    FactorDetail oFactorDetail = new FactorDetail();
                    oFactorDetail.Name = item.Name;
                    oFactorDetail.Price = item.Price;
                    oFactorDetail.ProductId = item.Id;
                    oFactorDetail.FactorId = oFactor.Id;
                    oFactorDetail.Count = item.count;
                    oFactor.FactorDetails.Add(oFactorDetail);
                }

                db.Factors.Add(oFactor);
                db.SaveChanges();

                success = true;

                if (Request.Cookies["listProduct"] != null)
                {
                    Response.Cookies.Remove("listProduct");
                    Response.Cookies["listProduct"].Expires = DateTime.Now.AddDays(-1);
                    Session.Abandon();
                }
            }
            catch (Exception)
            {
                success = false;
            }

            ViewBag.success = success;

            return View();
        }

    }
}