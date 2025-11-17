using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Controllers
{
    public class CartController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db = new QLBANDIENTHOAIDIDONGEntities();
        private CartService GetCartService()
        {
            return new CartService(Session);
        }

        // GET: Cart
        public ActionResult Index()
        {
            var cart = GetCartService().getCart();
            return View(cart);
        }
        public ActionResult AddToCart(int id, int quantity = 1)
        {
            var product =db.Products.Find(id);
            if (product != null)
            {
                var cartService = GetCartService();
                cartService.getCart().AddItem(product.ProductID, product.ProductImage, product.ProductName, product.ProductPrice,
                    quantity, product.CAtegory.CategoryName);
            }
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromCart(int id)
        {
            var cartService=GetCartService();
            cartService.getCart().RemoveItem(id);
            return RedirectToAction("Index");
        }
        public ActionResult ClearCart()
        {
            GetCartService().ClearCart();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult UpdateQuantity(int id ,int quantity)
        {
            var cartService =GetCartService();
            cartService.getCart().UpdateQuantity(id, quantity);
            return RedirectToAction("Index"); 
        }
    }
}