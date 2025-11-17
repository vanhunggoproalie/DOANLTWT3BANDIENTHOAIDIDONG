using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class CheckoutVM
    {
        public List<CartItem> CartItems { get; set; }
        public int CustomerID { get; set; }
        [Display(Name="Ngay Dat Hang")]
        public System.DateTime OrderDate{get;set;}

        [Display(Name ="Tong Gia Tri")]
        public decimal TotalAmount {  get; set; }

        [Display(Name ="Trang Thai Thanh Toan ")]
        public string PaymentStatus {  get; set; }

        [Display(Name ="Phuong Thuc Thanh Toan")]
        public string PaymentMethod { get; set; }

        [Display(Name ="Phuong Thuc Giao Hang ")]
        public string ShippingMethod {  get; set; }

        [Display(Name = "Dia Chi Giao Hang")]
        public string ShippingAddress { get; set; }

        public string Username { get; set; }    

        public List<OrderDetail> OrderDetails { get; set; }

    }
}