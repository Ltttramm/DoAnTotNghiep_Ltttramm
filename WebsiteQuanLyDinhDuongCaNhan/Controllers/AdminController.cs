using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebsiteQuanLyDinhDuongCaNhan.Models;
using System.Data.Entity;

namespace WebsiteQuanLyDinhDuongCaNhan.Controllers
{
    public class AdminController : Controller
    {
        private dbQuanLyDinhDuong db = new dbQuanLyDinhDuong();

        // Middleware kiểm tra quyền Admin
        private bool IsAdmin()
        {
            return Session["UserRole"]?.ToString() == "Admin";
        }

        // --- 1. TỔNG QUAN (DASHBOARD) ---
        public ActionResult Dashboard()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");

            ViewBag.UserCount = db.Users.Count();
            ViewBag.FoodCount = db.Foods.Count();

            var latestUsers = db.Users.OrderByDescending(u => u.CreatedAt).Take(5).ToList();
            return View(latestUsers);
        }

        // --- 2. QUẢN LÝ NGƯỜI DÙNG ---
        public ActionResult ManageUsers()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            return View(db.Users.OrderByDescending(u => u.CreatedAt).ToList());
        }

        public ActionResult EditUser(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            var user = db.Users.Find(id);
            if (user == null) return HttpNotFound();
            return View(user);
        }

        [HttpPost]
        public ActionResult EditUser(User user)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            try
            {
                var existingUser = db.Users.Find(user.UserID);
                if (existingUser != null)
                {
                    existingUser.FullName = user.FullName;
                    db.Entry(existingUser).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Dashboard");
                }
            }
            catch (Exception ex) { ModelState.AddModelError("", "Lỗi: " + ex.Message); }
            return View(user);
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            if (!IsAdmin()) return Json(new { success = false });
            try
            {
                var user = db.Users.Find(id);
                if (user != null)
                {
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch { return Json(new { success = false, message = "Dữ liệu đang được sử dụng!" }); }
        }

        // --- 3. QUẢN LÝ MÓN ĂN (FOODS) ---

        // Hiển thị danh sách món ăn
        public ActionResult ManageFoods()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            var allFoods = db.Foods.OrderByDescending(f => f.FoodID).ToList();
            return View(allFoods);
        }

        // Thêm món nội bộ (Ajax)
        [HttpPost]
        public JsonResult AddInternalFood(Food food)
        {
            if (!IsAdmin()) return Json(new { success = false, message = "Hết phiên làm việc" });
            try
            {
                food.IsVisible = true;

                db.Foods.Add(food);
                db.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex) { return Json(new { success = false, message = ex.Message }); }
        }

        // Trang sửa món ăn (GET)
        public ActionResult EditFood(int id)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            var food = db.Foods.Find(id);
            if (food == null) return HttpNotFound();
            return View(food);
        }

        // Lưu sửa món ăn (POST)
        [HttpPost]
        public ActionResult EditFood(Food food)
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            try
            {
                var f = db.Foods.Find(food.FoodID);
                if (f != null)
                {
                    f.FoodName = food.FoodName;
                    f.Calories = food.Calories;
                    f.ImageURL = food.ImageURL;
                    f.Category = food.Category;
                    db.Entry(f).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Cập nhật thành công!";
                    return RedirectToAction("ManageFoods");
                }
            }
            catch (Exception ex) { ModelState.AddModelError("", "Lỗi: " + ex.Message); }
            return View(food);
        }

        // Ẩn/Hiện món ăn
        [HttpPost]
        public JsonResult ToggleFoodVisibility(int id)
        {
            if (!IsAdmin()) return Json(new { success = false });
            var food = db.Foods.Find(id);
            if (food != null)
            {
                food.IsVisible = !(food.IsVisible ?? true);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

        // Xóa món ăn
        [HttpPost]
        public JsonResult DeleteFood(int id)
        {
            if (!IsAdmin()) return Json(new { success = false });
            try
            {
                var food = db.Foods.Find(id);
                if (food != null)
                {
                    db.Foods.Remove(food);
                    db.SaveChanges();
                    return Json(new { success = true });
                }
                return Json(new { success = false });
            }
            catch { return Json(new { success = false, message = "Thực phẩm này đang có dữ liệu liên quan!" }); }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }
        // Trang tìm kiếm món ăn từ API Spoonacular
        public ActionResult SearchApiFood()
        {
            if (!IsAdmin()) return RedirectToAction("Login", "Auth");
            return View();
        }
        // --- HÀM PHÊ DUYỆT MÓN TỪ API ---
        [HttpPost]
        public JsonResult ApproveApiFood(string apiId, string name, double calo, string img, string category)
        {
            if (!IsAdmin()) return Json(new { success = false, message = "Hết phiên làm việc hoặc không có quyền!" });

            try
            {
                // 1. Kiểm tra xem món này đã được duyệt trước đó chưa (dựa vào ExternalApiID)
                var existingFood = db.Foods.FirstOrDefault(f => f.ExternalApiID == apiId);
                if (existingFood != null)
                {
                    return Json(new { success = false, message = "Món ăn này đã tồn tại trong hệ thống của bạn!" });
                }

                // 2. Tạo đối tượng Food mới dựa trên Model của bạn
                var newFood = new Food
                {
                    FoodName = name,
                    Calories = calo,
                    ImageURL = img,
                    Category = category,
                    ExternalApiID = apiId, // Lưu ID của Spoonacular để sau này không bị trùng
                    IsVisible = true       // Mặc định cho hiện lên
                };

                // 3. Lưu vào database
                db.Foods.Add(newFood);
                db.SaveChanges();

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                // Trả về thông báo lỗi cụ thể để dễ debug
                return Json(new { success = false, message = "Lỗi hệ thống: " + ex.Message });
            }
        }
    }

}