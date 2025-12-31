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
            // Use InvariantCulture to prevent issues with comma decimal separators in some locales
            url += $"&targetCalories={tdee.ToString("F0", System.Globalization.CultureInfo.InvariantCulture)}";

            if (!string.IsNullOrEmpty(diet))
            {
                url += $"&diet={diet}";
            }

            if (!string.IsNullOrEmpty(exclude))
            {
                url += $"&exclude={exclude}";
            }

            // Detailed request logging (output to both Debug and Console for visibility)
            string logSeparator = "========== SPOONACULAR API REQUEST ==========";
            string logUrl = $"[REQUEST] TDEE Value: {tdee}";
            string logFullUrl = $"[REQUEST] Full URL: {url}";
            
            System.Diagnostics.Debug.WriteLine(logSeparator);
            System.Diagnostics.Debug.WriteLine(logUrl);
            System.Diagnostics.Debug.WriteLine(logFullUrl);
            
            Console.WriteLine(logSeparator);
            Console.WriteLine(logUrl);
            Console.WriteLine(logFullUrl);
            
            System.Diagnostics.Trace.WriteLine(logSeparator);
            System.Diagnostics.Trace.WriteLine(logUrl);
            System.Diagnostics.Trace.WriteLine(logFullUrl);

            using (HttpClient client = new HttpClient())
            {
                // Ensure TLS 1.2 is supported (critical for some environments)
                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

                // Add User-Agent header as some APIs block requests without it
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64)");

                HttpResponseMessage response = await client.GetAsync(url);
                string jsonData = await response.Content.ReadAsStringAsync();

                string logStatus = $"[RESPONSE] Status: {response.StatusCode}";
                string logContent = $"[RESPONSE] Content: {jsonData}";
                
                System.Diagnostics.Debug.WriteLine(logStatus);
                System.Diagnostics.Debug.WriteLine(logContent);
                
                Console.WriteLine(logStatus);
                Console.WriteLine(logContent);
                
                System.Diagnostics.Trace.WriteLine(logStatus);
                System.Diagnostics.Trace.WriteLine(logContent);

                if (!response.IsSuccessStatusCode)
                {
                    return $"{{\"error\": \"API Error: {response.StatusCode} - {jsonData.Replace("\"", "'")}\"}}";
                }

                // Check for empty content
                if (string.IsNullOrWhiteSpace(jsonData))
                {
                     return "{\"error\": \"API returned empty response\"}";
                }

                return jsonData;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"[EXCEPTION] {ex.Message}");
            return $"{{\"error\": \"Exception generating meal plan: {ex.Message}\"}}";
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
