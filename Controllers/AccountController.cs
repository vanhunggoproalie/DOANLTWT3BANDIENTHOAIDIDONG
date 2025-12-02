using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;


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
                    ModelState.AddModelError("Username", "Tên Đăng Nhập Đã Tồn Tại");
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
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Login", "Account");   
        }
        public ActionResult Profile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string username = User.Identity.Name;

            // Lấy thông tin từ cả 2 bảng User và Customer
            var user = db.Users.SingleOrDefault(u => u.Username == username);
            var customer = db.Customers.SingleOrDefault(c => c.Username == username);

            if (user == null || customer == null)
            {
                // Nếu không tìm thấy thông tin, có thể redirect hoặc hiển thị lỗi
                TempData["ErrorMessage"] = "Không tìm thấy thông tin người dùng.";
                return RedirectToAction("Index", "Home");
            }

            // Map dữ liệu sang ViewModel
            var profileVM = new ProfileVM
            {
                // Từ bảng User
                Username = user.Username,
                UserRole = user.UserRole,

                // Từ bảng Customer
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                CustomerEmail = customer.CustomerEmail,
                CustomerAddress = customer.CustomerAddress
            };

            return View(profileVM);
            
        }


        [Authorize]
        public ActionResult EditProfile()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string username = User.Identity.Name;
            var customer = db.Customers.SingleOrDefault(c => c.Username == username);

            if (customer == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy thông tin khách hàng.";
                return RedirectToAction("Profile", "Account");
            }

            // Tạo ViewModel cho form chỉnh sửa
            var editProfileVM = new EditProfileVM
            {
                CustomerID = customer.CustomerID,
                CustomerName = customer.CustomerName,
                CustomerPhone = customer.CustomerPhone,
                CustomerEmail = customer.CustomerEmail,
                CustomerAddress = customer.CustomerAddress
            };

            return View(editProfileVM);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult EditProfile(EditProfileVM model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string username = User.Identity.Name;

            if (ModelState.IsValid)
            {
                try
                {
                    // Lấy thông tin customer từ database
                    var customer = db.Customers.SingleOrDefault(c => c.Username == username);

                    if (customer == null)
                    {
                        TempData["ErrorMessage"] = "Không tìm thấy thông tin khách hàng.";
                        return RedirectToAction("Profile", "Account");
                    }

                    // Cập nhật thông tin
                    customer.CustomerName = model.CustomerName;
                    customer.CustomerPhone = model.CustomerPhone;
                    customer.CustomerEmail = model.CustomerEmail;
                    customer.CustomerAddress = model.CustomerAddress;

                    // Cập nhật Session nếu cần
                    if (Session["CustomerDisplayName"] != null)
                    {
                        Session["CustomerDisplayName"] = model.CustomerName;
                    }

                    // Lưu thay đổi
                    db.SaveChanges();

                    TempData["SuccessMessage"] = "Cập nhật thông tin thành công!";
                    return RedirectToAction("Profile", "Account");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật: " + ex.Message);
                }
            }

            return View(model);
        }
        [Authorize]
        public ActionResult ChangePassword()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }

        // POST: Account/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ChangePassword(ChangePasswordVM model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            if (ModelState.IsValid)
            {
                string username = User.Identity.Name;
                var user = db.Users.SingleOrDefault(u => u.Username == username);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "Không tìm thấy thông tin người dùng.";
                    return RedirectToAction("Profile", "Account");
                }

                // Kiểm tra mật khẩu cũ
                if (user.Password != model.OldPassword)
                {
                    ModelState.AddModelError("OldPassword", "Mật khẩu cũ không đúng");
                    return View(model);
                }

                // Kiểm tra mật khẩu mới và xác nhận
                if (model.NewPassword != model.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Mật khẩu xác nhận không khớp");
                    return View(model);
                }

                // Cập nhật mật khẩu mới
                user.Password = model.NewPassword;
                db.SaveChanges();

                TempData["SuccessMessage"] = "Đổi mật khẩu thành công!";
                return RedirectToAction("Profile", "Account");
            }

            return View(model);
        }
        [Authorize]
        public ActionResult MyOrder(string filter = "all")
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            string username = User.Identity.Name;

            // Lấy CustomerID từ bảng Customer
            var customer = db.Customers.SingleOrDefault(c => c.Username == username);
            if (customer == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy thông tin khách hàng.";
                return RedirectToAction("Index", "Home");
            }

            int customerId = customer.CustomerID;

            // Lấy tất cả đơn hàng của khách
            var orders = db.Orders.Where(o => o.CustomerID == customerId).ToList();

            // Tạo ViewModel
            var model = new MyOrderVM
            {
                AllOrders = orders,
                ShippingOrders = orders.Where(o => o.ShippingMethod == "DangGiao" ||
                                                   o.ShippingMethod == "Đang Giao" ||
                                                   o.ShippingMethod == "Shipping").ToList(),
                CompletedOrders = orders.Where(o => o.ShippingMethod == "DaHoanThanh" ||
                                                    o.ShippingMethod == "Hoàn Thành" ||
                                                    o.ShippingMethod == "Completed").ToList()
            };

            ViewBag.Filter = filter; // để biết user đang xem tab nào

            return View(model);
        }
    }
}

