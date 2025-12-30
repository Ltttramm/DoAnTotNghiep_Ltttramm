namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Security.Cryptography;
    using System.Text;

    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            CalorieTrackings = new HashSet<CalorieTracking>();
            ChatbotHistories = new HashSet<ChatbotHistory>();
            Comments = new HashSet<Comment>();
            FoodRecognitions = new HashSet<FoodRecognition>();
            Likes = new HashSet<Like>();
            MealPlans = new HashSet<MealPlan>();
            Posts = new HashSet<Post>();
            UserMeals = new HashSet<UserMeal>();
            WeeklyMealPlans = new HashSet<WeeklyMealPlan>();
            WeightPredictions = new HashSet<WeightPrediction>();
            WeightTrackings = new HashSet<WeightTracking>();
        }

        public int UserID { get; set; }

        [StringLength(255)]
        public string FullName { get; set; }

        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [StringLength(255)]
        public string PasswordHash { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public int? Age
        {
            get
            {
                return DateOfBirth.HasValue ? (int?)(DateTime.Now.Year - DateOfBirth.Value.Year) : null;
            }
        }
        [StringLength(50)]
        public string Gender { get; set; }
        public double? Height { get; set; }

        public double? Weight { get; set; }

        [StringLength(50)]
        public string ActivityLevel { get; set; }

        [StringLength(50)]
        public string Goal { get; set; }

        [StringLength(50)]
        public string PreferredDiet { get; set; }

        [Column(TypeName = "text")]
        public string Allergy { get; set; }

        // Thêm các thuộc tính mới
        public bool IsEmailConfirmed { get; set; } = false; // Mặc định chưa xác nhận email
        public string EmailVerificationToken { get; set; } // Lưu token xác thực email
        public DateTime? CreatedAt { get; set; }
        public string PasswordResetToken { get; set; } // Token reset mật khẩu
        public DateTime? ResetTokenExpiry { get; set; } // Hạn sử dụng token

        

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CalorieTracking> CalorieTrackings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChatbotHistory> ChatbotHistories { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FoodRecognition> FoodRecognitions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Like> Likes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MealPlan> MealPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Post> Posts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserMeal> UserMeals { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeeklyMealPlan> WeeklyMealPlans { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeightPrediction> WeightPredictions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<WeightTracking> WeightTrackings { get; set; }

        public bool AuthenticateUser(string email, string password)
        {
            if (Email != email)
                return false;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);
                string hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

                return hashString == PasswordHash;
            }
        }
    }
}
