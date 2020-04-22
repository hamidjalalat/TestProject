using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HJ_Template_MVC.Controllers
{
    public partial class AboutController : Infrastructure.BaseController
    {
        // GET: About
        public virtual ActionResult Index()
        {
            return View();
        }

        public virtual ActionResult HamidJalalat()
        {
            var listContact = db.Contacts.OrderByDescending(C => C.Id).ToList();

            return View(listContact);
        }
        [HttpPost]
        public ActionResult HamidJalalat(string text)
        {
            if (text != string.Empty)
            {
                try
                {
                    Models.Contact contect = new Models.Contact();
                    contect.UserName = User.Identity.Name;
                    contect.Text = text;

                    db.Contacts.Add(contect);
                    db.SaveChanges();

                }
                catch (Exception)
                {

                }
            }
            var listContact = db.Contacts.OrderByDescending(C => C.Id).ToList();
            return View(listContact);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            try
            {
                var selectDelete = db.Contacts.Where(C => C.Id == id).FirstOrDefault();
                db.Contacts.Remove(selectDelete);
                db.SaveChanges();
                result = true;
            }
            catch (Exception)
            {

            }


            return Json(result);
        }
    }
}