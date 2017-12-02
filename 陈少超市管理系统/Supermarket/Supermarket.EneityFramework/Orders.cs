namespace Supermarket.EneityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Orders
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Orders()
        {
            DetailOrder = new HashSet<DetailOrder>();
        }

        [Key]
        public int OrderFormID { get; set; }

        public int CustomsID { get; set; }

        [Column(TypeName = "money")]
        public decimal SunPrice { get; set; }

        public DateTime? CheckInTime { get; set; }

        [Required]
        [StringLength(20)]
        public string Way { get; set; }

        public int? AdminID { get; set; }

        public virtual AdminInfo AdminInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DetailOrder> DetailOrder { get; set; }

        public virtual User User { get; set; }
    }
}
