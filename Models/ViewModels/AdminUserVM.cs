using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class AdminUserVM
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 50 ký tự.")]
        [Display(Name = "Tên Đăng Nhập")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Mật khẩu phải từ 6 đến 20 ký tự.")]
        [Display(Name = "Mật Khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        // Trường này sẽ bị ẩn trong View, nhưng cần cho logic POST
        public string UserRole { get; set; } = "Admin";
    }
}