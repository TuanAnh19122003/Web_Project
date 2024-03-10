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
    
    public class PositionController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Position
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Positions.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Position/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position pos = db.Positions.Find(id);
            if (pos == null)
            {
                return HttpNotFound();
            }
            return View(pos);
        }

        // GET: Admin/Position/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Position/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] Position pos)
        {
            if (ModelState.IsValid)
            {
                db.Positions.Add(pos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pos);
        }

        // GET: Admin/Position/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Position pos = db.Positions.Find(id);
            if (pos == null)
            {
                return HttpNotFound();
            }
            return View(pos);
        }

        // POST: Admin/Position/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] Position pos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pos);
        }

        // GET: Admin/Position/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            role Role = db.roles.Find(id);
            if (Role == null)
            {
                return HttpNotFound();
            }
            return View(Role);
        }

        // POST: Admin/Position/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Position pos = db.Positions.Find(id);
            db.Positions.Remove(pos);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
