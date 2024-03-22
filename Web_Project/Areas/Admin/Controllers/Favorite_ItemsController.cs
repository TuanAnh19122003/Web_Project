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
    public class Favorite_ItemsController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Favorite_Items
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Favorite_Items.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Favorite_Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite_Items fi = db.Favorite_Items.Find(id);
            if (fi == null)
            {
                return HttpNotFound();
            }
            return View(fi);
        }

        // GET: Admin/Favorite_Items/Create
        public ActionResult Create()
        {
            ViewBag.member_id = new SelectList(db.Memberships, "id", "name");
            ViewBag.item_id = new SelectList(db.Menus, "id", "name");
            return View();
        }

        // POST: Admin/Favorite_Items/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,member_id,item_id")] Favorite_Items fi)
        {
            if (ModelState.IsValid)
            {
                db.Favorite_Items.Add(fi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.member_id = new SelectList(db.Favorite_Items, "id", "name", fi.member_id);
            ViewBag.item_id = new SelectList(db.Menus, "id", "name", fi.item_id);
            return View(fi);
        }

        // GET: Admin/Favorite_Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite_Items fi = db.Favorite_Items.Find(id);
            if (fi == null)
            {
                return HttpNotFound();
            }
            return View(fi);
        }

        // POST: Admin/Favorite_Items/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,member_id,item_id")] Favorite_Items fi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fi);
        }

        // GET: Admin/Favorite_Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Favorite_Items fi = db.Favorite_Items.Find(id);
            if (fi == null)
            {
                return HttpNotFound();
            }
            return View(fi);
        }

        // POST: Admin/Favorite_Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Favorite_Items fi = db.Favorite_Items.Find(id);
            db.Favorite_Items.Remove(fi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
