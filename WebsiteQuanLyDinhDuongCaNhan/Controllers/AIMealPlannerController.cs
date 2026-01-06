
////using System.Threading.Tasks;
////using System.Web.Mvc;
////using System;
////using WebsiteQuanLyDinhDuongCaNhan.Models;
////using System.Windows.Forms;
////using System.Linq;
////using Newtonsoft.Json;
////using System.Net.Http;
////using Newtonsoft.Json.Linq;
////using System.Collections.Generic;

////public class AIMealPlannerController : Controller
////{
////    private readonly SpoonacularService _spoonacularService;
////    private readonly UserService _userService;
////    public AIMealPlannerController()
////    {
////        _spoonacularService = new SpoonacularService();
////        _userService = new UserService();
////    }

////    [Route("AIMealPlanner/MealPlan")]
////    public async Task<ActionResult> MealPlan()
////    {
////        string entryLog = "[ENTRY] MealPlan action called";
////        System.Diagnostics.Debug.WriteLine(entryLog);
////        Console.WriteLine(entryLog);
////        System.Diagnostics.Trace.WriteLine(entryLog);

////        try
////        {
////            // Lấy thông tin người dùng từ database/session
////            string getUserLog = "[STEP 1] Getting current user...";
////            System.Diagnostics.Debug.WriteLine(getUserLog);
////            Console.WriteLine(getUserLog);
////            System.Diagnostics.Trace.WriteLine(getUserLog);

////            User user = GetCurrentUser();

////            if (user == null)
////            {
////                string noUserLog = "[ERROR] User is null - redirecting to login";
////                System.Diagnostics.Debug.WriteLine(noUserLog);
////                Console.WriteLine(noUserLog);
////                System.Diagnostics.Trace.WriteLine(noUserLog);
////                return RedirectToAction("Login", "Auth");
////            }

////            string userFoundLog = $"[STEP 2] User found: {user.FullName} (ID: {user.UserID})";
////            System.Diagnostics.Debug.WriteLine(userFoundLog);
////            Console.WriteLine(userFoundLog);
////            System.Diagnostics.Trace.WriteLine(userFoundLog);

////            // Tính TDEE
////            string calcLog = "[STEP 3] Calculating TDEE...";
////            System.Diagnostics.Debug.WriteLine(calcLog);
////            Console.WriteLine(calcLog);
////            System.Diagnostics.Trace.WriteLine(calcLog);

////            double tdee = _userService.CalculateTDEE(user);
////            ViewBag.TDEE = tdee;

////            // Debug logging (output to multiple channels for visibility)
////            string tdeeLog = $"[DEBUG] User TDEE: {tdee}";
////            System.Diagnostics.Debug.WriteLine(tdeeLog);
////            Console.WriteLine(tdeeLog);
////            System.Diagnostics.Trace.WriteLine(tdeeLog);

////            // Generate weekly meal plan (7 days, 3 meals per day) based on TDEE using Spoonacular
////            string apiCallLog = "[STEP 4] Calling Spoonacular WEEKLY API...";
////            System.Diagnostics.Debug.WriteLine(apiCallLog);
////            Console.WriteLine(apiCallLog);
////            System.Diagnostics.Trace.WriteLine(apiCallLog);

////            string weeklyMealPlanJson = await _spoonacularService.GenerateWeeklyMealPlanAsync(tdee);

////            // Debug logging (output to multiple channels for visibility)
////            string lengthLog = $"[DEBUG] Spoonacular Weekly Response Length: {weeklyMealPlanJson?.Length ?? 0}";
////            string responseLog = $"[DEBUG] Spoonacular Weekly Response: {weeklyMealPlanJson}";

////            System.Diagnostics.Debug.WriteLine(lengthLog);
////            System.Diagnostics.Debug.WriteLine(responseLog);

////            Console.WriteLine(lengthLog);
////            Console.WriteLine(responseLog);

////            System.Diagnostics.Trace.WriteLine(lengthLog);
////            System.Diagnostics.Trace.WriteLine(responseLog);

////            // Pass weekly meal plan to view
////            ViewBag.ApiRawResponse = weeklyMealPlanJson; // Always pass raw response for debugging

