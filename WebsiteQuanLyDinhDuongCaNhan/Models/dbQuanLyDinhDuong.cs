////using System;
////using System.ComponentModel.DataAnnotations.Schema;
////using System.Data.Entity;
////using System.Linq;

////namespace WebsiteQuanLyDinhDuongCaNhan.Models
////{
////    public partial class dbQuanLyDinhDuong : DbContext
////    {
////        public dbQuanLyDinhDuong()
////            : base("name=dbQuanLyDinhDuong")
////        {
////        }

////        public virtual DbSet<AdminUser> AdminUsers { get; set; }
////        public virtual DbSet<CalorieTracking> CalorieTrackings { get; set; }
////        public virtual DbSet<ChatbotHistory> ChatbotHistories { get; set; }
////        public virtual DbSet<Comment> Comments { get; set; }
////        public virtual DbSet<FoodRecognition> FoodRecognitions { get; set; }
////        public virtual DbSet<Food> Foods { get; set; }
////        public virtual DbSet<Like> Likes { get; set; }
////        public virtual DbSet<MealPlan> MealPlans { get; set; }
////        public virtual DbSet<Post> Posts { get; set; }
////        public virtual DbSet<UserMeal> UserMeals { get; set; }
////        public virtual DbSet<User> Users { get; set; }

////        // Thêm dòng này vào khoảng dòng 25-30
////        public virtual DbSet<UserFavorite> UserFavorites { get; set; }
////        public virtual DbSet<WeeklyMealPlan> WeeklyMealPlans { get; set; }
////        public virtual DbSet<WeightPrediction> WeightPredictions { get; set; }
////        public virtual DbSet<WeightTracking> WeightTrackings { get; set; }

////        protected override void OnModelCreating(DbModelBuilder modelBuilder)
////        {
////            modelBuilder.Entity<AdminUser>()
////                .Property(e => e.Role)
////                .IsUnicode(false);

////            modelBuilder.Entity<ChatbotHistory>()
////                .Property(e => e.UserMessage)
////                .IsUnicode(false);

////            modelBuilder.Entity<ChatbotHistory>()
////                .Property(e => e.BotResponse)
////                .IsUnicode(false);

////            modelBuilder.Entity<Comment>()
////                .Property(e => e.Content)
////                .IsUnicode(false);

////            modelBuilder.Entity<FoodRecognition>()
////                .Property(e => e.RecognizedFoods)
////                .IsUnicode(false);

////            modelBuilder.Entity<Food>()
////                .HasMany(e => e.UserMeals)
////                .WithOptional(e => e.Food)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<MealPlan>()
////                .Property(e => e.MealType)
////                .IsUnicode(false);

////            modelBuilder.Entity<MealPlan>()
////                .Property(e => e.SuggestedFoods)
////                .IsUnicode(false);

////            modelBuilder.Entity<Post>()
////                .Property(e => e.Content)
////                .IsUnicode(false);

////            modelBuilder.Entity<Post>()
////                .HasMany(e => e.Comments)
////                .WithOptional(e => e.Post)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<Post>()
////                .HasMany(e => e.Likes)
////                .WithOptional(e => e.Post)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<UserMeal>()
////                .Property(e => e.MealType)
////                .IsUnicode(false);

////            modelBuilder.Entity<User>()
////                .Property(e => e.ActivityLevel)
////                .IsUnicode(false);

////            modelBuilder.Entity<User>()
////                .Property(e => e.Goal)
////                .IsUnicode(false);

////            modelBuilder.Entity<User>()
////                .Property(e => e.PreferredDiet)
////                .IsUnicode(false);

////            modelBuilder.Entity<User>()
////                .Property(e => e.Allergy)
////                .IsUnicode(false);

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.CalorieTrackings)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.ChatbotHistories)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.Comments)
////                .WithRequired(e => e.User)
////                .WillCascadeOnDelete(false);

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.FoodRecognitions)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.Likes)
////                .WithRequired(e => e.User)
////                .WillCascadeOnDelete(false);

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.MealPlans)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.Posts)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.UserMeals)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.WeeklyMealPlans)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.WeightPredictions)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<User>()
////                .HasMany(e => e.WeightTrackings)
////                .WithOptional(e => e.User)
////                .WillCascadeOnDelete();

////            modelBuilder.Entity<WeeklyMealPlan>()
////                .Property(e => e.MealSchedule)
////                .IsUnicode(false);

////            // Cấu hình mối quan hệ cho bảng Yêu thích
////            modelBuilder.Entity<User>()
////                .HasMany(e => e.UserFavorites) // Lưu ý: Bạn cần thêm thuộc tính này vào class User.cs nữa
////                .WithRequired(e => e.User)
////                .HasForeignKey(e => e.UserID)
////                .WillCascadeOnDelete(true);
////        }
////    }
////}
//using System;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Data.Entity;
//using System.Linq;

//namespace WebsiteQuanLyDinhDuongCaNhan.Models
//{
//    public partial class dbQuanLyDinhDuong : DbContext
//    {
//        public dbQuanLyDinhDuong()
//            : base("name=dbQuanLyDinhDuong")
//        {
//        }

