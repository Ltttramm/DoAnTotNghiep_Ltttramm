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
        private const string EdamamApiId = "984cb63e";
        private const string EdamamApiKey = "2bbc5eb36a164d034f0a90c78b9458fb";

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
            string apiUrl = $"https://api.edamam.com/api/food-database/v2/parser?ingr={foodName}&app_id={EdamamApiId}&app_key={EdamamApiKey}";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(apiUrl);
                var content = await response.Content.ReadAsStringAsync();
                JObject jsonResponse = JObject.Parse(content);

                var hints = jsonResponse["hints"] as JArray;
                if (hints != null && hints.Count > 0)
                {
                    var food = hints[0]["food"];
                    if (food != null)
                    {
                        string name = food["label"]?.ToString();
                        string kcal = food["nutrients"]?["ENERC_KCAL"]?.ToString() ?? "Không có dữ liệu";
                        string protein = food["nutrients"]?["PROCNT"]?.ToString() ?? "Không có dữ liệu";

                        return $"Món ăn: {name}, Calo: {kcal} kcal, Protein: {protein}g";
                    }
                }
            }

            return "Không tìm thấy thông tin dinh dưỡng.";
        }
    }
}
