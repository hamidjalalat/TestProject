using Models;
using System;
using System.Collections.Generic;
using System.IO;
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

        [HttpPost]
        public virtual JsonResult DeleteProduct(int idDelete)
        {
            bool deleted = true;
            try
            {
                var result = db.Products.Where(C => C.Id == idDelete).FirstOrDefault();
                db.Products.Remove(result);
                db.SaveChanges();
            }
            catch (Exception)
            {
                deleted = false;
            }

            return Json(deleted);
        }
        public virtual ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public virtual ActionResult Create(Product product, HttpPostedFileBase file)
        {
            var allowedExtensions = new[] {
             ".Jpg", ".png", ".jpg", "jpeg"};

            product.Image_url = file.ToString(); 
            var fileName = Path.GetFileName(file.FileName);  
            var ext = Path.GetExtension(file.FileName);  
            
            if (allowedExtensions.Contains(ext))   
            {
                string name = Path.GetFileNameWithoutExtension(fileName); 
                string myfile = name + "_" + product.Name + ext; 
                var path = Path.Combine(Server.MapPath("~/Picture"), myfile);
                product.Image_url = path;
               file.SaveAs(path);
            }

            MyUnitOfWork.ProductRepository.Insert(product);
            MyUnitOfWork.Save();
            return View();
        }
        [HttpPost]
        public virtual JsonResult GetInfoEdit(int idEdit)
        {
            var result = db.Products.Where(C => C.Id == idEdit).FirstOrDefault();

            return Json(result);
        }
        [HttpPost]
        public virtual JsonResult Edit(ViewModels.Orders.ProductsViewModel product)
        {
            bool updated = false;
            try
            {
                var rowEdit = db.Products.Where(C => C.Id == product.Id).FirstOrDefault();
                rowEdit.Name = product.Name;
                rowEdit.Description = product.Description;
                rowEdit.Price = product.Price.Value;
                db.SaveChanges();
                updated = true;
            }
            catch (Exception)
            {

            }
            return Json(updated);

        }
    }
}