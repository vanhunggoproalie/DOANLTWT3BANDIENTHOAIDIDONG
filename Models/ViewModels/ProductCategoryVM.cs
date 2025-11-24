using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models.ViewModels
{
    public class ProductCategoryVM
    {
        public List<CAtegory> Categories { get; set; } 

        public List<Product> Products { get; set; }
    }
}