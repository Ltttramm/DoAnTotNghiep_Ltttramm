namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WeightTracking")]
    public partial class WeightTracking
    {
        [Key]
        public int RecordID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? RecordedDate { get; set; }

        public double? Weight { get; set; }

        public virtual User User { get; set; }
    }
}
