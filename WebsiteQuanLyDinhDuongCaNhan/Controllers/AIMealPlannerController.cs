
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
    public async Task<ActionResult> MealPlan()
    {
        try
        {
            // Lấy thông tin người dùng từ database/session
            User user = GetCurrentUser();

            if (user == null)
            {
                return RedirectToAction("Login", "Auth");
            }

            // Tính TDEE
            double tdee = _userService.CalculateTDEE(user);
            ViewBag.TDEE = tdee;

            // Automatically load meals based on TDEE on page load
            string mealPlanJson = await _edamamService.GetMealsByTDEEAsync(tdee);

            // Pass all meals to view for client-side filtering
            if (!string.IsNullOrWhiteSpace(mealPlanJson) && !mealPlanJson.Contains("\"error\""))
            {
                ViewBag.MealPlan = mealPlanJson;
                ViewBag.ErrorMessage = null;
            }
            else
            {
                ViewBag.MealPlan = null;
                ViewBag.ErrorMessage = "Không thể tải danh sách món ăn. Vui lòng thử lại sau.";
            }

            return View("MealPlan");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Lỗi trong MealPlan controller: {ex.Message}");
            ViewBag.MealPlan = null;
            ViewBag.ErrorMessage = "Đã xảy ra lỗi. Vui lòng thử lại.";
            return View("MealPlan");
        }
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


