using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
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
            ViewBag.GroupProductId = new SelectList(db.GroupProducts, "Id", "Name");
            return View();
        }
        [HttpPost]
        public virtual ActionResult Create(Product product, HttpPostedFileBase file)
        {

            if (file == null)
            {
                ModelState.AddModelError(key: "Available", errorMessage: "انتخاب عکس اجباری می باشد");
              
            }
            if (ModelState.IsValid)
            {
                var allowedExtensions = new[] { ".Jpg", ".png", ".jpg", "jpeg" };

                var fileName = Path.GetFileName(file.FileName);
                var ext = Path.GetExtension(file.FileName);

                if (allowedExtensions.Contains(ext))
                {
                    string name = Path.GetFileNameWithoutExtension(fileName);
                    string myfile = name + "_" + product.Name + ext;
                    var path = Path.Combine(Server.MapPath("~/Picture"), myfile);
                    product.Image_url = "/Picture/"+ myfile;
                    file.SaveAs(path);
                }

                MyUnitOfWork.ProductRepository.Insert(product);
                MyUnitOfWork.Save();
                return RedirectToAction(MVC.Order.Index());
            }

            return View(product);
        }
        [HttpPost]
        public virtual JsonResult GetInfoEdit(int idEdit)
        {
            var listProduct = db.Products.Where(C => C.Id == idEdit)
            .Select(C => new { Name = C.Name, Price = C.Price, Description = C.Description, Available = C.Available, GroupProductId= C.GroupProductId })
            .FirstOrDefault();

            var listGruopProduct = db.GroupProducts.Select(C=> new {Id=C.Id,Name=C.Name }).ToList();

            var result = new { listProduct= listProduct, listGruopProduct= listGruopProduct };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public virtual JsonResult Edit(ViewModels.Orders.ProductsViewModel product)
        {
            bool updated = false;
            try
            {
                var rowEdit = db.Products.Where(C => C.Id == product.Id).FirstOrDefault();
                rowEdit.GroupProductId = product.GroupProductId;
                rowEdit.Available = product.Available;
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
        //[Infrastructure.Log]
        public virtual ActionResult OrederCustomer()
        {
            var config = db.Configs.ToList();
            ConfigViewModel configVm = new ConfigViewModel();
            configVm.maxenable = config.Where(Value => Value.Name == "maxenable").FirstOrDefault().Value;
            Int64 maxorder =Convert.ToInt64( config.Where(Value => Value.Name == "maxorder").FirstOrDefault().Value);
            Int64 maxvalue =Convert.ToInt64( config.Where(Value => Value.Name == "maxvalue").FirstOrDefault().Value);
            if (configVm.maxenable == "1")
            {
                string message = $"توجه:سفارش کمتر از {maxorder.ToString("#,##0 ")} تومان مشمول دریافت   {maxvalue.ToString("#,##0 ")} تومان هزینه پیک می گردد " ;
                ViewBag.messgae = message;
            }

            return View();
        }
        [HttpPost]
        public virtual JsonResult GetListProduct()
        {
            var listProduct = db.Products
            .ToList();

            var config = db.Configs.ToList();

            ConfigViewModel configVm = new ConfigViewModel();

            configVm.breadPrice= config.Where(Value => Value.Name == "non").FirstOrDefault().Value;
            configVm.maxenable= config.Where(Value => Value.Name == "maxenable").FirstOrDefault().Value;
            configVm.maxorder = config.Where(Value => Value.Name == "maxorder").FirstOrDefault().Value;
            configVm.maxvalue = config.Where(Value => Value.Name == "maxvalue").FirstOrDefault().Value;

            List<ProductsViewModel> listProductVM = new List<ProductsViewModel>();

            foreach (var C in listProduct)
            {
                ProductsViewModel obj = new ProductsViewModel();

                obj.Id = C.Id;
                obj.Name = C.Name;
                obj.Price = C.Price;
                obj.Description = C.Description;
                obj.Available = C.Available;
                obj.GroupProductId = C.GroupProductId;
                obj.Image_url = C.Image_url;
                obj.GroupProductId = C.GroupProductId;
                obj.ShowBread = (C.GroupProductId == 3) ? true : false;
                obj.isFlipped = true;
              
                listProductVM.Add(obj);
            }

            var listGruopProduct = db.GroupProducts.Select(C => new { Id = C.Id, Name = C.Name }).ToList();
            var listProductresult = listProductVM.OrderBy(C => C.GroupProductId);
            var configSet = configVm;
            var result = new {listProduct = listProductresult, listGruopProduct = listGruopProduct,config= configSet };

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public virtual JsonResult GetConfig()
        {
        

            var config = db.Configs.ToList();

            ConfigViewModel configVm = new ConfigViewModel();

            configVm.breadPrice = config.Where(Value => Value.Name == "non").FirstOrDefault().Value;
            configVm.maxenable = config.Where(Value => Value.Name == "maxenable").FirstOrDefault().Value;
            configVm.maxorder = config.Where(Value => Value.Name == "maxorder").FirstOrDefault().Value;
            configVm.maxvalue = config.Where(Value => Value.Name == "maxvalue").FirstOrDefault().Value;



            var configSet = configVm;
            var result = new { config = configSet };

            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}