////            if (!string.IsNullOrWhiteSpace(weeklyMealPlanJson) && !weeklyMealPlanJson.Contains("\"error\""))
////            {
////                // Simply pass the raw weekly JSON - view will parse it
////                ViewBag.WeeklyMealPlan = weeklyMealPlanJson;
////                ViewBag.ErrorMessage = null;
////            }
////            else
////            {
////                ViewBag.WeeklyMealPlan = null;
////                ViewBag.ErrorMessage = "Unable to load weekly meal plan. See error details below.";
////            }

////            return View("MealPlan");
////        }
////        catch (Exception ex)
////        {
////            string errorLog = $"[ERROR] Exception in MealPlan controller: {ex.Message}";
////            string stackLog = $"[ERROR] Stack Trace: {ex.StackTrace}";
////            string innerLog = ex.InnerException != null ? $"[ERROR] Inner Exception: {ex.InnerException.Message}" : "[ERROR] No inner exception";

////            System.Diagnostics.Debug.WriteLine(errorLog);
////            System.Diagnostics.Debug.WriteLine(stackLog);
////            System.Diagnostics.Debug.WriteLine(innerLog);

////            Console.WriteLine(errorLog);
////            Console.WriteLine(stackLog);
////            Console.WriteLine(innerLog);

////            System.Diagnostics.Trace.WriteLine(errorLog);
////            System.Diagnostics.Trace.WriteLine(stackLog);
////            System.Diagnostics.Trace.WriteLine(innerLog);
////            ViewBag.MealPlan = null;
////            ViewBag.ErrorMessage = $"Đã xảy ra lỗi hệ thống: {ex.Message}";
////            ViewBag.ApiRawResponse = $"EXCEPTION STACK TRACE:\n{ex.ToString()}";
////            return View("MealPlan");
////        }
////    }

////    /// <summary>
////    /// Display detailed recipe page with instructions and nutrition
////    /// </summary>
////    public async Task<ActionResult> RecipeDetail(int recipeId)
////    {
////        try
////        {
////            string logEntry = $"[RECIPE DETAIL] Fetching recipe ID: {recipeId}";
////            System.Diagnostics.Debug.WriteLine(logEntry);
////            Console.WriteLine(logEntry);
////            System.Diagnostics.Trace.WriteLine(logEntry);

////            string recipeJson = await _spoonacularService.GetRecipeInformationAsync(recipeId);

////            if (!string.IsNullOrWhiteSpace(recipeJson) && !recipeJson.Contains("\"error\""))
////            {
////                ViewBag.RecipeData = recipeJson;
////                ViewBag.RecipeId = recipeId;
////                return View("RecipeDetail");
////            }
////            else
////            {
////                ViewBag.ErrorMessage = "Không thể tải thông tin công thức. Vui lòng thử lại.";
////                ViewBag.RecipeData = null;
////                return View("RecipeDetail");
////            }
////        }
////        catch (Exception ex)
////        {
////            string errorLog = $"[ERROR] Exception in RecipeDetail: {ex.Message}";
////            System.Diagnostics.Debug.WriteLine(errorLog);
////            Console.WriteLine(errorLog);
////            System.Diagnostics.Trace.WriteLine(errorLog);

////            ViewBag.ErrorMessage = $"Đã xảy ra lỗi: {ex.Message}";
////            ViewBag.RecipeData = null;
////            return View("RecipeDetail");
////        }
////    }

////    /// <summary>
////    /// AJAX endpoint to fetch detailed recipe information
////    /// </summary>
////    [HttpGet]
////    public async Task<JsonResult> GetRecipeDetails(int recipeId)
////    {
////        try
////        {
////            string recipeJson = await _spoonacularService.GetRecipeInformationAsync(recipeId);

////            if (!recipeJson.Contains("\"error\""))
////            {
////                var recipeData = JObject.Parse(recipeJson);
////                return Json(recipeData, JsonRequestBehavior.AllowGet);
////            }
////            else
////            {
////                return Json(new { error = "Không thể tải công thức" }, JsonRequestBehavior.AllowGet);
////            }
////        }
////        catch (Exception ex)
////        {
////            Console.WriteLine($"Error in GetRecipeDetails: {ex.Message}");
////            return Json(new { error = "Đã xảy ra lỗi" }, JsonRequestBehavior.AllowGet);
////        }
////    }

