using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class LoginVM
    {
        [Required]
        [Display(Name ="Ten Dang Nhap")]
        public string Username { get; set; }    

        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Mat Khau")]
        public string Password { get; set; }
    }
}