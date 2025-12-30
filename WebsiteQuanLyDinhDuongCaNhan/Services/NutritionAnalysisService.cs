using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class NutritionAnalysisService
{
    private readonly SpoonacularService _spoonacularService;

    public NutritionAnalysisService()
    {
        _spoonacularService = new SpoonacularService();
    }

    public async Task<JObject> GetNutritionInfoAsync(string ingredient = "")
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ingredient))
            {
                return null;
            }

            // Use Spoonacular to get ingredient nutrition
            string jsonResponse = await _spoonacularService.SearchIngredientNutritionAsync(ingredient);
            
            if (!jsonResponse.Contains("\"error\""))
            {
                System.Diagnostics.Debug.WriteLine("Spoonacular API JSON: " + jsonResponse);
                return JObject.Parse(jsonResponse);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Spoonacular API Error");
                return null;
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine("Error in GetNutritionInfoAsync: " + ex.Message);
            return null;
        }
    }
}
