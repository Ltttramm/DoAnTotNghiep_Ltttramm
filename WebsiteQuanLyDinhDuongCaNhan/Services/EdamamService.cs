////using System;
////using System.Net.Http;
////using System.Threading.Tasks;
////using Newtonsoft.Json.Linq;

////public class EdamamService
////{
////    private readonly string appId = "71a46160";  // Thay bằng App ID của bạn
////    private readonly string apiKey = "7ee5fcd0bd05b97eb3e09594eef38189";  // Thay bằng API Key của bạn
////    private readonly string userId = "TramLe";  // UserID tùy chọn
////    private readonly string baseUrl = "https://api.edamam.com/api/recipes/v2?type=public";

////    public async Task<string> GetMealPlanAsync(string keyword, string diet = "", string health = "")
////    {
////        string url = $"{baseUrl}&q={keyword}&app_id={appId}&app_key={apiKey}";

////        // Thêm chế độ ăn nếu có
////        if (!string.IsNullOrEmpty(diet))
////        {
////            url += $"&diet={diet}";
////        }

////        // Thêm hạn chế ăn uống nếu có
////        if (!string.IsNullOrEmpty(health))
////        {
////            url += $"&health={health}";
////        }

////        using (HttpClient client = new HttpClient())
////        {
////            client.DefaultRequestHeaders.Add("Edamam-Account-User", userId);

////            HttpResponseMessage response = await client.GetAsync(url);

////            if (response.IsSuccessStatusCode)
////            {
////                string jsonData = await response.Content.ReadAsStringAsync();
////                Console.WriteLine("Dữ liệu JSON trả về từ API: " + jsonData);
////                return jsonData;
////            }
////            else
////            {
////                Console.WriteLine($"Lỗi API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
////            }
////        }
////        return "{}"; // Trả về JSON rỗng nếu có lỗi
////    }
////}

////public async Task<string> GetMealPlanByCaloriesAsync(double calorieTarget, string diet = "", string health = "")
////{
////    // Xác định khoảng calo cần tìm (ví dụ: ±100 kcal so với calorieTarget)
////    string calorieRange = $"{(calorieTarget - 100):F0}-{(calorieTarget + 100):F0}";
////    string url = $"{baseUrl}&app_id={appId}&app_key={apiKey}&calories={calorieRange}";

////    if (!string.IsNullOrEmpty(diet))
////    {
////        url += $"&diet={diet}";
////    }
////    if (!string.IsNullOrEmpty(health))
////    {
////        url += $"&health={health}";
////    }

////    using (HttpClient client = new HttpClient())
////    {
////        client.DefaultRequestHeaders.Add("Edamam-Account-User", userId);
////        HttpResponseMessage response = await client.GetAsync(url);
////        if (response.IsSuccessStatusCode)
////        {
////            string jsonData = await response.Content.ReadAsStringAsync();
////            Console.WriteLine("Dữ liệu JSON trả về từ Edamam (theo calorie): " + jsonData);
////            return jsonData;
////        }
////        else
////        {
////            Console.WriteLine($"Lỗi API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
////        }
////    }
////    return "{}"; // Trả về JSON rỗng nếu có lỗi
////}

////}

////using System;
////using System.Net.Http;
////using System.Threading.Tasks;

////public class EdamamService
////{
////    private readonly string appId = "71a46160";
////    private readonly string apiKey = "7ee5fcd0bd05b97eb3e09594eef38189";
////    private readonly string baseUrl = "https://api.edamam.com/api/recipes/v2?type=public";

////    public async Task<string> GetMealPlanByCaloriesAsync(double calorieTarget, string diet = "", string health = "")
////    {
////        string calorieRange = $"{(calorieTarget - 100):F0}-{(calorieTarget + 100):F0}";
////        string url = $"{baseUrl}&app_id={appId}&app_key={apiKey}&calories={calorieRange}";

////        if (!string.IsNullOrEmpty(diet)) url += $"&diet={diet}";
////        if (!string.IsNullOrEmpty(health)) url += $"&health={health}";

////        using (HttpClient client = new HttpClient())
////        {
////            HttpResponseMessage response = await client.GetAsync(url);
////            if (response.IsSuccessStatusCode)
////            {
////                return await response.Content.ReadAsStringAsync();
////            }
////        }
////        return "{}";
////    }
////}



//using Microsoft.EntityFrameworkCore.Metadata;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System;
//using WebsiteQuanLyDinhDuongCaNhan.Models;

//public class EdamamService
//{
//    private readonly string appId = "71a46160";  // Thay bằng App ID của bạn
//    private readonly string apiKey = "7ee5fcd0bd05b97eb3e09594eef38189";  // Thay bằng API Key của bạn
//    private readonly string userId = "TramLe";  // UserID tùy chọn
//    private readonly string baseUrl = "https://api.edamam.com/api/recipes/v2?type=public";

//    public async Task<string> GetMealPlanAsync(string keyword, double? calories = null, string diet = "", string health = "")
//    {
//        string url = $"{baseUrl}&q={keyword}&app_id={appId}&app_key={apiKey}";

//        // Thêm giới hạn kcal nếu có
//        if (calories.HasValue)
//        {
//            url += $"&calories={calories.Value - 50}-{calories.Value + 50}";

//        }

//        // Thêm chế độ ăn nếu có
//        if (!string.IsNullOrEmpty(diet))
//        {
//            url += $"&diet={diet}";
//        }

