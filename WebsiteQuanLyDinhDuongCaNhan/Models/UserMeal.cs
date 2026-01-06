//namespace WebsiteQuanLyDinhDuongCaNhan.Models
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;
//    using System.Data.Entity.Spatial;

//    public partial class UserMeal
//    {
//        [Key]
//        public int MealID { get; set; }

//        public int? UserID { get; set; }

//        [Column(TypeName = "date")]
//        public DateTime? Date { get; set; }

//        [StringLength(50)]
//        public string MealType { get; set; }

//        public int? FoodID { get; set; }

//        public double? Quantity { get; set; }

//        public virtual Food Food { get; set; }

//        public virtual User User { get; set; }
//    }
//}
namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("NHATKYBUAAN")]
    public partial class UserMeal
    {
        [Key]
        [Column("maNhatKyBuaAn")]
        public long MealID { get; set; }

        [Column("maHoSo")]
        public long? ProfileID { get; set; }

        [Column("ngayAn", TypeName = "date")]
        public DateTime? Date { get; set; }

        [Column("buaAn")]
        [StringLength(50)]
        public string MealType { get; set; }

        [Column("maMon")]
        public int? FoodID { get; set; }

        [Column("khoiLuong")]
        public double? Quantity { get; set; }

        // Navigation Properties
        [ForeignKey("FoodID")]
        public virtual Food Food { get; set; }

        [ForeignKey("ProfileID")]
        public virtual NutritionProfile NutritionProfile { get; set; }

        // ✅ SỬA DÒNG NÀY - Thêm thuộc tính UserID và User
        [NotMapped] // Nếu không có cột maNguoiDung trong bảng NHATKYBUAAN
        public virtual User User { get; set; }
    }
}