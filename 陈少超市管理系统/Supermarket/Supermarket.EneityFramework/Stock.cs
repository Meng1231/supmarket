namespace Supermarket.EneityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stock")]
    public partial class Stock
    {
        public int StockID { get; set; }

        public int ProductID { get; set; }

        public DateTime StorageTime { get; set; }

        public decimal CommodityStockAmount { get; set; }

        public decimal StockUp { get; set; }

        public decimal StockDown { get; set; }

        public DateTime AlterTime { get; set; }

        public virtual Product Product { get; set; }
    }
}
