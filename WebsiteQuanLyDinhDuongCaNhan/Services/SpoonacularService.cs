using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Linq;

public class SpoonacularService
{
    private readonly string apiKey;
    private readonly string baseUrl = "https://api.spoonacular.com";

    public SpoonacularService()
    {
        // Load API key from Web.config
        apiKey = ConfigurationManager.AppSettings["SpoonacularApiKey"];
        System.Diagnostics.Debug.WriteLine($"[DEBUG] Spoonacular API Key (Service Init): {apiKey}");
    }

    /// <summary>
    /// Generate a daily meal plan (3 meals) based on target calories (TDEE)
    /// </summary>
    public async Task<string> GenerateDailyMealPlanAsync(double tdee, string diet = "", string exclude = "")
    {
        try
        {
            string url = $"{baseUrl}/mealplanner/generate";
            url += $"?apiKey={apiKey}";
            url += $"&timeFrame=day";
            url += $"&targetCalories={tdee:F0}";

            if (!string.IsNullOrEmpty(diet))
            {
                url += $"&diet={diet}";
            }

            if (!string.IsNullOrEmpty(exclude))
            {
                url += $"&exclude={exclude}";
            }

            // Detailed request logging
            Console.WriteLine("========== SPOONACULAR API REQUEST ==========");
            Console.WriteLine($"[REQUEST] Endpoint: /mealplanner/generate");
            Console.WriteLine($"[REQUEST] Method: GET");
            Console.WriteLine($"[REQUEST] Target Calories: {tdee:F0}");
            Console.WriteLine($"[REQUEST] TimeFrame: day");
            Console.WriteLine($"[REQUEST] Diet: {(string.IsNullOrEmpty(diet) ? "none" : diet)}");
            Console.WriteLine($"[REQUEST] Exclude: {(string.IsNullOrEmpty(exclude) ? "none" : exclude)}");
            Console.WriteLine($"[REQUEST] Full URL: {url.Replace(apiKey, "***HIDDEN***")}");
            Console.WriteLine("=============================================");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                // Detailed response logging
                Console.WriteLine("========== SPOONACULAR API RESPONSE ==========");
                Console.WriteLine($"[RESPONSE] Status Code: {(int)response.StatusCode} {response.StatusCode}");
                Console.WriteLine($"[RESPONSE] Success: {response.IsSuccessStatusCode}");
                Console.WriteLine($"[RESPONSE] Headers: {response.Headers}");

                string jsonData = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"[RESPONSE] Content Length: {jsonData?.Length ?? 0} characters");
                Console.WriteLine($"[RESPONSE] Content: {jsonData}");
                Console.WriteLine("==============================================");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"[ERROR] API returned error status. Message: {jsonData}");
                    return "{\"error\": \"Không thể tạo thực đơn\"}";
                }

                return jsonData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("========== EXCEPTION ==========");
            Console.WriteLine($"[EXCEPTION] Message: {ex.Message}");
            Console.WriteLine($"[EXCEPTION] Stack Trace: {ex.StackTrace}");
            Console.WriteLine("================================");
            return "{\"error\": \"Đã xảy ra lỗi khi tạo thực đơn\"}";
        }
    }

    /// <summary>
    /// Get detailed recipe information including cooking instructions and nutrition
    /// </summary>
    public async Task<string> GetRecipeInformationAsync(int recipeId)
    {
        try
        {
            string url = $"{baseUrl}/recipes/{recipeId}/information";
            url += $"?apiKey={apiKey}";
            url += $"&includeNutrition=true";

            Console.WriteLine($"Spoonacular URL (Recipe Info): {url}");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Spoonacular API Error: {response.StatusCode} - {errorContent}");
                    return "{\"error\": \"Không thể lấy thông tin công thức\"}";
                }

                string jsonData = await response.Content.ReadAsStringAsync();
                return jsonData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in GetRecipeInformationAsync: " + ex.Message);
            return "{\"error\": \"Đã xảy ra lỗi khi lấy thông tin công thức\"}";
        }
    }

    /// <summary>
    /// Search for ingredient nutrition information
    /// </summary>
    public async Task<string> SearchIngredientNutritionAsync(string ingredient, double amount = 100, string unit = "grams")
    {
        try
        {
            string url = $"{baseUrl}/recipes/parseIngredients";
            url += $"?apiKey={apiKey}";
            url += $"&ingredientList={Uri.EscapeDataString(ingredient)}";
            url += $"&servings=1";
            url += $"&includeNutrition=true";

            Console.WriteLine($"Spoonacular URL (Ingredient Nutrition): {url}");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, null);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Spoonacular API Error: {response.StatusCode} - {errorContent}");
                    return "{\"error\": \"Không thể lấy thông tin dinh dưỡng\"}";
                }

                string jsonData = await response.Content.ReadAsStringAsync();
                return jsonData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in SearchIngredientNutritionAsync: " + ex.Message);
            return "{\"error\": \"Đã xảy ra lỗi\"}";
        }
    }

    /// <summary>
    /// Search for food/ingredients in database
    /// </summary>
    public async Task<string> SearchFoodAsync(string query, int number = 10)
    {
        try
        {
            string url = $"{baseUrl}/food/ingredients/search";
            url += $"?apiKey={apiKey}";
            url += $"&query={Uri.EscapeDataString(query)}";
            url += $"&number={number}";
            url += $"&metaInformation=true";

            Console.WriteLine($"Spoonacular URL (Food Search): {url}");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Spoonacular API Error: {response.StatusCode} - {errorContent}");
                    return "{\"error\": \"Không thể tìm kiếm thực phẩm\"}";
                }

                string jsonData = await response.Content.ReadAsStringAsync();
                return jsonData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in SearchFoodAsync: " + ex.Message);
            return "{\"error\": \"Đã xảy ra lỗi\"}";
        }
    }

    /// <summary>
    /// Get ingredient information by ID
    /// </summary>
    public async Task<string> GetIngredientInformationAsync(int ingredientId, double amount = 100, string unit = "grams")
    {
        try
        {
            string url = $"{baseUrl}/food/ingredients/{ingredientId}/information";
            url += $"?apiKey={apiKey}";
            url += $"&amount={amount}";
            url += $"&unit={unit}";

            Console.WriteLine($"Spoonacular URL (Ingredient Info): {url}");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Spoonacular API Error: {response.StatusCode} - {errorContent}");
                    return "{\"error\": \"Không thể lấy thông tin nguyên liệu\"}";
                }

                string jsonData = await response.Content.ReadAsStringAsync();
                return jsonData;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in GetIngredientInformationAsync: " + ex.Message);
            return "{\"error\": \"Đã xảy ra lỗi\"}";
        }
    }
}
