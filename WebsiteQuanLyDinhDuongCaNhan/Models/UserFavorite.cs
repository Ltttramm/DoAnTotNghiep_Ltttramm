//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;
//using System.ComponentModel.DataAnnotations.Schema;

//namespace WebsiteQuanLyDinhDuongCaNhan.Models
//{
//    // Chỉ định tên bảng chính xác trong SQL Server
//    [Table("UserFavorites")]
//    public partial class UserFavorite
//    {
//        // Khóa chính tự tăng
//        [Key]
//        public int FavoriteID { get; set; }

//        // Khóa ngoại liên kết tới bảng Users
//        public int UserID { get; set; }

//        // ID món ăn từ API Spoonacular (để trống nếu là món nội bộ)
//        public long? ExternalRecipeID { get; set; }

//        // ID món ăn từ bảng Dishes nội bộ (để trống nếu là món API)
//        public long? InternalDishID { get; set; }

//        // Lưu tiêu đề món ăn để hiển thị nhanh trong danh sách
//        [StringLength(255)]
//        public string RecipeTitle { get; set; }

//        // Lưu đường dẫn ảnh món ăn để hiển thị nhanh
//        [StringLength(500)]
//        public string RecipeImage { get; set; }

//        // Ngày lưu vào danh sách
//        public DateTime CreatedAt { get; set; } = DateTime.Now;

//        // Thuộc tính điều hướng (Navigation Property) liên kết ngược lại User
//        [ForeignKey("UserID")]
//        public virtual User User { get; set; }

//        public string Note { get; set; } // Thêm dòng này vào class UserFavorite

//        public string Category { get; set; }
//    }
//}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    // Chỉ định tên bảng mới đã Việt hóa trong SQL Server
    [Table("MONAN_YEUTHICH")]
    public partial class UserFavorite
    {
        // Khóa chính tự tăng khớp với maMonYeuThich
        [Key]
        [Column("maMonYeuThich")]
        public int FavoriteID { get; set; }

        // Khóa ngoại liên kết tới bảng NGUOIDUNG
        [Column("maNguoiDung")]
        public int UserID { get; set; }

        // ID món ăn từ API Spoonacular
        [Column("maRecipeNgoai")]
        public long? ExternalRecipeID { get; set; }

        // ID món ăn từ bảng MONAN nội bộ
        [Column("maMon")]
        public int? FoodID { get; set; }

        // Lưu tiêu đề món ăn
        [Column("tieuDeRecipe")]
        [StringLength(255)]
        public string RecipeTitle { get; set; }

        // Lưu đường dẫn ảnh món ăn
        [Column("hinhAnhRecipe")]
        [StringLength(500)]
        public string RecipeImage { get; set; }

        // Ghi chú món ăn
        [Column("ghiChu")]
        public string Note { get; set; }

        // Danh mục món ăn
        [Column("danhMuc")]
        public string Category { get; set; }

        // Ngày lưu vào danh sách
        [Column("ngayTao")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // --- THUỘC TÍNH ĐIỀU HƯỚNG ---

        [ForeignKey("UserID")]
        public virtual User User { get; set; }

        [ForeignKey("FoodID")]
        public virtual Food Food { get; set; }
    }
}