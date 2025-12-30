namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CalorieTracking")]
    public partial class CalorieTracking
    {
        [Key]
        public int RecordID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        public double? CaloriesConsumed { get; set; }

        public double? CaloriesBurned { get; set; }

        public virtual User User { get; set; }
    }
}
