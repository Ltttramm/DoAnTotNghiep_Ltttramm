using System;
using WebsiteQuanLyDinhDuongCaNhan.Models;

public class UserService
{
    public double CalculateTDEE(User user)
    {
        string startLog = $"[TDEE CALC] Starting calculation for user: {user?.FullName ?? "NULL"}";
        System.Diagnostics.Debug.WriteLine(startLog);
        Console.WriteLine(startLog);
        System.Diagnostics.Trace.WriteLine(startLog);
        
        double bmr;
        int age = DateTime.Now.Year - user.DateOfBirth.Value.Year;
        if (DateTime.Now.DayOfYear < user.DateOfBirth.Value.DayOfYear)
            age--;


        if (user.Gender.ToLower() == "male")
        {
            bmr = (double)(88.36 + (13.4 * user.Weight) + (4.8 * user.Height) - (5.7 * age));
        }
        else
        {
            bmr = (double)(447.6 + (9.2 * user.Weight) + (3.1 * user.Height) - (4.3 * age));
        }

        double activityFactor;

        switch (user.ActivityLevel?.ToLower())
        {
            case "Low":
                activityFactor = 1.2;
                break;
            case "Medium":
                activityFactor = 1.55;
                break;
            case "High":
                activityFactor = 1.9;
                break;
            default:
                activityFactor = 1.2; // Giá trị mặc định
                break;
        }

        double tdee = bmr * activityFactor;
        
        string resultLog = $"[TDEE CALC] Result - Age: {age}, Gender: {user.Gender}, Weight: {user.Weight}kg, Height: {user.Height}cm, Activity: {user.ActivityLevel}, BMR: {bmr:F2}, TDEE: {tdee:F2}";
        System.Diagnostics.Debug.WriteLine(resultLog);
        Console.WriteLine(resultLog);
        System.Diagnostics.Trace.WriteLine(resultLog);
        
        return tdee;

    }

}
