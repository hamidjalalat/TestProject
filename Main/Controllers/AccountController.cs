using Infrastructure;
using Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Main.Controllers
{
    public partial class AccountController : Infrastructure.BaseController
    {

        // GET: Account
        public virtual ActionResult Login()
        {
            return View();
        }
   
        [HttpPost]
        public virtual ActionResult Login(string username, string password, string Remember,string Captcha)
        {
            //if (Session["Captcha"] == null || Captcha == null || Session["Captcha"].ToString() != Captcha.Trim())
            //{
            //    string capcha = Session["Captcha"].ToString();
            //    ViewBag.MessageCheck = "کد امنیتی را اشتباه وارد کرده اید";
            //    return View();
            //}
            Hash oHash = new Hash();
            string HashPassword = oHash.GetCreateHash(password.Trim());
            bool RememberMe = false;
            RememberMe = Remember != null ? true : false;
            var Result = db.Users
                .Where(current => current.Name == username.Trim() && current.Password ==HashPassword)
                .FirstOrDefault()
                ;
            if (Result != null)
            {
                System.Web.Security.FormsAuthentication
                                   .RedirectFromLoginPage(username,
                                   createPersistentCookie: RememberMe);
                var hc = new HttpCookie("currentuser", username);
                
                hc.Expires = DateTime.Now.AddDays(2);
                Response.SetCookie(hc);
            }
            else
            {
                ViewBag.MessageCheck = Resources.General.InvalidUserName;
            }
            return View();
        }

        public virtual ActionResult Logout()
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session["UserName"] = null;
            if (Request.Cookies["currentuser"] != null)
            {
                Response.Cookies["currentuser"].Expires = DateTime.Now.AddDays(-1);
            }
            return RedirectToAction("Login");
        }
        public virtual JsonResult CheckUsername(string name)
        {
            bool blnResult = true;
            var oUser = db.Users
                .Where(current => current.Name == name.Trim())
                .FirstOrDefault()
                ;
            if (oUser != null)
            {
                blnResult = false;
            }
            return Json(blnResult, JsonRequestBehavior.AllowGet);
        }

        public virtual ActionResult CaptchaImage()
        {
            Random rand = new Random((int)DateTime.Now.Ticks);
            //ساخت عدد رندوم در بازه مشخص
            int a = rand.Next(minValue: 1, maxValue: 99);
            string captcha = string.Format("{0}", a);

            //ذخیره در سشن برای تطبیق دادن
            Session["Captcha"] = a;

            FileContentResult img = null;

            using (MemoryStream mem = new MemoryStream())
            using (Bitmap bmp = new Bitmap(100, 30))
            using (Graphics gfx = Graphics.FromImage((Image)bmp))
            {
                gfx.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
                gfx.SmoothingMode = SmoothingMode.AntiAlias;
                gfx.FillRectangle(Brushes.White, new Rectangle(0, 0, bmp.Width, bmp.Height));

                //کشیدن دایره ها در عکس 
                int i, r, x, y;
                Pen pen = new Pen(Color.Yellow);
                for (i = 1; i < 50; i++)
                {
                    pen.Color = Color.FromArgb(
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)),
                    (rand.Next(0, 255)));

                    r = rand.Next(0, (130 / 3));
                    x = rand.Next(0, 130);
                    y = rand.Next(0, 130);

                    gfx.DrawEllipse(pen, x - r, y - r, r, r);
                }

                //درج عدد در عکس
                gfx.DrawString(captcha, new Font("Tahoma", 15), Brushes.BlueViolet, 2, 3);

                //تحویل عکس
                bmp.Save(mem, System.Drawing.Imaging.ImageFormat.Jpeg);
                img = this.File(mem.GetBuffer(), "image/Jpeg");
            }

            return img;
        }
    }
}