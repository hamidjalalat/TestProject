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