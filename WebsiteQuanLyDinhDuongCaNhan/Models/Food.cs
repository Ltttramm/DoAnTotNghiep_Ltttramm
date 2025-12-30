namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Food()
        {
            UserMeals = new HashSet<UserMeal>();
        }

        public int FoodID { get; set; }

        [StringLength(255)]
        public string FoodName { get; set; }

        public double? Calories { get; set; }

        public double? Protein { get; set; }

        public double? Carbs { get; set; }

        public double? Fat { get; set; }

        public double? Fiber { get; set; }

        public double? Sugar { get; set; }

        [StringLength(255)]
        public string Category { get; set; }

        [StringLength(255)]
        public string ImageURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMeal> UserMeals { get; set; }
    }
}
