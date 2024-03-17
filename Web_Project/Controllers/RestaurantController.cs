using PagedList;
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
        public ActionResult Deals(int ? page)
        {
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            var list = db.Deals.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }
        public ActionResult Reservation([Bind(Include = "reservation_id,customer_name,phone_number,table_id,quantity,reservation_time,reservation_date,Note")] Reservation res)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(res);
                db.SaveChanges();
                return RedirectToAction("Reservation");
            }
            ModelState.Clear();
            ViewBag.table_id = new SelectList(db.Table_Res, "id", "name");
            return View(res);
        }

    }
}