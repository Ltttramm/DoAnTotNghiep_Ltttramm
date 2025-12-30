namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class WeeklyMealPlan
    {
        [Key]
        public int PlanID { get; set; }

        public int? UserID { get; set; }

        [Column(TypeName = "date")]
        public DateTime? WeekStartDate { get; set; }

        [Column(TypeName = "text")]
        public string MealSchedule { get; set; }

        public virtual User User { get; set; }
    }
}
