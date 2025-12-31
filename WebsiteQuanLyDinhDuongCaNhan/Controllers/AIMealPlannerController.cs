
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
        string entryLog = "[ENTRY] MealPlan action called";
        System.Diagnostics.Debug.WriteLine(entryLog);
        Console.WriteLine(entryLog);
        System.Diagnostics.Trace.WriteLine(entryLog);
        
        try
        {
            // Lấy thông tin người dùng từ database/session
            string getUserLog = "[STEP 1] Getting current user...";
            System.Diagnostics.Debug.WriteLine(getUserLog);
            Console.WriteLine(getUserLog);
            System.Diagnostics.Trace.WriteLine(getUserLog);
            
            User user = GetCurrentUser();

            if (user == null)
            {
                string noUserLog = "[ERROR] User is null - redirecting to login";
                System.Diagnostics.Debug.WriteLine(noUserLog);
                Console.WriteLine(noUserLog);
                System.Diagnostics.Trace.WriteLine(noUserLog);
                return RedirectToAction("Login", "Auth");
            }
            
            string userFoundLog = $"[STEP 2] User found: {user.FullName} (ID: {user.UserId})";
            System.Diagnostics.Debug.WriteLine(userFoundLog);
            Console.WriteLine(userFoundLog);
            System.Diagnostics.Trace.WriteLine(userFoundLog);

            // Tính TDEE
            string calcLog = "[STEP 3] Calculating TDEE...";
            System.Diagnostics.Debug.WriteLine(calcLog);
            Console.WriteLine(calcLog);
            System.Diagnostics.Trace.WriteLine(calcLog);
            
            double tdee = _userService.CalculateTDEE(user);
            ViewBag.TDEE = tdee;

            // Debug logging (output to multiple channels for visibility)
            string tdeeLog = $"[DEBUG] User TDEE: {tdee}";
            System.Diagnostics.Debug.WriteLine(tdeeLog);
            Console.WriteLine(tdeeLog);
            System.Diagnostics.Trace.WriteLine(tdeeLog);

            // Generate daily meal plan (3 meals) based on TDEE using Spoonacular
            string apiCallLog = "[STEP 4] Calling Spoonacular API...";
            System.Diagnostics.Debug.WriteLine(apiCallLog);
            Console.WriteLine(apiCallLog);
            System.Diagnostics.Trace.WriteLine(apiCallLog);
            
            string mealPlanJson = await _spoonacularService.GenerateDailyMealPlanAsync(tdee);

            // Debug logging (output to multiple channels for visibility)
            string lengthLog = $"[DEBUG] Spoonacular Response Length: {mealPlanJson?.Length ?? 0}";
            string responseLog = $"[DEBUG] Spoonacular Response: {mealPlanJson}";
            
            System.Diagnostics.Debug.WriteLine(lengthLog);
            System.Diagnostics.Debug.WriteLine(responseLog);
            
            Console.WriteLine(lengthLog);
            Console.WriteLine(responseLog);
            
            System.Diagnostics.Trace.WriteLine(lengthLog);
            System.Diagnostics.Trace.WriteLine(responseLog);

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
            string errorLog = $"[ERROR] Exception in MealPlan controller: {ex.Message}";
            string stackLog = $"[ERROR] Stack Trace: {ex.StackTrace}";
            string innerLog = ex.InnerException != null ? $"[ERROR] Inner Exception: {ex.InnerException.Message}" : "[ERROR] No inner exception";
            
            System.Diagnostics.Debug.WriteLine(errorLog);
            System.Diagnostics.Debug.WriteLine(stackLog);
            System.Diagnostics.Debug.WriteLine(innerLog);
            
            Console.WriteLine(errorLog);
            Console.WriteLine(stackLog);
            Console.WriteLine(innerLog);
            
            System.Diagnostics.Trace.WriteLine(errorLog);
            System.Diagnostics.Trace.WriteLine(stackLog);
            System.Diagnostics.Trace.WriteLine(innerLog);
            ViewBag.MealPlan = null;
            ViewBag.ErrorMessage = $"Đã xảy ra lỗi hệ thống: {ex.Message}";
            ViewBag.ApiRawResponse = $"EXCEPTION STACK TRACE:\n{ex.ToString()}";
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


