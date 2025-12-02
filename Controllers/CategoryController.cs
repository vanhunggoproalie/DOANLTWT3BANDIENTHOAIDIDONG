using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;
using DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels;


namespace DOANLTWT3BANDIENTHOAIDIDONG.Controllers
{
    public class CategoryController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db = new QLBANDIENTHOAIDIDONGEntities();
        // GET: Category
        public ActionResult Index()
        {
            var model = new ProductCategoryVM();
            model.Categories = db.CAtegories.ToList(); // Lấy tất cả danh mục
            model.Products = db.Products.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult FilterProducts(int[] categoryIds)
        {
            var products = db.Products.AsQueryable();

            
            if (categoryIds != null && categoryIds.Length > 0)
            {
                products = products.Where(p => categoryIds.Contains(p.CategoryID));
            }

            // Trả về Partial View chứa danh sách sản phẩm đã lọc
            return PartialView("_ProductListPartial", products.ToList());
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
        public ActionResult MenuCategory()
        {
            var model = new ProductCategoryVM();
            model.Categories = db.CAtegories.ToList(); // Lấy tất cả danh mục
            model.Products = db.Products.ToList();
            return View(model);
        }
    }
}