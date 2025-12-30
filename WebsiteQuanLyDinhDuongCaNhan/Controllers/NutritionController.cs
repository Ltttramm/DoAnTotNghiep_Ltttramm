using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

public class NutritionController : Controller
{
    private readonly HttpClient _httpClient;

    public NutritionController()
    {
        _httpClient = new HttpClient();
    }

    public async Task<ActionResult> Analyze(string ingredient)
    {
        if (string.IsNullOrEmpty(ingredient))
        {
            ViewBag.Error = "Vui lòng nhập nguyên liệu.";
            return View();
        }

        string apiUrl = $"https://api.edamam.com/api/nutrition-data?app_id=26a45bf8&app_key=72311f618c619600f6d6e59358a19358&ingr={Uri.EscapeDataString(ingredient)}";

        try
        {
            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                JObject nutritionData = JObject.Parse(jsonString);

                ViewBag.JsonData = nutritionData.ToString(); // Lưu dữ liệu JSON vào ViewBag
            }
            else
            {
                ViewBag.Error = "Không thể kết nối đến API.";
            }
        }
        catch (Exception ex)
        {
            ViewBag.Error = $"Lỗi khi gọi API: {ex.Message}";
        }

        return View();
    }
}
