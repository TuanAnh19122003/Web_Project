using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Web_Project.Models;

namespace Web_Project.Areas.Admin.Controllers
{
    public class DealController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Deal
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Deals.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Deal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal dl = db.Deals.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }

        // GET: Admin/Deal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Deal/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,img,name,description")] Deal dl, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Theme/img/deal"), newFileName);
                    img.SaveAs(path);
                    dl.img = newFileName;
                }

                // Save the rest of the model and perform other necessary actions
                db.Deals.Add(dl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(dl);
        }

        // GET: Admin/Deal/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal dl = db.Deals.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            
            return View(dl);
        }

        // POST: Admin/Deal/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,img,name,description")] Deal dl, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (img != null && img.ContentLength > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Theme/img/deal"), newFileName);
                    img.SaveAs(path);
                    dl.img = newFileName;
                }

                db.Entry(dl).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(dl);
        }

        // GET: Admin/Deal/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Deal dl = db.Deals.Find(id);
            if (dl == null)
            {
                return HttpNotFound();
            }
            return View(dl);
        }

        // POST: Admin/Deal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Deal dl = db.Deals.Find(id);
            db.Deals.Remove(dl);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
