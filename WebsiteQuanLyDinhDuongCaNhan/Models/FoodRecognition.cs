namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FoodRecognition")]
    public partial class FoodRecognition
    {
        [Key]
        public int RecognitionID { get; set; }

        public int? UserID { get; set; }

        [StringLength(255)]
        public string ImageURL { get; set; }

        [Column(TypeName = "text")]
        public string RecognizedFoods { get; set; }

        public double? Confidence { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
