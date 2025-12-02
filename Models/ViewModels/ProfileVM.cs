using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class ProfileVM
    {
        public string Username { get; set; }
        public string UserRole { get; set; }

        // Từ bảng Customer
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
    }
}