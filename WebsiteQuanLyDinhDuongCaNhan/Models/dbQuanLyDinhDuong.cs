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
        public virtual DbSet<WeeklyMealPlan> WeeklyMealPlans { get; set; }
        public virtual DbSet<WeightPrediction> WeightPredictions { get; set; }
        public virtual DbSet<WeightTracking> WeightTrackings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdminUser>()
                .Property(e => e.Role)
                .IsUnicode(false);

            modelBuilder.Entity<ChatbotHistory>()
                .Property(e => e.UserMessage)
                .IsUnicode(false);

            modelBuilder.Entity<ChatbotHistory>()
                .Property(e => e.BotResponse)
                .IsUnicode(false);

            modelBuilder.Entity<Comment>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<FoodRecognition>()
                .Property(e => e.RecognizedFoods)
                .IsUnicode(false);

            modelBuilder.Entity<Food>()
                .HasMany(e => e.UserMeals)
                .WithOptional(e => e.Food)
                .WillCascadeOnDelete();

            modelBuilder.Entity<MealPlan>()
                .Property(e => e.MealType)
                .IsUnicode(false);

            modelBuilder.Entity<MealPlan>()
                .Property(e => e.SuggestedFoods)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Post)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Likes)
                .WithOptional(e => e.Post)
                .WillCascadeOnDelete();

            modelBuilder.Entity<UserMeal>()
                .Property(e => e.MealType)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ActivityLevel)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Goal)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PreferredDiet)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Allergy)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.CalorieTrackings)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.ChatbotHistories)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.FoodRecognitions)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Likes)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.MealPlans)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserMeals)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.WeeklyMealPlans)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.WeightPredictions)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<User>()
                .HasMany(e => e.WeightTrackings)
                .WithOptional(e => e.User)
                .WillCascadeOnDelete();

            modelBuilder.Entity<WeeklyMealPlan>()
                .Property(e => e.MealSchedule)
                .IsUnicode(false);
        }
    }
}
