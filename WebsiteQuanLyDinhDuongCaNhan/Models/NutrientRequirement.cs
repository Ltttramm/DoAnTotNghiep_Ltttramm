using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    [Table("NHUCAUCHAT")]
    public partial class NutrientRequirement
    {
        [Key] // Khóa chính - Cần thiết để không bị lỗi tương tự NutritionProfile
        [Column("maNhuCauChat")]
        public long RequirementID { get; set; }

        [Column("maHoSo")]
        public long? ProfileID { get; set; }

        [Column("proteinCanNgay")]
        public decimal? DailyProtein { get; set; }

        [Column("carbCanNgay")]
        public decimal? DailyCarb { get; set; }

        [Column("vitaminCanNgay")]
        public decimal? DailyVitamin { get; set; }

        [Column("chatXoCanNgay")]
        public decimal? DailyFiber { get; set; }

        [Column("chatBeoCanNgay")]
        public decimal? DailyFat { get; set; }

        [Column("chiSoBMI")]
        public decimal? BMICalculated { get; set; }

        [Column("chiSoTDEE")]
        public int? TDEECalculated { get; set; }

        // Quan hệ ngược lại với Hồ sơ dinh dưỡng
        [ForeignKey("ProfileID")]
        public virtual NutritionProfile NutritionProfile { get; set; }
    }
}