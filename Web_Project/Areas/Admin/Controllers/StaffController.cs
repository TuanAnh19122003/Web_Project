using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Web_Project.Controllers;
using Web_Project.Models;

namespace Web_Project.Areas.Admin.Controllers
{
    public class StaffController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Staff
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Staffs.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Staff/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff nv = db.Staffs.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }

        // GET: Admin/Staff/Create
        public ActionResult Create()
        {
            ViewBag.position_id = new SelectList(db.Positions, "id", "name");
            return View();
        }

        // POST: Admin/Staff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,img,name,position_id,phone_number,date_of_birth,address,gender")] Staff nv, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Theme/img/staff"), newFileName);
                    img.SaveAs(path);
                    nv.img = newFileName;
                }

                // Save the rest of the model and perform other necessary actions
                db.Staffs.Add(nv);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.position_id = new SelectList(db.Positions, "id", "name");
            return View(nv);
        }

        // GET: Admin/Staff/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff nv = db.Staffs.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            ViewBag.position_id = new SelectList(db.Positions, "id", "name", nv.position_id);
            return View(nv);
        }

        // POST: Admin/Staff/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,img,name,position_id,phone_number,date_of_birth,address,gender")] Staff nv, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (img != null && img.ContentLength > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Theme/img/staff"), newFileName);
                    img.SaveAs(path);
                    nv.img = newFileName;
                }

                db.Entry(nv).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.position_id = new SelectList(db.Positions, "id", "name", nv.position_id);
            return View(nv);
        }
        // POST: Admin/Staff/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff nv = db.Staffs.Find(id);
            if (nv == null)
            {
                return HttpNotFound();
            }
            return View(nv);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Staff nv = db.Staffs.Find(id);
            db.Staffs.Remove(nv);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
