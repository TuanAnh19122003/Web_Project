using System;
using System.Collections.Generic;
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

        public ActionResult logout()
        {
            Session.Remove("user");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}