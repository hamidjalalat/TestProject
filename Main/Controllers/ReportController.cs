using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Watch;


namespace HJ_Template_MVC.Controllers
{
    [Authorize(Users = "AdminAdmin")]
    public partial class ReportController : Infrastructure.BaseController
    {
        // GET: Report
        public virtual ActionResult Index()
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
              .OrderBy(current => current.Date)
              .Skip(pageIndex * pageSize)
              .Take(pageSize);

            var listFactor = listFactorQuery.ToList();

            List<FactorViewModel> listFactorViewModel = new List<FactorViewModel>();

            foreach (var item in listFactor)
            {
                FactorViewModel objFactor = new FactorViewModel();
                switch (item.approved)
                {
                    case 2:
                        {
                            objFactor.approved = "پذیرفته نشد!";
                            break;
                        }
                    case 1:
                        {
                            objFactor.approved = "تایید شده";
                            break;
                        }

                    default:
                        {
                            objFactor.approved = "منتظر تایید";
                            break;
                        }

                }
                objFactor.UserName = item.UserName;
                objFactor.Address = item.Address;
                objFactor.Mobile = item.Mobile;
                objFactor.RowNumber = ++rowNumber;
                objFactor.Id = item.Id;
                objFactor.Date = item.Date.ToShamsi() + " |" + item.Date.ToShortTimeString();
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