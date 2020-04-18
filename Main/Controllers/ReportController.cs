using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Watch;

namespace HJ_Template_MVC.Controllers
{
    public class ReportController : Infrastructure.BaseController
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public virtual JsonResult GetFactor(FactorViewModel factor, int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            int rowNumber = pageIndex * pageSize;

            var listFactorQuery =
                    db.Factors
                    .AsQueryable()
                    ;

            if (factor.UserName != "" && factor.UserName != null)
            {
                listFactorQuery = listFactorQuery.Where(C => C.UserName.Contains(factor.UserName)).AsQueryable();
            }

            if (factor.Mobile != "" && factor.Mobile != null)
            {
                listFactorQuery = listFactorQuery.Where(C => C.Mobile.Contains(factor.Mobile)).AsQueryable();
            }

            if (factor.Address != "" && factor.Address != null)
            {
                listFactorQuery = listFactorQuery.Where(C => C.Address.Contains(factor.Address)).AsQueryable();
            }

            int Count = listFactorQuery.Count();

            listFactorQuery =
              listFactorQuery
              .OrderBy(current => current.UserName)
              .Skip(pageIndex * pageSize)
              .Take(pageSize);

            var listFactor = listFactorQuery.ToList();

            List<FactorViewModel> listFactorViewModel = new List<FactorViewModel>();

            foreach (var item in listFactor)
            {
                FactorViewModel objFactor = new FactorViewModel();
                objFactor.UserName = item.UserName;
                objFactor.Address = item.Address;
                objFactor.approved = item.approved.ToString();
                objFactor.Mobile = item.Mobile;
                objFactor.RowNumber = ++rowNumber;



                listFactorViewModel.Add(objFactor);
            }

            var result = new { data = listFactorViewModel, count = Count };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}