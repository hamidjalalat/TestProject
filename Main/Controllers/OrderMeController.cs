using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Watch;

namespace HJ_Template_MVC.Controllers
{
    [Authorize]
    public partial class OrderMeController : Infrastructure.BaseController
    {
        // GET: OrderMe
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public virtual JsonResult GetFactor(int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            int rowNumber = pageIndex * pageSize;

            var listFactorQuery =
                    db.Factors

                    .Where(C => C.UserName == User.Identity.Name)
                    .AsQueryable()
                    ;



            int Count = listFactorQuery.Count();

            listFactorQuery =
              listFactorQuery
              .OrderByDescending(current => current.Date)
              .Skip(pageIndex * pageSize)
              .Take(pageSize);

            var listFactor = listFactorQuery.ToList();

            List<FactorViewModel> listFactorViewModel = new List<FactorViewModel>();

            foreach (var item in listFactor)
            {
                FactorViewModel objFactor = new FactorViewModel();
                objFactor.approved = (item.approved.ToString() == "True") ? "تایید شد" : "منتظر تایید";
                objFactor.RowNumber = ++rowNumber;
                objFactor.Id = item.Id;
                objFactor.Date = item.Date.ToShamsi() + " و ساعت:  " + item.Date.ToShortTimeString();
                listFactorViewModel.Add(objFactor);
            }


            var result = new { data = listFactorViewModel, count = Count };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult GetDatialFactor(Guid id)
        {
            var result = db.FactorDetails
                .Where(C => C.FactorId == id)
                .Select(D => new { Name = D.Name, Price = D.Price, count = D.Count, Id = D.Id })
                .ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}