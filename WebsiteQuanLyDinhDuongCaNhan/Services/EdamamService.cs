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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

public class EdamamService
{
    private readonly string appId = "71a46160";  // Thay bằng App ID của bạn
    private readonly string apiKey = "7ee5fcd0bd05b97eb3e09594eef38189";  // Thay bằng API Key của bạn
    private readonly string userId = "TramLe";  // UserID tùy chọn
    private readonly string baseUrl = "https://api.edamam.com/api/recipes/v2?type=public";

    public async Task<string> GetMealPlanAsync(string keyword = "", double? calories = null, string diet = "", string health = "")
    {
        try
        {
            string url = $"{baseUrl}&app_id={appId}&app_key={apiKey}";

            // Chỉ thêm keyword nếu người dùng nhập
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                url += $"&q={keyword}";
            }

            // Thêm giới hạn kcal nếu có
            if (calories.HasValue)
            {
                url += $"&calories={(calories.Value - 100):F0}-{(calories.Value + 100):F0}";
            }

            // Thêm chế độ ăn nếu có
            if (!string.IsNullOrEmpty(diet))
            {
                url += $"&diet={diet}";
            }

            // Thêm hạn chế ăn uống nếu có
            if (!string.IsNullOrEmpty(health))
            {
                url += $"&health={health}";
            }

            Console.WriteLine("URL gọi API: " + url); // Debug URL

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Edamam-Account-User", userId);
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Lỗi API: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    return "{}"; // Trả về JSON rỗng nếu API lỗi
                }

                string jsonData = await response.Content.ReadAsStringAsync();
                JObject data = JObject.Parse(jsonData);

                // Lọc lại danh sách món ăn có calories nằm trong khoảng mong muốn
                if (calories.HasValue && data["hits"] != null)
                {
                    var hitsList = ((JArray)data["hits"]).ToList();

                    hitsList = hitsList
                        .Where(hit => hit["recipe"]?["calories"] != null &&
                                      (double)hit["recipe"]["calories"] >= calories.Value - 50 &&
                                      (double)hit["recipe"]["calories"] <= calories.Value + 50)
                        .ToList();

                    data["hits"] = JArray.FromObject(hitsList); // Cập nhật danh sách đã lọc
                }

                string filteredJson = data.ToString();
                Console.WriteLine("Dữ liệu JSON sau khi lọc: " + filteredJson);
                return filteredJson;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Lỗi xảy ra: " + ex.Message);
            return "{}"; // Trả về JSON rỗng nếu có lỗi bất thường
        }
    }

    
}
