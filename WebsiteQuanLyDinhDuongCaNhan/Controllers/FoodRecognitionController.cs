using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;

namespace WebsiteQuanLyDinhDuongCaNhan.Controllers
{
    public class FoodRecognitionController : Controller
    {
        private readonly SpoonacularService _spoonacularService;

        public FoodRecognitionController()
        {
            _spoonacularService = new SpoonacularService();
        }

        public ActionResult FoodRecognition()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadImage(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength == 0)
            {
                ViewBag.Message = "Vui lòng chọn ảnh!";
                return View("FoodRecognition");
            }

            // Kiểm tra và tạo thư mục nếu chưa tồn tại
            string uploadDir = Server.MapPath("~/UploadedImages");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            // Lưu ảnh vào thư mục trên server
            string fileName = Path.GetFileName(file.FileName);
            string filePath = Path.Combine(uploadDir, fileName);
            file.SaveAs(filePath);

            byte[] imageBytes = ConvertToBytes(file);
            string foodName = await DetectFood(imageBytes);

            if (!string.IsNullOrEmpty(foodName))
            {
                var nutritionInfo = await GetNutritionInfo(foodName);
                ViewBag.FoodName = foodName;
                ViewBag.NutritionInfo = nutritionInfo;
                ViewBag.ImageUrl = Url.Content($"~/UploadedImages/{fileName}"); // Lưu URL ảnh
            }

            return View("FoodRecognition");
        }


        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            using (var inputStream = file.InputStream)
            {
                MemoryStream memoryStream = new MemoryStream();
                inputStream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        private async Task<string> DetectFood(byte[] imageBytes)
        {
            try
            {
                string apiKey = "d12825acdcc380371226629e043c3f764c337cf9"; // Thay bằng API Key thực tế
                string apiUrl = $"https://vision.googleapis.com/v1/images:annotate?key={apiKey}";

                var requestBody = new
                {
                    requests = new[]
                    {
                        new
                        {
                            image = new { content = Convert.ToBase64String(imageBytes) },
                            features = new[] { new { type = "LABEL_DETECTION", maxResults = 5 } }
                        }
                    }
                };

                using (HttpClient client = new HttpClient())
                {
                    var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestBody), System.Text.Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(apiUrl, content);
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(jsonResponse);

                    var labels = json["responses"]?[0]?["labelAnnotations"];
                    if (labels != null)
                    {
                        foreach (var label in labels)
                        {
                            string description = label["description"]?.ToString();
                            float score = float.Parse(label["score"]?.ToString() ?? "0");

                            if (!string.IsNullOrEmpty(description) && (description.ToLower().Contains("food") || score > 0.7))
                            {
                                return description;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return "Không nhận diện được món ăn";
        }

        private async Task<string> GetNutritionInfo(string foodName)
        {
            try
            {
                // Use Spoonacular to search for food nutrition
                string jsonResponse = await _spoonacularService.SearchFoodAsync(foodName, 1);
                JObject json = JObject.Parse(jsonResponse);

                var results = json["results"] as JArray;
                if (results != null && results.Count > 0)
                {
                    int ingredientId = int.Parse(results[0]["id"]?.ToString() ?? "0");
                    string name = results[0]["name"]?.ToString();

                    // Get detailed nutrition information
                    string nutritionData = await _spoonacularService.GetIngredientInformationAsync(ingredientId, 100, "grams");
                    JObject nutritionJson = JObject.Parse(nutritionData);

                    string kcal = nutritionJson["nutrition"]?["nutrients"]?.FirstOrDefault(n => n["name"]?.ToString() == "Calories")?["amount"]?.ToString() ?? "N/A";
                    string protein = nutritionJson["nutrition"]?["nutrients"]?.FirstOrDefault(n => n["name"]?.ToString() == "Protein")?["amount"]?.ToString() ?? "N/A";

                    return $"Món ăn: {name}, Calo: {kcal} kcal, Protein: {protein}g";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetNutritionInfo: " + ex.Message);
            }

            return "Không tìm thấy thông tin dinh dưỡng.";
        }
    }
}
