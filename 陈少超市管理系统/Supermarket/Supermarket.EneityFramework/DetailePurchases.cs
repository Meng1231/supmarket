namespace Supermarket.EneityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DetailePurchases
    {
        public int DetailePurchasesID { get; set; }

        public int PurchasesID { get; set; }

        public int ProductID { get; set; }

        public decimal ProductAmount { get; set; }

        [StringLength(50)]
        public string Reamrk { get; set; }

        public virtual Product Product { get; set; }

        public virtual Purchases Purchases { get; set; }
    }
}
