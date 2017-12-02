namespace Supermarket.EneityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Purchases
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Purchases()
        {
            DetailePurchases = new HashSet<DetailePurchases>();
        }

        public int PurchasesID { get; set; }

        [Column(TypeName = "money")]
        public decimal PurchasesTotal { get; set; }

        public int AdminID { get; set; }

        [StringLength(50)]
        public string Reamrk { get; set; }

        public DateTime PurchasesTime { get; set; }

        public virtual AdminInfo AdminInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailePurchases> DetailePurchases { get; set; }
    }
}
