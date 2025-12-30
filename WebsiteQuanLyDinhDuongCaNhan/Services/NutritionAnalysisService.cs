using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

public class NutritionAnalysisService
{
    public async Task<JObject> GetNutritionInfoAsync(string ingredient="")
    {
        string url = $"https://api.edamam.com/api/nutrition-data?app_id=26a45bf8&app_key=72311f618c619600f6d6e59358a19358&ingr={ingredient}";

        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string json = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("API JSON: " + json); // Debug JSON
                return JObject.Parse(json);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("API Error: " + response.StatusCode);
                return null;
            }
        }
    }

}
