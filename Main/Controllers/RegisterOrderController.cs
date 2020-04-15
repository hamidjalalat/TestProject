﻿using Models;
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
        public virtual ActionResult Check()
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
        public virtual ActionResult Check(string jsonOrder)
        {
            TempData["ListOrder"] = JsonConvert.DeserializeObject<List<RegisterOrderViewModel>>(jsonOrder);
            return Json(true);
        }
        public virtual ActionResult Index()
        {
            List<RegisterOrderViewModel> List = TempData["ListOrder"] as List<RegisterOrderViewModel>;
            TempData.Keep("ListOrder");
            return View(List);
        }
        public ActionResult RefisterFactor()
        {
            List<RegisterOrderViewModel> ListOrder = TempData["ListOrder"] as List<RegisterOrderViewModel>;
            TempData.Keep("ListOrder");
            Factor oFactor = new Factor();
           
            oFactor.Date = DateTime.Now;
            oFactor.UserName = User.Identity.Name;
            oFactor.FactorDetails = new List<FactorDetail>();
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
          
            return View();
        }

    }
}