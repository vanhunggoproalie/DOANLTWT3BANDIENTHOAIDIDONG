using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class RegisterVM
    {
        [Required]
        [Display(Name ="Tên Đăng Nhập")]
        public string Username {  get; set; }   

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Mật Khẩu")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Xác Nhật Mật Khẩu")]
        [Compare("Password",ErrorMessage ="Mật Khẩu Xác Nhận Không Khớp")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name ="Họ Tên")]
        public string CustomerName {  get; set; }

        [Required]
        [Display(Name ="Số Điện Thoại")]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone {  get; set; }

        [Required]
        [Display(Name ="Email")]
        [DataType (DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Required]
        [Display(Name ="Địa Chỉ")]
        
        public string CustomerAddress { get; set; }

    }
}