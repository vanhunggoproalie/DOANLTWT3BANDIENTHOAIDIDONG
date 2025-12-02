using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class CheckoutVM
    {
        public CheckoutVM()
        {
            CartItems = new List<CartItem>();
            OrderDetails = new List<OrderDetail>();
            OrderDate = DateTime.Now;
            PaymentStatus = "Chưa thanh toán";
        }
        public List<CartItem> CartItems { get; set; }
        public int CustomerID { get; set; }
        [Display(Name="Ngay Dat Hang")]
        public System.DateTime OrderDate{get;set;}

        [Display(Name ="Tong Gia Tri")]
        public decimal TotalAmount {  get; set; }

        [Display(Name = "Trạng Thái Thanh Toán")]
        public string PaymentStatus {  get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        [Display(Name = "Phương Thức Thanh Toán")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn phương thức giao hàng")]
        [Display(Name = "Phương Thức Giao Hàng")]
        public string ShippingMethod {  get; set; }


        [Required(ErrorMessage = "Địa chỉ giao hàng không được để trống")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        [Display(Name = "Địa Chỉ Giao Hàng")]

        public string ShippingAddress { get; set; }

        public string Username { get; set; }    

        public List<OrderDetail> OrderDetails { get; set; }

    }
}