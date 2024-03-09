using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using Web_Project.Models;

namespace Web_Project.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Category
        public ActionResult Index(int? page)
        {
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            var list = db.Categories.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Category/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category ct = db.Categories.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // GET: Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Category ct)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(ct);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ct);
        }

        // GET: Admin/Category/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category ct = db.Categories.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // POST: Admin/Category/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Category ct)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ct).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ct);
        }

        // GET: Admin/Category/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category ct = db.Categories.Find(id);
            if (ct == null)
            {
                return HttpNotFound();
            }
            return View(ct);
        }

        // POST: Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Category ct = db.Categories.Find(id);
            db.Categories.Remove(ct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
