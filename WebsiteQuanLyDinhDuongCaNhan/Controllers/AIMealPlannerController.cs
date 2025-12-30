
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
    private readonly SpoonacularService _spoonacularService;
    private readonly UserService _userService;
    public AIMealPlannerController()
    {
        _spoonacularService = new SpoonacularService();
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

            // Debug logging
            System.Diagnostics.Debug.WriteLine($"[DEBUG] User TDEE: {tdee}");

            // Generate daily meal plan (3 meals) based on TDEE using Spoonacular
            string mealPlanJson = await _spoonacularService.GenerateDailyMealPlanAsync(tdee);

            // Debug logging
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Spoonacular Response Length: {mealPlanJson?.Length ?? 0}");
            System.Diagnostics.Debug.WriteLine($"[DEBUG] Spoonacular Response: {mealPlanJson}");

            // Pass meal plan to view
            ViewBag.ApiRawResponse = mealPlanJson; // Always pass raw response for debugging

            if (!string.IsNullOrWhiteSpace(mealPlanJson) && !mealPlanJson.Contains("\"error\""))
            {
                ViewBag.MealPlan = mealPlanJson;
                ViewBag.ErrorMessage = null;
            }
            else
            {
                ViewBag.MealPlan = null;
                ViewBag.ErrorMessage = "Không thể tải danh sách món ăn. Xem chi tiết lỗi bên dưới.";
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

    /// <summary>
    /// AJAX endpoint to fetch detailed recipe information
    /// </summary>
    [HttpGet]
    public async Task<JsonResult> GetRecipeDetails(int recipeId)
    {
        try
        {
            string recipeJson = await _spoonacularService.GetRecipeInformationAsync(recipeId);
            
            if (!recipeJson.Contains("\"error\""))
            {
                var recipeData = JObject.Parse(recipeJson);
                return Json(recipeData, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { error = "Không thể tải công thức" }, JsonRequestBehavior.AllowGet);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetRecipeDetails: {ex.Message}");
            return Json(new { error = "Đã xảy ra lỗi" }, JsonRequestBehavior.AllowGet);
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