////    private User GetCurrentUser()
////    {
////        if (!User.Identity.IsAuthenticated)
////        {
////            return null;
////        }

////        string username = User.Identity.Name; // Lấy tên đăng nhập từ Identity

////        using (var db = new dbQuanLyDinhDuong())
////        {
////            // Lấy trực tiếp thông tin từ database
////            User user = db.Users.FirstOrDefault(u => u.FullName == username); // Hoặc u.Email nếu cần

////            return user;
////        }
////    }

////}


//using System.Threading.Tasks;
//using System.Web.Mvc;
//using System;
//using WebsiteQuanLyDinhDuongCaNhan.Models;
//using System.Linq;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

//public class AIMealPlannerController : Controller
//{
//    private readonly SpoonacularService _spoonacularService;
//    private readonly UserService _userService;

//    public AIMealPlannerController()
//    {
//        _spoonacularService = new SpoonacularService();
//        _userService = new UserService();
//    }

//    /// <summary>
//    /// Trang hiển thị thực đơn: Chỉ lấy dữ liệu CŨ NHẤT trong DB để hiển thị.
//    /// Không bao giờ tự ý gọi API Spoonacular tại đây.
//    /// </summary>
//    [Route("AIMealPlanner/MealPlan")]
//    public async Task<ActionResult> MealPlan()
//    {
//        try
//        {
//            User user = GetCurrentUser();
//            if (user == null) return RedirectToAction("Login", "Auth");

//            double tdee = _userService.CalculateTDEE(user);
//            ViewBag.TDEE = tdee;

//            using (var db = new dbQuanLyDinhDuong())
//            {
//                // Lấy thực đơn MỚI NHẤT của người dùng trong lịch sử
//                var latestPlan = db.WeeklyMealPlans
//                                   .Where(p => p.UserID == user.UserID)
//                                   .OrderByDescending(p => p.PlanID) // Lấy ID lớn nhất (mới nhất)
//                                   .FirstOrDefault();

//                if (latestPlan != null)
//                {
//                    ViewBag.WeeklyMealPlan = latestPlan.MealSchedule;
//                }
//                else
//                {
//                    // Nếu người dùng hoàn toàn chưa có thực đơn nào, mới gọi hàm tạo lần đầu
//                    return await GenerateNewPlan();
//                }
//            }

//            return View("MealPlan");
//        }
//        catch (Exception ex)
//        {
//            ViewBag.ErrorMessage = $"Lỗi hệ thống: {ex.Message}";
//            return View("MealPlan");
//        }
//    }

//    /// <summary>
//    /// Hàm này CHỈ CHẠY khi người dùng nhấn nút "Làm mới" hoặc chưa có dữ liệu.
//    /// Nó sẽ gọi API và lưu một bản ghi MỚI vào Database (Lưu lịch sử).
//    /// </summary>
//    public async Task<ActionResult> GenerateNewPlan()
//    {
//        try
//        {
//            User user = GetCurrentUser();
//            if (user == null) return RedirectToAction("Login", "Auth");

//            double tdee = _userService.CalculateTDEE(user);

//            // Gọi API Spoonacular để lấy thực đơn ngẫu nhiên mới
//            string weeklyMealPlanJson = await _spoonacularService.GenerateWeeklyMealPlanAsync(tdee);

//            if (!string.IsNullOrWhiteSpace(weeklyMealPlanJson) && !weeklyMealPlanJson.Contains("\"error\""))
//            {
//                using (var db = new dbQuanLyDinhDuong())
//                {
//                    // Lưu vào bảng WeeklyMealPlans (không xóa cái cũ, để làm lịch sử)
//                    var newRecord = new WeeklyMealPlan
//                    {
//                        UserID = user.UserID,
//                        WeekStartDate = DateTime.Now,
//                        MealSchedule = weeklyMealPlanJson
//                        // Nếu có cột CreatedAt hãy gán: CreatedAt = DateTime.Now
//                    };
//                    db.WeeklyMealPlans.Add(newRecord);
//                    await db.SaveChangesAsync();
//                }
//            }
//            else
//            {
//                TempData["ErrorMessage"] = "Không thể lấy thực đơn mới từ AI. Vui lòng thử lại sau.";
//            }

