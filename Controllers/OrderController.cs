using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Controllers
{
    public class OrderController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db = new QLBANDIENTHOAIDIDONGEntities();

        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CheckOut()
        {
            var cart = Session["Cart"] as List<CartItem>;
            //**Kiem Tra Gio Hang**
            if (cart == null || !cart.Any())
            {
                TempData["Error"] = "Giỏ hàng của bạn đang trống. Vui lòng mua hàng trước.";
                return RedirectToAction("Index", "Home");

            }

            var model = new CheckoutVM
            {
                CartItems = cart,
                TotalAmount = cart.Sum(item => item.TotalPrice),
                OrderDate = DateTime.Now,
                ShippingAddress =  "Vui lòng nhập địa chỉ giao hàng",
                CustomerID = 0,
                Username = User.Identity.IsAuthenticated ? User.Identity.Name : null,
            };
           
            return View(model);
        }
        [HttpPost]
        public ActionResult CheckOut(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var cart = Session["Cart"] as List<CartItem>;
                if (cart == null || !cart.Any())
                {
                    return RedirectToAction("Index", "Home");
                }
                var user = db.Users.SingleOrDefault(u => u.Username == User.Identity.Name);
                if (user == null) { return RedirectToAction("Login", "Account"); }
                var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
                if (customer == null) { return RedirectToAction("Login", "Account"); }
                if (model.PaymentMethod == "Paypal")
                {
                    return RedirectToAction("PaymentWithPayPal", "PayPal", model);
                }
                string paymentStatus = "Chua Thanh Toan";
                switch (model.PaymentMethod)
                {
                    case "Tien Mat": paymentStatus = "Thanh Toan Tien Mat"; break;
                    case "Paypal": paymentStatus = "Paypal"; break;
                    case "Mua Truoc Tra Sau": paymentStatus = "Tra gop"; break;
                    default: paymentStatus = "Chua Thanh Toan "; break;
                }
                var order = new Order
                {
                    CustomerID = customer.CustomerID,
                    OrderDate = model.OrderDate,
                    TotalAmount = model.TotalAmount,
                    PaymentStatus = paymentStatus,
                    PaymentMethod = model.PaymentMethod,
                    ShippingAddress = model.ShippingAddress,
                    ShippingMethod = model.ShippingMethod,
                    OrderDetails = cart.Select(item => new OrderDetail
                    {
                        ProductID = item.ProductID,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice,
                    }).ToList()
                };
                db.Orders.Add(order);
                db.SaveChanges();

                Session["Cart"] = null;
                return RedirectToAction("OrderSucess", new { id = order.OrderID });
            }
            return View(model);
        }

       
        public ActionResult OrderSucess(int id)
        {
            var order = db.Orders.Include("OrderDetails").SingleOrDefault(o => o.OrderID == id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View("OrderSuccess", new { id = order.OrderID });
        }
    }
}