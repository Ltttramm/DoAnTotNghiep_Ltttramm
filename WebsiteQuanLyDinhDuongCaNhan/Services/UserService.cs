using System;
using WebsiteQuanLyDinhDuongCaNhan.Models;

namespace WebsiteQuanLyDinhDuongCaNhan.Services
{
    public class UserService
    {
        /// <summary>
        /// Tính toán TDEE (Total Daily Energy Expenditure) - Tổng năng lượng tiêu thụ hàng ngày
        /// </summary>
        /// <param name="user">Đối tượng người dùng</param>
        /// <returns>TDEE tính bằng kcal/ngày</returns>
        public double CalculateTDEE(User user)
        {
            // Kiểm tra user null
            if (user == null)
            {
                LogMessage("[TDEE CALC] ERROR: User is null");
                return 0;
            }

            LogMessage($"[TDEE CALC] Starting calculation for user: {user.FullName ?? "Unknown"}");

            // Kiểm tra dữ liệu đầu vào
            if (!user.DateOfBirth.HasValue || !user.Weight.HasValue || !user.Height.HasValue)
            {
                LogMessage("[TDEE CALC] ERROR: Missing required data (DateOfBirth, Weight, or Height)");
                return 0;
            }

            // Tính tuổi
            int age = CalculateAge(user.DateOfBirth.Value);

            // Tính BMR (Basal Metabolic Rate) theo công thức Harris-Benedict
            double bmr = CalculateBMR(user.Gender, user.Weight.Value, user.Height.Value, age);

            // Lấy hệ số hoạt động
            double activityFactor = GetActivityFactor(user.ActivityLevel);

            // Tính TDEE
            double tdee = bmr * activityFactor;

            // Log kết quả
            string resultLog = $"[TDEE CALC] Result - " +
                              $"Age: {age}, " +
                              $"Gender: {user.Gender ?? "N/A"}, " +
                              $"Weight: {user.Weight:F1}kg, " +
                              $"Height: {user.Height:F1}cm, " +
                              $"Activity: {user.ActivityLevel ?? "N/A"}, " +
                              $"BMR: {bmr:F2} kcal, " +
                              $"TDEE: {tdee:F2} kcal";

            LogMessage(resultLog);

            return tdee;
        }

        /// <summary>
        /// Tính tuổi từ ngày sinh
        /// </summary>
        private int CalculateAge(DateTime dateOfBirth)
        {
            int age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
            {
                age--;
            }
            return age;
        }

        /// <summary>
        /// Tính BMR theo công thức Harris-Benedict
        /// Nam: BMR = 88.36 + (13.4 × cân nặng kg) + (4.8 × chiều cao cm) - (5.7 × tuổi)
        /// Nữ: BMR = 447.6 + (9.2 × cân nặng kg) + (3.1 × chiều cao cm) - (4.3 × tuổi)
        /// </summary>
        private double CalculateBMR(string gender, decimal weight, decimal height, int age)
        {
            double bmr;
            double w = (double)weight;
            double h = (double)height;

            // Chuyển gender về chữ thường để so sánh
            string genderLower = gender?.ToLower() ?? "female";

            if (genderLower == "male" || genderLower == "nam")
            {
                // Công thức cho nam
                bmr = 88.36 + (13.4 * w) + (4.8 * h) - (5.7 * age);
            }
            else
            {
                // Công thức cho nữ (mặc định)
                bmr = 447.6 + (9.2 * w) + (3.1 * h) - (4.3 * age);
            }

            return bmr;
        }

        /// <summary>
        /// Lấy hệ số hoạt động dựa trên mức độ vận động
        /// </summary>
        private double GetActivityFactor(string activityLevel)
        {
            if (string.IsNullOrEmpty(activityLevel))
            {
                return 1.2; // Mặc định: ít vận động
            }

            switch (activityLevel.ToLower())
            {
                case "low":
                case "thấp":
                case "ít vận động":
                    return 1.2; // Ít hoạt động (1-2 ngày/tuần)

                case "medium":
                case "trung bình":
                case "vừa phải":
                    return 1.55; // Hoạt động trung bình (3-5 ngày/tuần)

                case "high":
                case "cao":
                case "nhiều":
                    return 1.9; // Hoạt động nhiều (6-7 ngày/tuần)

                case "very high":
                case "rất cao":
                case "vận động viên":
                    return 2.2; // Rất cao (vận động viên chuyên nghiệp)

                default:
                    return 1.2; // Mặc định
            }
        }

        /// <summary>
        /// Ghi log ra nhiều nơi để debug
        /// </summary>
        private void LogMessage(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
            Console.WriteLine(message);
            System.Diagnostics.Trace.WriteLine(message);
        }

        /// <summary>
        /// Tính lượng calo khuyến nghị dựa trên mục tiêu
        /// </summary>
        /// <param name="tdee">TDEE hiện tại</param>
        /// <param name="goal">Mục tiêu (Lose Weight, Gain Weight, Maintain)</param>
        /// <returns>Lượng calo khuyến nghị</returns>
        public double CalculateRecommendedCalories(double tdee, string goal)
        {
            if (string.IsNullOrEmpty(goal))
            {
                return tdee;
            }

            switch (goal.ToLower())
            {
                case "lose weight":
                case "giảm cân":
                    return tdee - 500; // Giảm 500 kcal/ngày (giảm khoảng 0.5kg/tuần)

                case "gain weight":
                case "tăng cân":
                    return tdee + 500; // Tăng 500 kcal/ngày (tăng khoảng 0.5kg/tuần)

                case "maintain":
                case "duy trì":
                default:
                    return tdee; // Duy trì cân nặng
            }
        }
    }
}