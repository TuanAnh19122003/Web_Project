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
    public class ContactController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Contact
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Contacts.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact ct = db.Contacts.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // GET: Admin/Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Contact/Create
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,name,email,phone_number,request_date,Note")] Contact ct)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(ct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ct);
        }

        // GET: Admin/Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact ct = db.Contacts.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // POST: Admin/Contact/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,email,phone_number,request_date,Note")] Contact ct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ct);
        }

        // GET: Admin/Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact ct = db.Contacts.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // POST: Admin/Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact ct = db.Contacts.Find(id);
            db.Contacts.Remove(ct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