//        // Thêm hạn chế ăn uống nếu có
//        if (!string.IsNullOrEmpty(health))
//        {
//            url += $"&health={health}";
//        }

//        Console.WriteLine("URL gọi API: " + url); // Debug xem URL có đúng không

//        using (HttpClient client = new HttpClient())
//        {
//            client.DefaultRequestHeaders.Add("Edamam-Account-User", userId);

//            HttpResponseMessage response = await client.GetAsync(url);

//            if (response.IsSuccessStatusCode)
//            {
//                string jsonData = await response.Content.ReadAsStringAsync();
//                Console.WriteLine("Dữ liệu JSON trả về từ API: " + jsonData);
//                return jsonData;
//            }
//            else
//            {
//                Console.WriteLine($"Lỗi API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
//            }
//        }
//        return "{}"; // Trả về JSON rỗng nếu có lỗi
//    }

//}

using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

public class EdamamService
{
    private readonly string appId;
    private readonly string apiKey;
    private readonly string userId;
    private readonly string baseUrl = "https://api.edamam.com/api/recipes/v2?type=public";
    private readonly int resultLimit;
    private readonly int calorieBuffer;

    public EdamamService()
    {
        // Load credentials from Web.config
        appId = ConfigurationManager.AppSettings["EdamamAppId"];
        apiKey = ConfigurationManager.AppSettings["EdamamApiKey"];
        userId = ConfigurationManager.AppSettings["EdamamUserId"];
        
        // Load configuration with defaults
        int.TryParse(ConfigurationManager.AppSettings["EdamamResultLimit"], out resultLimit);
        if (resultLimit == 0) resultLimit = 100;
        
        int.TryParse(ConfigurationManager.AppSettings["EdamamCalorieBuffer"], out calorieBuffer);
        if (calorieBuffer == 0) calorieBuffer = 200;
    }

    /// <summary>
    /// Fetch meals from Edamam API based on TDEE calorie range
    /// Searches for recipes where calories per serving matches a single meal portion (TDEE/3)
    /// </summary>
    public async Task<string> GetMealsByTDEEAsync(double tdee)
    {
        try
        {
            // Calculate appropriate calories for a single meal (divide TDEE by 3 meals per day)
            double mealCalories = tdee / 3;
            double minCalories = mealCalories - 50;  // ±100 range for closer matching
            double maxCalories = mealCalories + 50;

            // Use a wider range for initial API search to get more results
            string url = $"{baseUrl}&app_id={appId}&app_key={apiKey}";
            url += $"&calories={minCalories * 0.5:F0}-{maxCalories * 4:F0}";  // Cast wider net
            url += $"&to={resultLimit}";

            Console.WriteLine($"URL gọi API (TDEE-based): {url}");
            Console.WriteLine($"TDEE: {tdee} kcal/ngày → Mỗi bữa: {mealCalories:F0} kcal (khoảng {minCalories:F0}-{maxCalories:F0})");

            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    client.DefaultRequestHeaders.Add("Edamam-Account-User", userId);
                }

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Lỗi API: {response.StatusCode} - {errorContent}");
                    return "{\"error\": \"Không thể kết nối đến dịch vụ tìm kiếm món ăn\"}";
                }

                string jsonData = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(jsonData);

                // Filter results to only include recipes where calories per serving is appropriate
                if (data["hits"] != null)
                {
                    var hits = (JArray)data["hits"];
                    var filteredHits = new JArray();

                    foreach (var hit in hits)
                    {
                        var recipe = hit["recipe"];
                        double totalCalories = recipe["calories"]?.Value<double>() ?? 0;
                        double servings = recipe["yield"]?.Value<double>() ?? 1;
                        double caloriesPerServing = totalCalories / servings;

                        // Only include if calories per serving is within our target range
                        if (caloriesPerServing >= minCalories && caloriesPerServing <= maxCalories)
                        {
                            filteredHits.Add(hit);
                        }
                    }

                    data["hits"] = filteredHits;
                    Console.WriteLine($"API trả về {hits.Count} món ăn, sau khi lọc theo calories/serving: {filteredHits.Count} món");
                }

                return data.ToString();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi xảy ra trong GetMealsByTDEEAsync: " + ex.Message);
            return "{\"error\": \"Đã xảy ra lỗi khi tải danh sách món ăn\"}";
        }
    }

    /// <summary>
    /// Legacy method - kept for backward compatibility
    /// </summary>
    public async Task<string> GetMealPlanAsync(string keyword = "", double? calories = null, string diet = "", string health = "")
    {
        try
        {
            string url = $"{baseUrl}&app_id={appId}&app_key={apiKey}";

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                url += $"&q={keyword}";
            }

            if (calories.HasValue)
            {
                url += $"&calories={(calories.Value - calorieBuffer):F0}-{(calories.Value + calorieBuffer):F0}";
            }

            if (!string.IsNullOrEmpty(diet))
            {
                url += $"&diet={diet}";
            }

            if (!string.IsNullOrEmpty(health))
            {
                url += $"&health={health}";
            }

            url += $"&to={resultLimit}";

            Console.WriteLine("URL gọi API: " + url);

            using (HttpClient client = new HttpClient())
            {
                if (!string.IsNullOrEmpty(userId))
                {
                    client.DefaultRequestHeaders.Add("Edamam-Account-User", userId);
                }

                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Lỗi API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return "{}";
                }

                string jsonData = await response.Content.ReadAsStringAsync();
                return jsonData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi xảy ra: " + ex.Message);
            return "{}";
        }
    }

    
}
