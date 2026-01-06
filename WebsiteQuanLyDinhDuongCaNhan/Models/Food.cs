//namespace WebsiteQuanLyDinhDuongCaNhan.Models
//{
//    using System;
//    using System.Collections.Generic;
//    using System.ComponentModel.DataAnnotations;
//    using System.ComponentModel.DataAnnotations.Schema;

//    [Table("MONAN")] // Ánh xạ với bảng MONAN trong SQL (Thay cho Foods)
//    public partial class Food
//    {
//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
//        public Food()
//        {
//            UserMeals = new HashSet<UserMeal>();
//        }

//        [Key]
//        [Column("maMon")] // Ánh xạ với cột maMon trong ERD
//        public int FoodID { get; set; }

//        [Column("tenMon")] // Ánh xạ với cột tenMon
//        [StringLength(255)]
//        public string FoodName { get; set; }

//        [Column("tongCalo")] // Ánh xạ với cột tongCalo
//        public double? Calories { get; set; }

//        // Các thuộc tính dinh dưỡng chi tiết
//        public double? Protein { get; set; }

//        public double? Carbs { get; set; }

//        public double? Fat { get; set; }

//        public double? Fiber { get; set; }

//        public double? Sugar { get; set; }

//        // Ánh xạ với cột moTa (Nếu bạn muốn dùng Category làm mô tả hoặc thêm cột moTa)
//        [Column("moTa")]
//        public string Category { get; set; }

//        [StringLength(255)]
//        public string ImageURL { get; set; }

//        // --- CÁC THUỘC TÍNH QUẢN LÝ ---

//        [StringLength(100)]
//        public string ExternalApiID { get; set; }

//        public bool? IsVisible { get; set; }

//        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
//        public virtual ICollection<UserMeal> UserMeals { get; set; }
//    }
//}

namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MONAN")] // Ánh xạ với bảng MONAN trong SQL mới
    public partial class Food
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Food()
        {
            // Nếu bạn đổi tên bảng UserMeals thành NHATKYBUAAN trong SQL
            // thì tên ICollection này có thể giữ nguyên nhưng phải sửa Class UserMeal (xem lưu ý dưới)
            UserMeals = new HashSet<UserMeal>();
        }

        [Key]
        [Column("maMon")] // Ánh xạ với khóa chính maMon trong SQL
        public int FoodID { get; set; }

        [Column("tenMon")] // Ánh xạ với cột tenMon
        [StringLength(255)]
        public string FoodName { get; set; }

        [Column("tongCalo")] // Ánh xạ với cột tongCalo (kiểu Decimal trong SQL map với double? hoặc decimal? trong C#)
        public double? Calories { get; set; }

        // Các thuộc tính dinh dưỡng chi tiết (Nếu trong SQL tên cột vẫn để tiếng Anh thì giữ nguyên)
        public double? Protein { get; set; }

        public double? Carbs { get; set; }

        public double? Fat { get; set; }

        public double? Fiber { get; set; }

        public double? Sugar { get; set; }

        [Column("moTa")] // Ánh xạ cột Category trong code cũ với cột moTa trong SQL mới
        public string Category { get; set; }

        [Column("ImageURL")] // Tên cột trong SQL là ImageURL
        [StringLength(255)]
        public string ImageURL { get; set; }

        // --- CÁC THUỘC TÍNH QUẢN LÝ TỪ CODE CŨ ---

        [Column("ExternalApiID")]
        [StringLength(100)]
        public string ExternalApiID { get; set; }

        [Column("IsVisible")]
        public bool? IsVisible { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMeal> UserMeals { get; set; }
    }
}