using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    [Table("HOSO_DINHDUONG")]
    public partial class NutritionProfile
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NutritionProfile()
        {
            NutrientRequirements = new HashSet<NutrientRequirement>();
            UserMeals = new HashSet<UserMeal>();
        }

        [Key] // Khóa chính - Giải quyết lỗi bạn đang gặp
        [Column("maHoSo")]
        public long ProfileID { get; set; }

        [Column("maNguoiDung")]
        public int? UserID { get; set; }

        [Column("canNang")]
        public decimal? Weight { get; set; }

        [Column("chieuCao")]
        public decimal? Height { get; set; }

        [Column("gioiTinh")]
        [StringLength(10)]
        public string Gender { get; set; }

        [Column("tuoi")]
        public int? Age { get; set; }

        [Column("mucDoHoatDong")]
        [StringLength(50)]
        public string ActivityLevel { get; set; }

        [Column("mucTieu")]
        [StringLength(50)]
        public string Goal { get; set; }

        [Column("canNangMucTieu")]
        public decimal? TargetWeight { get; set; }

        [Column("soNgayThucHien")]
        public int? DurationDays { get; set; }

        [Column("tongCalo")]
        public int? TotalRequiredCalories { get; set; }

        [Column("ngayTao")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        // Quan hệ với bảng Người dùng
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        // Quan hệ với bảng Nhu cầu chất
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NutrientRequirement> NutrientRequirements { get; set; }

        // Quan hệ với bảng Nhật ký bữa ăn (Sửa lại UserMeal map tới bảng NHATKYBUAAN)
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMeal> UserMeals { get; set; }
    }
}