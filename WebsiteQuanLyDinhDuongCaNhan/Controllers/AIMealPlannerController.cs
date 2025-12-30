
using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using WebsiteQuanLyDinhDuongCaNhan.Models;
using System.Windows.Forms;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

public class AIMealPlannerController : Controller
{
    private readonly EdamamService _edamamService;
    private readonly UserService _userService;
    public AIMealPlannerController()
    {
        _edamamService = new EdamamService();
        _userService = new UserService();
    }

    [Route("AIMealPlanner/MealPlan")]
    public async Task<ActionResult> MealPlan(string keyword = "chicken", double? calories = null, string diet = "", string health = "")
    {
        // Lấy thông tin người dùng từ database/session
        User user = GetCurrentUser();

        if (user == null)
        {
            return RedirectToAction("Login", "Auth"); // Chuyển hướng nếu chưa đăng nhập
        }

        // Tính TDEE
        double tdee = _userService.CalculateTDEE(user);
        ViewBag.TDEE = tdee; // Lưu giá trị TDEE vào ViewBag để hiển thị trên giao diện

        // Gọi API lấy meal plan (giữ nguyên cách gọi từ code cũ)
        string mealPlanJson = await _edamamService.GetMealPlanAsync(keyword, calories, diet, health);

        if (!string.IsNullOrWhiteSpace(mealPlanJson) && mealPlanJson != "{}")
        {
            ViewBag.MealPlan = mealPlanJson;
        }
        else
        {
            ViewBag.MealPlan = null;
        }

        return View("MealPlan");
    }

    private User GetCurrentUser()
    {
        if (!User.Identity.IsAuthenticated)
        {
            return null;
        }

        string username = User.Identity.Name; // Lấy tên đăng nhập từ Identity

        using (var db = new dbQuanLyDinhDuong())
        {
            // Lấy trực tiếp thông tin từ database
            User user = db.Users.FirstOrDefault(u => u.FullName == username); // Hoặc u.Email nếu cần

            return user;
        }
    }

}


