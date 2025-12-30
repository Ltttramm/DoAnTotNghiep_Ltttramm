namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MealPlan
    {
        public int MealPlanID { get; set; }

        public int? UserID { get; set; }

        [StringLength(50)]
        public string MealType { get; set; }

        [Column(TypeName = "text")]
        public string SuggestedFoods { get; set; }

        public DateTime? CreatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
