namespace Supermarket.EneityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ReturnGoods
    {
        public int ReturnGoodsID { get; set; }

        public int ProductID { get; set; }

        public decimal ReturnAmount { get; set; }

        [StringLength(200)]
        public string Remark { get; set; }

        public DateTime CheckInTime { get; set; }

        public virtual Product Product { get; set; }
    }
}