//        // Đảm bảo tên DbSet vẫn giữ nguyên để không phải sửa Controller
//        public virtual DbSet<AdminUser> AdminUsers { get; set; }
//        public virtual DbSet<CalorieTracking> CalorieTrackings { get; set; }
//        public virtual DbSet<ChatbotHistory> ChatbotHistories { get; set; }
//        public virtual DbSet<Comment> Comments { get; set; }
//        public virtual DbSet<FoodRecognition> FoodRecognitions { get; set; }
//        public virtual DbSet<Food> Foods { get; set; }
//        public virtual DbSet<Like> Likes { get; set; }
//        public virtual DbSet<MealPlan> MealPlans { get; set; }
//        public virtual DbSet<Post> Posts { get; set; }
//        public virtual DbSet<UserMeal> UserMeals { get; set; }
//        public virtual DbSet<User> Users { get; set; }
//        public virtual DbSet<UserFavorite> UserFavorites { get; set; }
//        public virtual DbSet<WeeklyMealPlan> WeeklyMealPlans { get; set; }
//        public virtual DbSet<WeightPrediction> WeightPredictions { get; set; }
//        public virtual DbSet<WeightTracking> WeightTrackings { get; set; }
//        public virtual DbSet<NutritionProfile> NutritionProfiles { get; set; }
//        public virtual DbSet<NutrientRequirement> NutrientRequirements { get; set; }
//        protected override void OnModelCreating(DbModelBuilder modelBuilder)
//        {
//            // 1. Cấu hình bảng MONAN (Food)
//            modelBuilder.Entity<Food>()
//                .HasMany(e => e.UserMeals)
//                .WithOptional(e => e.Food)
//                .HasForeignKey(e => e.FoodID) // Đảm bảo map đúng maMon
//                .WillCascadeOnDelete();

//            // 2. Cấu hình bảng NGUOIDUNG (User)
//            // Lưu ý: Các thuộc tính IsUnicode(false) chỉ dùng cho các cột VARCHAR (không có N)
//            modelBuilder.Entity<User>()
//                .Property(e => e.ActivityLevel)
//                .IsUnicode(true); // Đổi thành true nếu dùng NVARCHAR

//            modelBuilder.Entity<User>()
//                .HasMany(e => e.UserMeals)
//                .WithOptional(e => e.User)
//                .WillCascadeOnDelete();

//            // 3. Cấu hình bảng NHATKYBUAAN (UserMeal)
//            modelBuilder.Entity<UserMeal>()
//                .Property(e => e.MealType)
//                .IsUnicode(true);

//            // 4. Cấu hình mối quan hệ cho bảng MONAN_YEUTHICH (UserFavorite)
//            modelBuilder.Entity<UserFavorite>()
//                .HasRequired(e => e.User)
//                .WithMany(u => u.UserFavorites)
//                .HasForeignKey(e => e.UserID)
//                .WillCascadeOnDelete(true);

//            // 5. Nếu bạn có dùng bảng MONAN cho UserFavorite
//            modelBuilder.Entity<UserFavorite>()
//                .HasOptional(e => e.Food)
//                .WithMany()
//                .HasForeignKey(e => e.FoodID);

//            // Các cấu hình cũ giữ lại nếu bảng đó chưa đổi tên
//            modelBuilder.Entity<AdminUser>()
//                .Property(e => e.Role)
//                .IsUnicode(false);

//            modelBuilder.Entity<Post>()
//                .HasMany(e => e.Comments)
//                .WithOptional(e => e.Post)
//                .WillCascadeOnDelete();
//        }
//    }
//}

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    public partial class dbQuanLyDinhDuong : DbContext
    {
        public dbQuanLyDinhDuong()
            : base("name=dbQuanLyDinhDuong")
        {
            Database.SetInitializer<dbQuanLyDinhDuong>(null);
        }

        public virtual DbSet<AdminUser> AdminUsers { get; set; }
        public virtual DbSet<CalorieTracking> CalorieTrackings { get; set; }
        public virtual DbSet<ChatbotHistory> ChatbotHistories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FoodRecognition> FoodRecognitions { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<MealPlan> MealPlans { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<UserMeal> UserMeals { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserFavorite> UserFavorites { get; set; }
        public virtual DbSet<WeeklyMealPlan> WeeklyMealPlans { get; set; }
        public virtual DbSet<WeightPrediction> WeightPredictions { get; set; }
        public virtual DbSet<WeightTracking> WeightTrackings { get; set; }
        public virtual DbSet<NutritionProfile> NutritionProfiles { get; set; }
        public virtual DbSet<NutrientRequirement> NutrientRequirements { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Cấu hình bảng Food
            modelBuilder.Entity<Food>()
                .HasMany(e => e.UserMeals)
                .WithOptional(e => e.Food)
                .HasForeignKey(e => e.FoodID)
                .WillCascadeOnDelete();

            // Cấu hình bảng UserMeal
            modelBuilder.Entity<UserMeal>()
                .Property(e => e.MealType)
                .IsUnicode(true);

            // Cấu hình quan hệ User -> UserMeal
            modelBuilder.Entity<User>()
                .HasMany(e => e.UserMeals)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete(false);

            // Cấu hình bảng UserFavorite
            modelBuilder.Entity<UserFavorite>()
                .HasRequired(e => e.User)
                .WithMany(u => u.UserFavorites)
                .HasForeignKey(e => e.UserID)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<UserFavorite>()
                .HasOptional(e => e.Food)
                .WithMany()
                .HasForeignKey(e => e.FoodID);

            // Cấu hình AdminUser
            modelBuilder.Entity<AdminUser>()
                .Property(e => e.Role)
                .IsUnicode(false);

            // Cấu hình Post
            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Post)
                .WillCascadeOnDelete();

            // Cấu hình User
            modelBuilder.Entity<User>()
                .Property(e => e.FullName)
                .IsUnicode(true);
        }
    }
}