namespace Supermarket.EneityFramework
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SysLog")]
    public partial class SysLog
    {
        [Key]
        public int LogID { get; set; }

        [StringLength(500)]
        public string Behavior { get; set; }

        public int? FK_TypeID { get; set; }

        public int? FK_AdminID { get; set; }

        public string Parameters { get; set; }

        [StringLength(20)]
        public string IP { get; set; }

        public DateTime? CheckInTime { get; set; }

        public string Exception { get; set; }

        public byte? IsException { get; set; }

        public virtual AdminInfo AdminInfo { get; set; }

        public virtual LogDic LogDic { get; set; }
    }
}
