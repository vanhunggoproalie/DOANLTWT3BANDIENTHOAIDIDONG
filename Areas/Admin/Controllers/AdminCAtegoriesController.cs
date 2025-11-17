using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DOANLTWT3BANDIENTHOAIDIDONG.Models;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Areas.Admin.Controllers
{
    public class AdminCAtegoriesController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db = new QLBANDIENTHOAIDIDONGEntities();

        // GET: Admin/CAtegories
        public ActionResult Index()
        {
            return View(db.CAtegories.ToList());
        }

        // GET: Admin/CAtegories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAtegory cAtegory = db.CAtegories.Find(id);
            if (cAtegory == null)
            {
                return HttpNotFound();
            }
            return View(cAtegory);
        }

        // GET: Admin/CAtegories/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CAtegories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName")] CAtegory cAtegory)
        {
            if (ModelState.IsValid)
            {
                db.CAtegories.Add(cAtegory);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cAtegory);
        }

        //GET: Admin/CAtegories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAtegory cAtegory = db.CAtegories.Find(id);
            if (cAtegory == null)
            {
                return HttpNotFound();
            }
            return View(cAtegory);
        }
        //public ActionResult Edit(int? id)
        //{
        //    return View(Details(id));
        //}

        // POST: Admin/CAtegories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName")] CAtegory cAtegory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cAtegory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cAtegory);
        }

        // GET: Admin/CAtegories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAtegory cAtegory = db.CAtegories.Find(id);
            if (cAtegory == null)
            {
                return HttpNotFound();
            }
            return View(cAtegory);
        }
        //public ActionResult Delete(int? id)
        //{
        //    return View(Details(id));
        //}

        // POST: Admin/CAtegories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CAtegory cAtegory = db.CAtegories.Find(id);
            db.CAtegories.Remove(cAtegory);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
