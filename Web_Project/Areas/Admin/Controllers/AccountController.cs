using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using PagedList;
using Web_Project.Models;

namespace Web_Project.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private Restaurant_Web_DataEntities db = new Restaurant_Web_DataEntities();
        // GET: Admin/Account
        public ActionResult Index(int? page)
        {
            int pageSize = 4;
            int pageNumber = (page ?? 1);
            var list = db.Accounts.OrderBy(b => b.id).ToPagedList(pageNumber, pageSize);
            return View(list);
        }

        // GET: Admin/Account/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account acc = db.Accounts.Find(id);
            if (acc == null)
            {
                return HttpNotFound();
            }
            return View(acc);
        }

        // GET: Admin/Account/Create
        public ActionResult Create()
        {
            ViewBag.role_id = new SelectList(db.roles, "id", "name");
            return View();
        }

        // POST: Admin/Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,img,account1,password,role_id,register_date,phone_number")] Account acc, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                if (img != null && img.ContentLength > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Theme/img/Foods"), newFileName);
                    img.SaveAs(path);
                    acc.img = newFileName;
                }

                // Save the rest of the model and perform other necessary actions
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.role_id = new SelectList(db.roles, "id", "name", acc.role_id);
            return View(acc);
        }

        // GET: Admin/Account/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account acc = db.Accounts.Find(id);
            if (acc == null)
            {
                return HttpNotFound();
            }
            ViewBag.role_id = new SelectList(db.roles, "id", "name", acc.role_id);
            return View(acc);
        }

        // POST: Admin/Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,img,account1,password,role_id,register_date,phone_number")] Account acc, HttpPostedFileBase img)
        {
            if (ModelState.IsValid)
            {
                // Handle file upload
                if (img != null && img.ContentLength > 0)
                {
                    var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(img.FileName);
                    var path = Path.Combine(Server.MapPath("~/Theme/img/Foods"), newFileName);
                    img.SaveAs(path);
                    acc.img = newFileName;
                }

                db.Entry(acc).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.role_id = new SelectList(db.roles, "id", "name", acc.role_id);
            return View(acc);
        }

        // GET: Admin/Account/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account acc = db.Accounts.Find(id);
            if (acc == null)
            {
                return HttpNotFound();
            }
            return View(acc);
        }

        // POST: Admin/Account/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account acc = db.Accounts.Find(id);
            db.Accounts.Remove(acc);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
