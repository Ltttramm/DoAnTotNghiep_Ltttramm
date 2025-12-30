namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class UserMeal
    {
        [Key]
        public int MealID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? Date { get; set; }

        [StringLength(50)]
        public string MealType { get; set; }

        public int? FoodID { get; set; }

        public double? Quantity { get; set; }

        public virtual Food Food { get; set; }

        public virtual User User { get; set; }
    }
}
