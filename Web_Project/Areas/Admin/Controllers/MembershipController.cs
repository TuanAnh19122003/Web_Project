using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Project.Models;

namespace Web_Project.Areas.Admin.Controllers
{
    public class MembershipController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Membership
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Memberships.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Membership/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membership ms = db.Memberships.Find(id);
            if (ms == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // GET: Admin/Membership/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Membership/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,points,account_id")] Membership ms)
        {
            if (ModelState.IsValid)
            {
                db.Memberships.Add(ms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ms);
        }

        // GET: Admin/Membership/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membership ms = db.Memberships.Find(id);
            if (ms == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Admin/Membership/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,points,account_id")] Membership ms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ms);
        }

        // GET: Admin/Membership/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Membership ms = db.Memberships.Find(id);
            if (ms == null)
            {
                return HttpNotFound();
            }
            return View();
        }

        // POST: Admin/Membership/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Membership ms = db.Memberships.Find(id);
            db.Memberships.Remove(ms);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
