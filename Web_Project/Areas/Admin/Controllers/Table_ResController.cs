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
    public class Table_ResController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Table_Res
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Table_Res.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Table_Res/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Res tb = db.Table_Res.Find(id);
            if (tb == null)
            {
                return HttpNotFound();
            }
            return View(tb);
        }

        // GET: Admin/Table_Res/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Table_Res/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,location,quantity,status")] Table_Res tb)
        {
            if (ModelState.IsValid)
            {
                db.Table_Res.Add(tb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tb);
        }

        // GET: Admin/Table_Res/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Res tb = db.Table_Res.Find(id);
            if (tb == null)
            {
                return HttpNotFound();
            }
            return View(tb);
        }

        // POST: Admin/Table_Res/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,name,location,quantity,status")] Table_Res tb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tb);
        }

        // GET: Admin/Table_Res/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Table_Res tb = db.Table_Res.Find(id);
            if (tb == null)
            {
                return HttpNotFound();
            }
            return View(tb);
        }

        // POST: Admin/Table_Res/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Table_Res tb = db.Table_Res.Find(id);
            db.Table_Res.Remove(tb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