//            // Sau khi tạo và lưu xong, quay lại trang hiển thị
//            return RedirectToAction("MealPlan");
//        }
//        catch (Exception ex)
//        {
//            TempData["ErrorMessage"] = "Lỗi khi tạo thực đơn: " + ex.Message;
//            return RedirectToAction("MealPlan");
//        }
//    }

//    public async Task<ActionResult> RecipeDetail(int recipeId)
//    {
//        try
//        {
//            string recipeJson = await _spoonacularService.GetRecipeInformationAsync(recipeId);
//            if (!string.IsNullOrWhiteSpace(recipeJson) && !recipeJson.Contains("\"error\""))
//            {
//                ViewBag.RecipeData = recipeJson;
//                ViewBag.RecipeId = recipeId;
//                return View("RecipeDetail");
//            }
//            ViewBag.ErrorMessage = "Không thể tải thông tin công thức.";
//            return View("RecipeDetail");
//        }
//        catch (Exception ex)
//        {
//            ViewBag.ErrorMessage = $"Đã xảy ra lỗi: {ex.Message}";
//            return View("RecipeDetail");
//        }
//    }

//    [HttpGet]
//    public async Task<JsonResult> GetRecipeDetails(int recipeId)
//    {
//        try
//        {
//            string recipeJson = await _spoonacularService.GetRecipeInformationAsync(recipeId);
//            if (!recipeJson.Contains("\"error\""))
//            {
//                var recipeData = JObject.Parse(recipeJson);
//                return Json(recipeData, JsonRequestBehavior.AllowGet);
//            }
//            return Json(new { error = "Không thể tải công thức" }, JsonRequestBehavior.AllowGet);
//        }
//        catch (Exception ex)
//        {
//            return Json(new { error = "Đã xảy ra lỗi" }, JsonRequestBehavior.AllowGet);
//        }
//    }

//    private User GetCurrentUser()
//    {
//        if (!User.Identity.IsAuthenticated) return null;
//        string username = User.Identity.Name;
//        using (var db = new dbQuanLyDinhDuong())
//        {
//            return db.Users.FirstOrDefault(u => u.FullName == username);
//        }
//    }


//}



using System.Threading.Tasks;
using System.Web.Mvc;
using System;
using WebsiteQuanLyDinhDuongCaNhan.Models;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Data.Entity;
using WebsiteQuanLyDinhDuongCaNhan.Services;

namespace WebsiteQuanLyDinhDuongCaNhan.Controllers
{
    public class AIMealPlannerController : Controller
    {
        private readonly SpoonacularService _spoonacularService;
        private readonly UserService _userService;

        public AIMealPlannerController()
        {
            _spoonacularService = new SpoonacularService();
            _userService = new UserService();
        }

        /// <summary>
        /// Hiển thị thực đơn tuần - Lấy thực đơn mới nhất từ DB
        /// </summary>
        [Route("AIMealPlanner/MealPlan")]
        public async Task<ActionResult> MealPlan()
        {
            using (var db = new dbQuanLyDinhDuong())
            {
                try
                {
                    User user = GetCurrentUser();
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Auth");
                    }

                    // Tính TDEE cho người dùng
                    double tdee = _userService.CalculateTDEE(user);
                    ViewBag.TDEE = tdee;

                    // Lấy thực đơn mới nhất từ database
                    var latestPlan = db.WeeklyMealPlans
                                       .Where(p => p.UserID == user.UserID)
                                       .OrderByDescending(p => p.PlanID)
                                       .FirstOrDefault();

                    if (latestPlan != null)
                    {
                        ViewBag.WeeklyMealPlan = latestPlan.MealSchedule;
                        ViewBag.PlanCreatedDate = latestPlan.WeekStartDate;
                    }
                    else
                    {
                        // Nếu chưa có thực đơn, tự động tạo mới
                        return await GenerateNewPlanInternal();
                    }

                    return View("MealPlan");
                }
                catch (Exception ex)
                {
                    LogError("MealPlan", ex);
                    ViewBag.ErrorMessage = $"Lỗi hệ thống: {ex.Message}";
                    return View("MealPlan");
                }
            }
        }

