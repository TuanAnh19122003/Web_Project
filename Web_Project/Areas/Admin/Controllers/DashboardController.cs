using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Web_Project.Models;

namespace Web_Project.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Dashboard
        public ActionResult Index()
        {
            if (Session["user"] == null)
            {
                return RedirectToAction("Login");
            }
            else
            {
                return View();
            }

        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string user, string pass)
        {
            var acc = db.Accounts.SingleOrDefault(m => m.account1.Trim().ToLower() == user.Trim().ToLower() && m.password == pass);

            if (acc != null)
            {
                Session["user"] = acc;
                FormsAuthentication.SetAuthCookie(acc.account1, false);
                if (acc.role_id == "Admin")
                {
                    return RedirectToAction("Index");
                }
                else if (acc.role_id == "Member")
                {
                    return RedirectToAction("Index", "Restaurant", new { area = "" });
                }
                // Add a return statement for other roles if needed
            }

            TempData["error"] = "Tài khoản không chính xác vui lòng nhập lại";
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.role_id = new SelectList(db.roles, "id", "name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "id,img,account1,password,role_id,register_date,phone_number")] Account acc, HttpPostedFileBase img)
        {
            var existingAccount = db.Accounts.FirstOrDefault(a => a.account1 == acc.account1);
            if (existingAccount != null)
            {
                ModelState.AddModelError("account1", "Tên tài khoản đã tồn tại.");
            }
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Theme/img/Foods"), newFileName);
                    img.SaveAs(path);
                    acc.img = newFileName;
                }

                // Save the rest of the model and perform other necessary actions
                db.Accounts.Add(acc);
                db.SaveChanges();

                // Chuyển hướng về trang đăng nhập sau khi đăng ký thành công
                return RedirectToAction("Login");
            }
            ViewBag.role_id = new SelectList(db.roles, "id", "name", acc.role_id);
            return View(acc);
        }

        public ActionResult logout()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Restaurant", new { area = "" });
        }
    }
}