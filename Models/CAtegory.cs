

namespace DOANLTWT3BANDIENTHOAIDIDONG.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAtegory
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CAtegory()
        {
            this.Products = new HashSet<Product>();
        }
    
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
