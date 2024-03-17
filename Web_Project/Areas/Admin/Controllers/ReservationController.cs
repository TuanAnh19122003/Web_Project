using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using Web_Project.Models;

namespace Web_Project.Areas.Admin.Controllers
{
    public class ReservationController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Reservation
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Reservations.OrderBy(b => b.reservation_id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation res = db.Reservations.Find(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        // GET: Admin/Reservation/Create
        public ActionResult Create()
        {
            ViewBag.table_id = new SelectList(db.Table_Res, "id", "name");
            return View();
        }

        // POST: Admin/Reservation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "reservation_id,customer_name,phone_number,table_id,quantity,reservation_time,reservation_date,Note")] Reservation res)
        {
            if (ModelState.IsValid)
            {
                db.Reservations.Add(res);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(res);
        }

        // GET: Admin/Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation res = db.Reservations.Find(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            ViewBag.table_id = new SelectList(db.Table_Res, "id", "name", res.table_id);
            return View(res);
        }

        // POST: Admin/Reservation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "reservation_id,customer_name,phone_number,table_id,quantity,reservation_time,reservation_date,Note")] Reservation res)
        {
            if (ModelState.IsValid)
            {
                db.Entry(res).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.table_id = new SelectList(db.Reservations, "id", "name", res.table_id);
            return View(res);
        }

        // GET: Admin/Reservation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation res = db.Reservations.Find(id);
            if (res == null)
            {
                return HttpNotFound();
            }
            return View(res);
        }

        // POST: Admin/Reservation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservation res = db.Reservations.Find(id);
            db.Reservations.Remove(res);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
