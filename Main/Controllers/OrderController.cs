using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Orders;

namespace HJ_Template_MVC.Controllers
{
    public class OrderController : Infrastructure.BaseController
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetProduct(string name,int skip=0,int take=10)
        {
            List<Product> listProduct = null;
         

            if (name == null)
            {
                listProduct = db.Products
                    .OrderBy(C => C.Name)
                    .Skip(skip).Take(take)
                    .ToList();
            }
            else
            {
              listProduct = db.Products
             .Where(C => C.Name.Contains(name))
             .ToList();
            }

            return Json(listProduct, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            MyUnitOfWork.ProductRepository.Insert(product);
            MyUnitOfWork.Save();
            return View();
        }
    }
}