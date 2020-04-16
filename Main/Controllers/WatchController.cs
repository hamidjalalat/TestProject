using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace HJ_Template_MVC.Controllers
{
    public class WatchController : Infrastructure.BaseController
    {
        // GET: Watch
        public ActionResult Index()
        {
            var listOrder = db.Factors.Include(C => C.FactorDetails).ToList();
            return View(listOrder);
        }
        [HttpPost]
        public ActionResult Index(Guid Id)
        {
            try
            {
                var factor = db.Factors.Where(C => C.Id == Id).FirstOrDefault();
                factor.approved = true;
                db.SaveChanges();
            }
            catch (Exception)
            {

           
            }
            var listOrder = db.Factors.Include(C => C.FactorDetails).ToList();
            return View(listOrder);
        }
    }
}