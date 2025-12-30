using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

public class NutritionController : Controller
{
    private readonly SpoonacularService _spoonacularService;

    public NutritionController()
    {
        _spoonacularService = new SpoonacularService();
    }

    public async Task<ActionResult> Analyze(string ingredient)
    {
        if (string.IsNullOrEmpty(ingredient))
        {
            ViewBag.Error = "Vui lòng nhập nguyên liệu.";
            return View();
        }

        try
        {
            string jsonString = await _spoonacularService.SearchIngredientNutritionAsync(ingredient);
            
            if (!jsonString.Contains("\"error\""))
            {
                ViewBag.JsonData = jsonString; // Lưu dữ liệu JSON vào ViewBag
            }
            else
            {
                ViewBag.Error = "Không thể lấy thông tin dinh dưỡng.";
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Lỗi khi gọi API: {ex.Message}";
        }

        return View();
    }
}
