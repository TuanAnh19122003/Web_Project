using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web_Project.Models;

namespace Web_Project.Controllers
{
    public class RestaurantController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Restaurant
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            return View();
        }
        public ActionResult Contact([Bind(Include = "id,name,email,phone_number,request_date,Note")] Contact ct)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(ct);
                db.SaveChanges();
                return RedirectToAction("Contact");
            }

            ModelState.Clear();
            return View(ct);

        }
        public ActionResult Menu()
        {
            var menuData = db.Menus.Include("Category").ToList();
            return View(menuData);
        }
        public ActionResult Deals()
        {
            return View();
        }
    }
}