using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels;


namespace DOANLTWT3BANDIENTHOAIDIDONG.Controllers
{
    public class AccountController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db = new QLBANDIENTHOAIDIDONGEntities();
        // GET: Addcount
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = db.Users.SingleOrDefault(u => u.Username == model.Username);
                if (existingUser != null)
                {
                    ModelState.AddModelError(" Username", "Tên Đăng Nhập Đã Tồn Tại");
                    return View(model);
                }
                var user = new User
                {
                    Username = model.Username,
                    Password = model.Password,
                    UserRole = "Customer"
                };
                db.Users.Add(user);
                var customer = new Customer
                {
                    CustomerName = model.CustomerName,
                    CustomerEmail = model.CustomerEmail,
                    
                    CustomerPhone = model.CustomerPhone,
                    CustomerAddress = model.CustomerAddress,
                    Username = model.Username,

                };
                db.Customers.Add(customer);
                db.SaveChanges();
                FormsAuthentication.SetAuthCookie(model.Username, false);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = db.Users.SingleOrDefault(u => u.Username == model.Username
                && u.Password == model.Password
                && u.UserRole == "Customer");
                if (user != null)
                {
                    var customer = db.Customers.SingleOrDefault(c => c.Username == user.Username);
                    Session["Username"] = user.Username;
                    Session["Userrole"] = user.UserRole;
                    Session["CustomerDisplayName"] = customer.CustomerName;
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "ten dang nhap hoac mat khau khong dung.");

                }

            }
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "Account");   
        }
    }
}
