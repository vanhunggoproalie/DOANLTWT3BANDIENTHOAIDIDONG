using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;


namespace DOANLTWT3BANDIENTHOAIDIDONG.Controllers
{
    public class CategoryController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db = new QLBANDIENTHOAIDIDONGEntities();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.CAtegories.ToList());
        }
        public ActionResult Add()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CAtegory category = db.CAtegories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }
    }
}