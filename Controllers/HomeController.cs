using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels;
using System.Net;



namespace DOANLTWT3BANDIENTHOAIDIDONG.Controllers
{
    public class HomeController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db= new QLBANDIENTHOAIDIDONGEntities();
        public ActionResult Index(string searchTerm, int? page)
        {
            var model  = new HomeProductVM();
            var products = db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p=>p.ProductName.Contains(searchTerm) ||
                           p.ProductDecription.Contains(searchTerm)||
                           p.CAtegory.CategoryName.Contains(searchTerm)
                );
            }
            int pageNumber = page ?? 1;
            int pageSize = 6;
            model.FeaturedProducts = products.OrderByDescending(p=>p.OrderDetails.Count()).Take(8).ToList();
            model.NewProducts = products.OrderBy(p=>p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber, pageSize);
            return View(model);
        }
        [ChildActionOnly]
        public ActionResult HeaderMenu()
        {
            // 1. Lấy thông tin Giỏ hàng
            var cartService = new CartService(Session);
            var cart = cartService.getCart();
            int cartCount = cart.Items.Count();

            // 2. Lấy thông tin Đăng nhập
            var customerName = Session["CustomerName"] as string;

            var viewModel = new PersonalMenuVM
            {
                IsLoggedIn = !string.IsNullOrEmpty(customerName),
                Username = customerName,
                CartCount = cartCount
            };

            // Trả về Partial View chỉ chứa phần Header Actions
            return PartialView("_HeaderMenuActions", viewModel);
        }

        public ActionResult ShoppingCart()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult HomeSearch(string searchTerm, int? page)
        {
            var model = new HomeProductVM();
            var products = db.Products.AsQueryable();
            if (!string.IsNullOrEmpty(searchTerm))
            {
                model.SearchTerm = searchTerm;
                products = products.Where(p => p.ProductName.Contains(searchTerm) ||
                p.ProductDecription.Contains(searchTerm) ||
                p.CAtegory.CategoryName.Contains(searchTerm));
            }
            int pageNumber = page?? 1;
            int pageSize = 6;
            model.FeaturedProducts = products.OrderByDescending(p=>p.OrderDetails.Count()).Take(10).ToList();
            model.NewProducts = products.OrderByDescending(p => p.OrderDetails.Count()).Take(20).ToPagedList(pageNumber,pageSize);
            return View(model);
        }
        public ActionResult ProductDetails(int? id, int? quantity, int?page)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product pro = db.Products.Find(id);
            if (pro == null)
            {
                return HttpNotFound();
            }

            var products = db.Products.Where(p=>p.CategoryID == pro.CategoryID && p.ProductID!=pro.ProductID).AsQueryable();
            ProductDetailsVM model = new ProductDetailsVM();
            int pageNumber = page ?? 1;
            int pageSize=  model.PageSize;
            model.product = pro;
            model.RelatedProducts = products.OrderBy(p => p.ProductID).Take(8).ToPagedList(pageNumber, pageSize);
            model.TopProducts = products.OrderByDescending(p=>p.OrderDetails.Count()).Take(8).ToPagedList(pageNumber,pageSize);
            if (quantity.HasValue)
            {
                model.quantity = quantity.Value;
            }
            return View(model);
        }
    }
}