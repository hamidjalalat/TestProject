using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Models;
using ViewModels.Users;
using Infrastructure;

namespace Main.Controllers
{

 
    public partial class UsersController : Infrastructure.BaseController
    {
        //
        public UsersController()
        {
           
        }
        Hash oHash = new Hash();
        public UsersController(DAL.IUnitOfWork unitOfWork)
			: base(unitOfWork)
		{
        }
        // GET: Users
        public virtual ViewResult Index()
        {
         
            var users = db.Users
                .OrderByDescending(current => current.DataCreate)
               .ToList()
               ;
            return (View(users));
        }

        // GET: Users/Details/5
        public virtual ActionResult Details(int? id)
        {
            if (id.HasValue == false)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users
                .Where(current => current.Id == id)
                .FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(model: user);
        }
        [AllowAnonymous]
  
        public virtual ViewResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public virtual ActionResult Create( CreateViewModel user)
        {
            string username = user.Name.Trim();
            if (string.Compare(username, "AdminAdmin") ==0)
            {
                ModelState.AddModelError(key:"Name", errorMessage: "Some error message!");
            }
            if (username.Length<7)
            {
                ModelState.AddModelError(key: "Name", errorMessage: "نام کاربری باید بیش از هشت حروف باشد");
            }

            if (ModelState.IsValid)
            {
                User objUser = new User();

                objUser.Mobile = user.Mobile;
                objUser.Address = user.Address;
                objUser.Name = username;
                objUser.Password = oHash.GetCreateHash(user.Password);
                objUser.DataCreate = DateTime.Now;
                MyUnitOfWork.UserRepository.Insert(objUser);
                MyUnitOfWork.Save();
                //db.Users.Add(user);
                //db.SaveChanges();
                return RedirectToAction(MVC.Account.Login());
            }
            return View(user);
        }
        [HttpPost]
        public virtual JsonResult CreateAjax(User user)
        {
            bool success = false;

            if (string.Compare(user.Name, "Hjalalat") == 0)
            {
                ModelState.AddModelError(key: "Name", errorMessage: "Some error message!");
            }

            if (ModelState.IsValid)
            {
                Hash oHash = new Hash();
                user.Password = oHash.GetCreateHash(user.Password);
                user.DataCreate = DateTime.Now;
                db.Users.Add(user);
                db.SaveChanges();
                success = true;
            }

            return (Json(new { success = success },
                JsonRequestBehavior.AllowGet));
        }

        // GET: Users/Edit/5
        [Authorize(Users = "AdminAdmin")]
        public virtual ActionResult Edit(int? id)
        {
        
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user =
                db.Users
                .Where(current => current.Id == id)
                .FirstOrDefault()
                ;
            EditViewModel EditViewModel = new EditViewModel();
            EditViewModel.Id = user.Id;
            EditViewModel.Name = user.Name;
            EditViewModel.Address = user.Address;
            EditViewModel.Mobile = user.Mobile;
            

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(EditViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "AdminAdmin")]
        public virtual ActionResult Edit([Bind(Exclude ="googoli_magooli")] EditViewModel user)
        {
        
            User orginalUser =
                db.Users
                .Where(current => current.Id == user.Id)
                .FirstOrDefault();
            if (orginalUser == null)
            {
                return (HttpNotFound());
            }
         
            if (ModelState.IsValid)
            {
                orginalUser.Name = user.Name;
                orginalUser.Password = oHash.GetCreateHash(user.Password);
                orginalUser.DataCreate = DateTime.Now;
                orginalUser.Mobile = user.Mobile;
                orginalUser.Address = user.Address;
                
                db.SaveChanges();
                return RedirectToAction(MVC.Users.Index());
            }
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Users = "AdminAdmin")]
        public virtual ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users
                .Where(current=>current.Id==id)
                .FirstOrDefault()
                ;
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

       
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Users = "AdminAdmin")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users
                .Where(current => current.Id == id)
                .FirstOrDefault()
                ;
            db.Users.Remove(user);
            db.SaveChanges();
            return RedirectToAction(MVC.Users.Index());
        }
        [Authorize(Users = "AdminAdmin")]
        public virtual JsonResult DeleteAJAX(int id)
        {
            //System.Threading.Thread.Sleep(5000);
            User user = db.Users.Where(p => p.Id == id).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("user Not Found!");
                //return Json("Person not found!", JsonRequestBehavior.AllowGet);
            }
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
                //PageMessages.Add(new Infrastructure.PageMessages(Infrastructure.PageMessages.Types.Error, "{0} deleted"+user.Name));
                return Json(new { Succeed = true, Message = string.Format("{0} حذف شد", user.Name), Id = id });
            }
            catch (Exception ex)
            {
                return Json(new { Succeed = false, Message = "حذف نشد!" + ex.Message, Id = id });
            }
        }

    }
}
