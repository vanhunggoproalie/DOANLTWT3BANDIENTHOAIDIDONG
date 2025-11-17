using DOANLTWT3BANDIENTHOAIDIDONG.Models;
using DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Areas.Admin.Controllers
{
    public class AdminUsersController : Controller
    {
        private QLBANDIENTHOAIDIDONGEntities db = new QLBANDIENTHOAIDIDONGEntities();

        public ActionResult Index()
        {
            // LỌC: Chỉ lấy những User có UserRole là "Admin"
            var adminUsers = db.Users
                .Where(u => u.UserRole == "Admin")
                .ToList();

            return View(adminUsers);
        }

        // GET: Admin/AdminUsers/Create
        public ActionResult Create()
        {
            // Khởi tạo một User trống
            return View(new User());
        }

        // POST: Admin/AdminUsers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Username,Password,UserRole")] User user)
        {
            // *** KIỂM TRA QUAN TRỌNG: CHỈ CHO PHÉP THÊM ROLE LÀ ADMIN ***
            if (user.UserRole != "Admin")
            {
                ModelState.AddModelError("UserRole", "Chỉ được phép tạo tài khoản với vai trò Admin.");
                // Giữ lại giá trị Role là "Customer" để người dùng thấy lỗi
                return View(user);
            }

            if (ModelState.IsValid)
            {
                if (db.Users.Any(u => u.Username == user.Username))
                {
                    ModelState.AddModelError("Username", "Tên đăng nhập đã tồn tại.");
                    return View(user);
                }

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Admin/AdminUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);

            if (user == null)
            {
                // Thử tìm kiếm với Trim() nếu Find(id) thất bại
                user = db.Users.SingleOrDefault(u => u.Username.Trim() == id.Trim());
            }

            if (user == null) return HttpNotFound();

            // KIỂM TRA: CHỈ CHO PHÉP SỬA nếu Role là Admin (dùng Trim())
            if (user.UserRole.Trim() != "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Chỉ được phép chỉnh sửa tài khoản Admin.");
            }

            // Đảm bảo dữ liệu User được truyền đi sạch sẽ (bỏ khoảng trắng thừa)
            // Điều này giúp cho việc hiển thị trong View chính xác
            user.Username = user.Username.Trim();
            user.Password = user.Password.Trim();
            user.UserRole = user.UserRole.Trim();

            // TRUYỀN KIỂU USER
            return View(user);
        }

        // POST: Admin/AdminUsers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        // SỬA: CHỈ NHẬN KIỂU USER
        public ActionResult Edit([Bind(Include = "Username,Password,UserRole")] User user)
        {
          
            user.Username = user.Username.Trim();
            user.Password = user.Password.Trim();
            user.UserRole = user.UserRole.Trim();

      
            var originalUser = db.Users.AsNoTracking()
                .FirstOrDefault(u => u.Username.Trim() == user.Username);

      
            if (originalUser != null && originalUser.UserRole.Trim() != "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Không được phép chỉnh sửa tài khoản Khách hàng.");
            }

            
            if (user.UserRole != "Admin")
            {
                ModelState.AddModelError("UserRole", "Bạn chỉ có thể chỉnh sửa Role Admin, và Role phải được giữ là Admin.");
                return View(user);
            }

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

          
            return View(user);
        }

        // GET: Admin/AdminUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            User user = db.Users.Find(id);

            // BỔ SUNG: Xử lý lỗi tìm kiếm nếu Find(id) thất bại do khoảng trắng
            if (user == null)
            {
                user = db.Users.SingleOrDefault(u => u.Username.Trim() == id.Trim());
            }

            if (user == null) return HttpNotFound();

            // KIỂM TRA (SỬA): Dùng .Trim()
            if (user.UserRole.Trim() != "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden, "Chỉ được phép xóa tài khoản Admin.");
            }

            return View(user);
        }

        // POST: Admin/AdminUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            User user = db.Users.Find(id);

            // BỔ SUNG: Xử lý lỗi tìm kiếm nếu Find(id) thất bại do khoảng trắng
            if (user == null)
            {
                user = db.Users.SingleOrDefault(u => u.Username.Trim() == id.Trim());
            }

            // KIỂM TRA (SỬA): Dùng .Trim()
            if (user != null && user.UserRole.Trim() == "Admin")
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)

        {

            if (disposing) db.Dispose();

            base.Dispose(disposing);

        }

    }
}
