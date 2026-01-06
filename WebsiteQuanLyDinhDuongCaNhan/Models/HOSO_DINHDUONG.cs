using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    [Table("HOSO_DINHDUONG")]
    public class HosoDinhDuong
    {
        [Key]
        [Column("maHoSo")]
        public long MaHoSo { get; set; }

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

        [Column("ngayTao", TypeName = "date")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        // Liên kết với bảng NguoiDung (nếu cần truy xuất ngược)
        [ForeignKey("UserID")]
        public virtual User User { get; set; }
    }
}