        /// <summary>
        /// Endpoint công khai cho nút Refresh (hỗ trợ cả GET và POST)
        /// </summary>
        [Route("AIMealPlanner/GenerateNewPlan")]
        [HttpGet]
        [HttpPost]
        public async Task<ActionResult> GenerateNewPlan()
        {
            return await GenerateNewPlanInternal();
        }

        /// <summary>
        /// Logic tạo thực đơn tuần mới từ Spoonacular API và lưu vào DB
        /// </summary>
        private async Task<ActionResult> GenerateNewPlanInternal()
        {
            using (var db = new dbQuanLyDinhDuong())
            {
                try
                {
                    User user = GetCurrentUser();
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Auth");
                    }

                    double tdee = _userService.CalculateTDEE(user);
                    LogInfo($"Generating new meal plan for user {user.UserID} with TDEE: {tdee}");

                    // 1. Lấy JSON kế hoạch tuần từ API
                    string weeklyMealPlanJson = await _spoonacularService.GenerateWeeklyMealPlanAsync(tdee);

                    if (string.IsNullOrWhiteSpace(weeklyMealPlanJson) || weeklyMealPlanJson.Contains("\"error\""))
                    {
                        TempData["ErrorMessage"] = "Không thể lấy thực đơn từ API. Vui lòng kiểm tra API key hoặc quota.";
                        LogError("GenerateNewPlanInternal", new Exception($"API Error: {weeklyMealPlanJson}"));
                        return RedirectToAction("MealPlan");
                    }

                    JObject planData = JObject.Parse(weeklyMealPlanJson);

                    if (planData["week"] != null)
                    {
                        // 2. Gom tất cả ID món ăn
                        var allIds = new HashSet<string>();
                        foreach (var day in planData["week"].Children<JProperty>())
                        {
                            if (day.Value["meals"] != null)
                            {
                                foreach (var meal in day.Value["meals"])
                                {
                                    if (meal["id"] != null)
                                    {
                                        allIds.Add(meal["id"].ToString());
                                    }
                                }
                            }
                        }

                        // 3. Lấy thông tin chi tiết món ăn (bao gồm calories)
                        if (allIds.Any())
                        {
                            string idsString = string.Join(",", allIds);
                            LogInfo($"Fetching details for {allIds.Count} recipes: {idsString}");

                            string detailsJson = await _spoonacularService.GetRecipesInformationBulkAsync(idsString);

                            if (!string.IsNullOrEmpty(detailsJson))
                            {
                                var detailsArray = JArray.Parse(detailsJson);

                                // 4. Gán calories và thông tin bổ sung vào JSON
                                foreach (var day in planData["week"].Children<JProperty>())
                                {
                                    if (day.Value["meals"] != null)
                                    {
                                        foreach (var meal in day.Value["meals"])
                                        {
                                            string mealId = meal["id"]?.ToString();
                                            if (string.IsNullOrEmpty(mealId)) continue;

                                            var detail = detailsArray.FirstOrDefault(d => d["id"]?.ToString() == mealId);
                                            if (detail != null)
                                            {
                                                // Lấy calories
                                                var caloriesNutrient = detail["nutrition"]?["nutrients"]?
                                                    .FirstOrDefault(n => n["name"]?.ToString() == "Calories");

                                                if (caloriesNutrient != null)
                                                {
                                                    meal["calories"] = caloriesNutrient["amount"];
                                                }

                                                // Cập nhật image URL đầy đủ
                                                if (detail["image"] != null)
                                                {
                                                    meal["image"] = detail["image"];
                                                }

                                                // Thêm readyInMinutes nếu có
                                                if (detail["readyInMinutes"] != null)
                                                {
                                                    meal["readyInMinutes"] = detail["readyInMinutes"];
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // 5. Lưu vào database với error handling chi tiết
                    var newRecord = new WeeklyMealPlan
                    {
                        UserID = user.UserID,
                        WeekStartDate = DateTime.Now,
                        MealSchedule = planData.ToString(Formatting.None) // Compact JSON
                    };

                    LogInfo($"Attempting to save meal plan for UserID: {user.UserID}");

                    db.WeeklyMealPlans.Add(newRecord);

                    try
                    {
                        await db.SaveChangesAsync();
                        LogInfo($"Successfully saved meal plan with ID: {newRecord.PlanID}");
                    }
                    catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                    {
                        // Hiển thị chi tiết lỗi validation
                        string errorDetails = "";
                        foreach (var validationErrors in dbEx.EntityValidationErrors)
                        {
                            foreach (var validationError in validationErrors.ValidationErrors)
                            {
                                errorDetails += $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}\n";
                            }
                        }
                        LogError("SaveChanges Validation Error", new Exception(errorDetails));
                        TempData["ErrorMessage"] = $"Lỗi validation: {errorDetails}";
                        return RedirectToAction("MealPlan");
                    }
                    catch (Exception saveEx)
                    {
                        LogError("SaveChanges Error", saveEx);
                        string innerMsg = saveEx.InnerException?.Message ?? saveEx.Message;
                        TempData["ErrorMessage"] = $"Lỗi khi lưu database: {innerMsg}";
                        return RedirectToAction("MealPlan");
                    }

                    TempData["SuccessMessage"] = "Đã tạo thực đơn mới thành công!";
                    return RedirectToAction("MealPlan");
                }
                catch (Exception ex)
                {
                    LogError("GenerateNewPlanInternal", ex);
                    string innerMsg = ex.InnerException?.Message ?? ex.Message;
                    TempData["ErrorMessage"] = $"Lỗi khi tạo thực đơn: {innerMsg}";
                    return RedirectToAction("MealPlan");
                }
            }
        }

        /// <summary>
        /// Hiển thị chi tiết công thức món ăn
        /// </summary>
        public async Task<ActionResult> RecipeDetail(int recipeId)
        {
            using (var db = new dbQuanLyDinhDuong())
            {
                try
                {
                    string recipeJson = await _spoonacularService.GetRecipeInformationAsync(recipeId);

                    if (string.IsNullOrWhiteSpace(recipeJson) || recipeJson.Contains("\"error\""))
                    {
                        ViewBag.ErrorMessage = "Không thể tải thông tin công thức.";
                        return View("RecipeDetail");
                    }

                    ViewBag.RecipeData = recipeJson;
                    ViewBag.RecipeId = recipeId;

                    // Kiểm tra món ăn đã được yêu thích chưa
                    User user = GetCurrentUser();
                    if (user != null)
                    {
                        ViewBag.IsFavorite = db.UserFavorites.Any(f =>
                            f.UserID == user.UserID &&
                            f.ExternalRecipeID == recipeId
                        );
                    }

                    return View("RecipeDetail");
                }
                catch (Exception ex)
                {
                    LogError("RecipeDetail", ex);
                    ViewBag.ErrorMessage = $"Đã xảy ra lỗi: {ex.Message}";
                    return View("RecipeDetail");
                }
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết công thức qua AJAX
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

                return Json(new { error = "Không thể tải công thức" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                LogError("GetRecipeDetails", ex);
                return Json(new { error = "Đã xảy ra lỗi" }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Hiển thị danh sách món ăn yêu thích
        /// </summary>
        public ActionResult MyFavorites()
        {
            using (var db = new dbQuanLyDinhDuong())
            {
                User user = GetCurrentUser();
                if (user == null)
                {
                    return RedirectToAction("Login", "Auth");
                }

                var favorites = db.UserFavorites
                                  .Where(f => f.UserID == user.UserID)
                                  .OrderByDescending(f => f.CreatedAt)
                                  .ToList();

                return View(favorites);
            }
        }

        /// <summary>
        /// Thêm/Xóa món ăn khỏi danh sách yêu thích
        /// </summary>
        [HttpPost]
        public JsonResult ToggleFavorite(int recipeId, string title, string image, string category)
        {
            using (var db = new dbQuanLyDinhDuong())
            {
                try
                {
                    User user = GetCurrentUser();
                    if (user == null)
                    {
                        return Json(new { success = false, message = "Vui lòng đăng nhập" });
                    }

                    var existing = db.UserFavorites.FirstOrDefault(f =>
                        f.UserID == user.UserID &&
                        f.ExternalRecipeID == recipeId
                    );

                    if (existing != null)
                    {
                        // Xóa khỏi yêu thích
                        db.UserFavorites.Remove(existing);
                        db.SaveChanges();
                        return Json(new { success = true, action = "removed", message = "Đã xóa khỏi yêu thích" });
                    }
                    else
                    {
                        // Thêm vào yêu thích
                        var fav = new UserFavorite
                        {
                            UserID = user.UserID,
                            ExternalRecipeID = recipeId,
                            RecipeTitle = title ?? "Unknown Recipe",
                            RecipeImage = image,
                            Category = string.IsNullOrEmpty(category) ? "healthy" : category.ToLower(),
                            CreatedAt = DateTime.Now
                        };
                        db.UserFavorites.Add(fav);
                        db.SaveChanges();
                        return Json(new { success = true, action = "added", message = "Đã thêm vào yêu thích" });
                    }
                }
                catch (Exception ex)
                {
                    LogError("ToggleFavorite", ex);
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        /// <summary>
        /// Cập nhật ghi chú cho món ăn yêu thích
        /// </summary>
        [HttpPost]
        public JsonResult UpdateFavoriteNote(int id, string note)
        {
            using (var db = new dbQuanLyDinhDuong())
            {
                try
                {
                    var fav = db.UserFavorites.Find(id);
                    if (fav != null)
                    {
                        fav.Note = note;
                        db.SaveChanges();
                        return Json(new { success = true, message = "Đã cập nhật ghi chú" });
                    }
                    return Json(new { success = false, message = "Không tìm thấy bản ghi" });
                }
                catch (Exception ex)
                {
                    LogError("UpdateFavoriteNote", ex);
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        /// <summary>
        /// Xóa món ăn khỏi danh sách yêu thích
        /// </summary>
        [HttpPost]
        public JsonResult RemoveFavorite(int id)
        {
            using (var db = new dbQuanLyDinhDuong())
            {
                try
                {
                    var fav = db.UserFavorites.Find(id);
                    if (fav != null)
                    {
                        db.UserFavorites.Remove(fav);
                        db.SaveChanges();
                        return Json(new { success = true, message = "Đã xóa thành công" });
                    }
                    return Json(new { success = false, message = "Không tìm thấy món ăn" });
                }
                catch (Exception ex)
                {
                    LogError("RemoveFavorite", ex);
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }

        /// <summary>
        /// Lấy thông tin người dùng hiện tại
        /// </summary>
        private User GetCurrentUser()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return null;
            }

            string currentUsername = User.Identity.Name;

            using (var db = new dbQuanLyDinhDuong())
            {
                return db.Users.FirstOrDefault(u =>
                    u.Email == currentUsername ||
                    u.FullName == currentUsername
                );
            }
        }

        /// <summary>
        /// Ghi log lỗi chi tiết
        /// </summary>
        private void LogError(string method, Exception ex)
        {
            string errorLog = $"[ERROR] {method}: {ex.Message}";
            string stackLog = $"[STACK] {ex.StackTrace}";
            string innerLog = ex.InnerException != null ? $"[INNER] {ex.InnerException.Message}" : "";

            System.Diagnostics.Debug.WriteLine(errorLog);
            System.Diagnostics.Debug.WriteLine(stackLog);
            if (!string.IsNullOrEmpty(innerLog))
            {
                System.Diagnostics.Debug.WriteLine(innerLog);
            }

            Console.WriteLine(errorLog);
            Console.WriteLine(stackLog);
            if (!string.IsNullOrEmpty(innerLog))
            {
                Console.WriteLine(innerLog);
            }

            // Ghi vào file log (tùy chọn)
            try
            {
                string logPath = Server.MapPath("~/App_Data/error.log");
                string logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {errorLog}\n{stackLog}\n{innerLog}\n\n";
                System.IO.File.AppendAllText(logPath, logEntry);
            }
            catch
            {
                // Bỏ qua nếu không thể ghi file
            }
        }

        /// <summary>
        /// Ghi log thông tin
        /// </summary>
        private void LogInfo(string message)
        {
            string log = $"[INFO] {DateTime.Now:HH:mm:ss} - {message}";
            System.Diagnostics.Debug.WriteLine(log);
            Console.WriteLine(log);
        }
    }
}