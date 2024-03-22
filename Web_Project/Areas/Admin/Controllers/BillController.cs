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
    public class BillController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Bill
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Bills.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Bill/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // GET: Admin/Bill/Create
        public ActionResult Create()
        {
            ViewBag.staff_id = new SelectList(db.Staffs, "id", "name");
            ViewBag.member_id = new SelectList(db.Memberships, "id", "name");
            ViewBag.table_id = new SelectList(db.Table_Res, "id", "name");
            return View();
        }

        // POST: Admin/Bill/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,staff_id,member_id,reservation_id,table_id,payment_method,bill_time,total_amount")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Bills.Add(bill);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.staff_id = new SelectList(db.Staffs, "id", "name", bill.staff_id);
            ViewBag.member_id = new SelectList(db.Memberships, "id", "name", bill.member_id);
            ViewBag.table_id = new SelectList(db.Table_Res, "id", "name", bill.table_id);
            return View(bill);
        }

        // GET: Admin/Bill/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            ViewBag.staff_id = new SelectList(db.Staffs, "id", "name");
            ViewBag.member_id = new SelectList(db.Memberships, "id", "name");
            ViewBag.table_id = new SelectList(db.Table_Res, "id", "name");
            return View(bill);
        }

        // POST: Admin/Bill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,staff_id,member_id,reservation_id,table_id,payment_method,bill_time,total_amount")] Bill bill)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bill).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.staff_id = new SelectList(db.Bills, "id", "name", bill.staff_id);
            ViewBag.member_id = new SelectList(db.Bills, "id", "name", bill.member_id);
            ViewBag.table_id = new SelectList(db.Bills, "id", "name", bill.table_id);
            return View(bill);
        }

        // GET: Admin/Bill/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bill bill = db.Bills.Find(id);
            if (bill == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Admin/Bill/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bill bill = db.Bills.Find(id);
            db.Bills.Remove(bill);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
