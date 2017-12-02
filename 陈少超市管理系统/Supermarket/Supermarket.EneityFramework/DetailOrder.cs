namespace Supermarket.EneityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DetailOrder")]
    public partial class DetailOrder
    {
        public int DetailOrderID { get; set; }

        public int ProductID { get; set; }

        public int OrderFormID { get; set; }

        public decimal Amount { get; set; }

        [Column(TypeName = "money")]
        public decimal subtotal { get; set; }

        public virtual Orders Orders { get; set; }

        public virtual Product Product { get; set; }
    }
}
