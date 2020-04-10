using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels.Orders;

namespace HJ_Template_MVC.Controllers
{
    public partial class OrderController : Infrastructure.BaseController
    {
        // GET: Order
        public virtual ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public virtual JsonResult GetProduct(ViewModels.Orders.ProductsViewModel product, int pageIndex, int pageSize)
        {
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }

            int rowNumber = pageIndex * pageSize;

            var listProductQuery =
                    db.Products
                    .AsQueryable()
                    ;

            if (product.Name != null)
            {
                listProductQuery =
                    listProductQuery
                    .Where(current => current.Name.Contains(product.Name))
                    ;
            }

            if (product.Price != null)
            {
                listProductQuery =
                    listProductQuery
                    .Where(current => current.Price == product.Price.Value)
                    ;
            }

            int Count = listProductQuery.Count();

            listProductQuery =
              listProductQuery
              .OrderBy(current => current.Name)
              .Skip(pageIndex * pageSize)
              .Take(pageSize);

            var listProduct = listProductQuery.ToList();

            List<ProductsViewModel> listProductViewModel = new List<ProductsViewModel>();

            foreach (var item in listProduct)
            {
                ProductsViewModel objProductsViewModel = new ProductsViewModel();

                objProductsViewModel.Id = item.Id;
                objProductsViewModel.Name = item.Name;
                objProductsViewModel.Price = item.Price;
                objProductsViewModel.Description = item.Description;
                objProductsViewModel.RowNumber = ++rowNumber;

                listProductViewModel.Add(objProductsViewModel);
            }

            var result = new { data = listProductViewModel, count = Count };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //private int GetCountryCount()
        //{
        //    int intCount =
        //        db.Products
        //        .Count();

        //    return (intCount);
        //}
        //[HttpPost]
        //public virtual JsonResult GetListProduct()
        //{

        //    var listProduct = db.Products
        //             .OrderBy(C => C.Name)
        //             .Skip(0).Take(10)
        //             .ToList();
        //    var result = new { data = listProduct, count = GetCountryCount() };
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}
        public virtual ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public virtual ActionResult Create(Product product)
        {
            MyUnitOfWork.ProductRepository.Insert(product);
            MyUnitOfWork.Save();
            return View();
        }
    }
}