using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class MyOrderVM
    {
        public List<Order> AllOrders { get; set; }
        public List<Order> ShippingOrders { get; set; }
        public List<Order> CompletedOrders { get; set; }
    }
}