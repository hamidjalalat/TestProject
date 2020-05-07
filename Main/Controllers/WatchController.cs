using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using ViewModels.Watch;

namespace HJ_Template_MVC.Controllers
{
    [Authorize(Users ="AdminAdmin")]
    public partial class WatchController : Infrastructure.BaseController
    {
        // GET: Watch
        public virtual ActionResult Index()
        {
            ViewBag.Config = db.Configs.ToList();

            var listOrder = db.Factors
                .Include(C => C.FactorDetails)
                .Where(Now => Now.Date.Day == DateTime.Now.Day)
                .OrderBy(Date => Date.Date)
                .ToList();
            var FirstOrder = db.Factors.Where(D=>D.approved==1).Select(C => C.UserName).ToArray();
            ViewBag.FirstOrder = FirstOrder;
            return View(listOrder);

        }
        [HttpPost]
        public virtual ActionResult Index(FactorViewModel factor)
        {

            var listOrder = db.Factors
              .Include(C => C.FactorDetails)
              .AsQueryable();
            
            if (factor.UserName != "" && factor.UserName!=null)
            {
                listOrder = listOrder.Where(C => C.UserName.Contains(factor.UserName)).AsQueryable();
            }
            
            if (factor.Mobile != "" && factor.Mobile != null)
            {
                listOrder = listOrder.Where(C => C.Mobile.Contains(factor.Mobile)).AsQueryable();
            }
           
            if (factor.Address != "" && factor.Address != null)
            {
                listOrder = listOrder.Where(C => C.Address.Contains(factor.Address)).AsQueryable();
            }
            if (factor.approved !=null && factor.approved != null)
            {
                listOrder = listOrder.Where(C => C.approved==0).AsQueryable();
            }
            ViewBag.Serche = factor;
            var FirstOrder = db.Factors.Where(D => D.approved == 0).Select(C => C.UserName).ToArray();
            ViewBag.FirstOrder = FirstOrder;

            var  result = listOrder
                 .Where(Now => Now.Date.Day == DateTime.Now.Day)
              .OrderBy(Date => Date.Date).ToList();

            return View(result);
        }

        [HttpPost]
        public virtual ActionResult approve(Guid Id,string ma,int appro)
        {
            bool Success = false;
            try
            {
                var factor = db.Factors.Where(C => C.Id == Id).FirstOrDefault();
                factor.approved = appro;
                factor.MessageAdmin = ma;
                db.SaveChanges();
                Success = true;
            }
            catch (Exception)
            {
                Success = false;

            }
            
            return Json(Success);
        }
    }
}