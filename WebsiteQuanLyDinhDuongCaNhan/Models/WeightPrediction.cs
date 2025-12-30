namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WeightPrediction")]
    public partial class WeightPrediction
    {
        [Key]
        public int PredictionID { get; set; }

        public int? UserID { get; set; }

        public double? PredictedWeight { get; set; }

        [Column(TypeName = "date")]
        public DateTime? PredictionDate { get; set; }

        public double? Accuracy { get; set; }

        public virtual User User { get; set; }
    }
}
