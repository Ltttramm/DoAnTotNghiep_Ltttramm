//using System;
//using System.Linq;
//using System.Security.Claims;
//using System.Security.Cryptography;
//using System.Text;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Security;
//using System.Windows.Forms;
//using Microsoft.AspNet.Identity;
//using Microsoft.Owin.Security;
//using Microsoft.Owin.Security.Cookies;
//using WebsiteQuanLyDinhDuongCaNhan.Models;

//namespace WebsiteQuanLyDinhDuongCaNhan.Controllers
//{
//    public class AuthController : Controller
//    {
//        private dbQuanLyDinhDuong db = new dbQuanLyDinhDuong();

//        // Hiển thị trang Login
//        public ActionResult Login()
//        {
//            ViewData["HideHeader"] = true;
//            return View();
//        }

//        //      [HttpPost]
//        //      public JsonResult Login(string email, string password)
//        //      {
//        //          if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
//        //          {
//        //              return Json(new { success = false, message = "Vui lòng nhập email và mật khẩu." });
//        //          }

//        //          var user = db.Users.FirstOrDefault(u => u.Email == email);

//        //          if (user == null)
//        //          {
//        //              return Json(new { success = false, message = "Email chưa được đăng ký." });
//        //          }

//        //          if (!user.IsEmailConfirmed)
//        //          {
//        //              return Json(new { success = false, message = "Tài khoản chưa được xác thực. Vui lòng kiểm tra email và xác nhận tài khoản." });
//        //          }

//        //          string hashedPassword = HashPassword(password);
//        //          if (user.PasswordHash != hashedPassword)
//        //          {
//        //              return Json(new { success = false, message = "Mật khẩu không đúng, vui lòng nhập lại." });
//        //          }

//        //          // Đăng nhập thành công
//        //          Session["UserEmail"] = user.Email;
//        //          Session["UserName"] = user.FullName;

//        //          var authManager = HttpContext.GetOwinContext().Authentication;
//        //          var claims = new ClaimsIdentity(new[]
//        //          {
//        //  new Claim(ClaimTypes.Email, user.Email),
//        //  new Claim(ClaimTypes.Name, user.FullName)
//        //}, CookieAuthenticationDefaults.AuthenticationType);

//        //          authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claims);

//        //          return Json(new { success = true, redirectUrl = Url.Action("Index", "Home") });
//        //      }

//        [HttpPost]
//        public JsonResult Login(string email, string password)
//        {
//            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
//            {
//                return Json(new { success = false, message = "Vui lòng nhập email và mật khẩu." });
//            }

//            string redirectUrl = "";
//            string userRole = "";
//            string userName = "";

//            // 1. KIỂM TRA XEM CÓ PHẢI ADMIN DUY NHẤT KHÔNG (Check cứng trước)
//            if (email.ToLower() == "admin@gmail.com" && password == "admin123")
//            {
//                userRole = "Admin";
//                userName = "Quản trị viên";
//                redirectUrl = Url.Action("Dashboard", "Admin");
//            }
//            else
//            {
//                // 2. NẾU KHÔNG PHẢI ADMIN THÌ MỚI TÌM TRONG DATABASE ĐỂ CHECK USER
//                var user = db.Users.FirstOrDefault(u => u.Email == email);

//                if (user == null)
//                {
//                    return Json(new { success = false, message = "Email chưa được đăng ký." });
//                }

//                // Kiểm tra mật khẩu User thường
//                string hashedPassword = HashPassword(password);
//                if (user.PasswordHash != hashedPassword)
//                {
//                    return Json(new { success = false, message = "Mật khẩu không đúng." });
//                }

//                // Kiểm tra xác thực email cho User thường
//                if (!user.IsEmailConfirmed)
//                {
//                    return Json(new { success = false, message = "Tài khoản chưa được xác thực email." });
//                }

//                userRole = "User";
//                userName = user.FullName;
//                redirectUrl = Url.Action("Index", "Home");
//            }

//            // 3. THIẾT LẬP SESSION VÀ ĐĂNG NHẬP (Cho cả Admin và User)
//            Session["UserEmail"] = email;
//            Session["UserName"] = userName;
//            Session["UserRole"] = userRole;

//            var authManager = HttpContext.GetOwinContext().Authentication;
//            var claims = new ClaimsIdentity(new[]
//            {
//        new Claim(ClaimTypes.Email, email),
//        new Claim(ClaimTypes.Name, userName),
//        new Claim(ClaimTypes.Role, userRole)
//    }, CookieAuthenticationDefaults.AuthenticationType);

//            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claims);

//            return Json(new { success = true, redirectUrl = redirectUrl });
//        }
//        // Google Login Redirect
//        public void GoogleLogin()
//        {
//            var properties = new AuthenticationProperties
//            {
//                RedirectUri = Url.Action("Index", "Home", null, Request.Url.Scheme)
//            };

//            HttpContext.GetOwinContext().Authentication.Challenge(properties, "Google");
//        }

//        // Google Callback - Xử lý phản hồi sau khi đăng nhập
//        public ActionResult GoogleResponse()
//        {
//            var authResult = HttpContext.GetOwinContext().Authentication.AuthenticateAsync("ExternalCookie").Result;

//            if (authResult == null || authResult.Identity == null)
//            {
//                Console.WriteLine("Google Authentication Failed");
//                TempData["ErrorMessage"] = "Google authentication failed. Please try again.";
//                return RedirectToAction("Login");
//            }

//            var identity = authResult.Identity;
//            var email = identity.FindFirst(ClaimTypes.Email)?.Value;
//            var name = identity.FindFirst(ClaimTypes.Name)?.Value;

//            if (string.IsNullOrEmpty(email))
//            {
//                Console.WriteLine("No email found in Google response.");
//                TempData["ErrorMessage"] = "Google login failed. Email not found.";
//                return RedirectToAction("Login");
//            }

//            // Lưu session đăng nhập
//            Session["UserEmail"] = email;
//            Session["UserName"] = name;
//            Console.WriteLine($"User logged in: {email}");

//            // Đăng nhập bằng Cookie Authentication
//            var authManager = HttpContext.GetOwinContext().Authentication;
//            authManager.SignOut("ExternalCookie");

//            // Tạo Claims Identity mới
//            var claims = new ClaimsIdentity(new[]
//      {
//        new Claim(ClaimTypes.Email, email),
//        new Claim(ClaimTypes.Name, name)
//      }, CookieAuthenticationDefaults.AuthenticationType);

//            authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claims);

//            Console.WriteLine("Redirecting to Home/Index...");

//            // Kiểm tra nếu có lỗi redirect
//            try
//            {
//                return RedirectToAction("Index", "Home");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine("Redirect failed: " + ex.Message);
//                return RedirectToAction("Login");
//            }
//        }


//        //// Đăng xuất
//        //public ActionResult Logout()
//        //{
//        //    HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
//        //    Session.Clear();
//        //    return RedirectToAction("Login");
//        //}

//        // Hiển thị trang đăng ký
//        public ActionResult Register()
//        {
//            return View();
//        }

//        // Xử lý đăng ký tài khoản
//        //[HttpPost]
//        //public ActionResult Register(string FullName, string Email, string Password, string ConfirmPassword)
//        //{
//        //    if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
//        //    {
//        //        ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
//        //        return View();
//        //    }

//        //    if (Password != ConfirmPassword)
//        //    {
//        //        ModelState.AddModelError("", "Mật khẩu xác nhận không khớp.");
//        //        return View();
//        //    }

//        //    // Kiểm tra xem email đã tồn tại chưa
//        //    var existingUser = db.Users.FirstOrDefault(u => u.Email == Email);
//        //    if (existingUser != null)
//        //    {
//        //        ModelState.AddModelError("", "Email đã được đăng ký.");
//        //        return View();
//        //    }

//        //    // Mã hóa mật khẩu
//        //    string hashedPassword = HashPassword(Password);

//        //    // Tạo tài khoản mới
//        //    User newUser = new User
//        //    {
//        //        FullName = FullName,
//        //        Email = Email,
//        //        PasswordHash = hashedPassword,
//        //        CreatedAt = DateTime.Now
//        //    };

//        //    db.Users.Add(newUser);
//        //    db.SaveChanges();

//        //    TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng đăng nhập.";
//        //    return RedirectToAction("Login");
//        //}
//        [HttpPost]
//        public ActionResult Register(string FullName, string Email, string Password, string ConfirmPassword)
//        {
//            if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(ConfirmPassword))
//            {
//                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
//                return View();
//            }

//            if (Password != ConfirmPassword)
//            {
//                ModelState.AddModelError("", "Mật khẩu xác nhận không khớp.");
//                return View();
//            }

//            var existingUser = db.Users.FirstOrDefault(u => u.Email == Email);
//            if (existingUser != null)
//            {
//                ModelState.AddModelError("", "Email đã được đăng ký.");
//                return View();
//            }

//            // Mã hóa mật khẩu
//            string hashedPassword = HashPassword(Password);

//            // Tạo mã xác thực email
//            string verificationToken = Guid.NewGuid().ToString();

//            // Tạo tài khoản mới
//            User newUser = new User
//            {
//                FullName = FullName,
//                Email = Email,
//                PasswordHash = hashedPassword,
//                IsEmailConfirmed = false, // Chưa xác thực email
//                EmailVerificationToken = verificationToken,
//                CreatedAt = DateTime.Now
//            };

//            db.Users.Add(newUser);
//            db.SaveChanges();

//            // Gửi email xác thực
//            string verificationLink = Url.Action("ConfirmEmail", "Auth", new { token = verificationToken }, Request.Url.Scheme);
//            SendVerificationEmail(Email, verificationLink);

//            TempData["SuccessMessage"] = "Đăng ký thành công! Vui lòng kiểm tra email để xác nhận tài khoản.";
//            return View();
//        }

//        private void SendVerificationEmail(string email, string verificationLink)
//        {
//            string fromEmail = "lethithuytram140803@gmail.com";
//            string password = "odfvscbjxifsmeqb"; // Sử dụng App Password thay vì mật khẩu tài khoản

//            string subject = "Xác nhận tài khoản của bạn";
//            string body = $@"
//        <p>Xin chào,</p>
//        <p>Vui lòng nhấp vào liên kết sau để xác nhận tài khoản của bạn:</p>
//        <p><a href='{verificationLink}' style='padding:10px;background-color:#007bff;color:white;text-decoration:none;border-radius:5px;'>Xác nhận email</a></p>
//        <p>Nếu bạn không đăng ký tài khoản, vui lòng bỏ qua email này.</p>";

//            try
//            {
//                using (var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
//                {
//                    smtp.Port = 587;
//                    smtp.Credentials = new System.Net.NetworkCredential(fromEmail, password);
//                    smtp.EnableSsl = true;

//                    var message = new System.Net.Mail.MailMessage();
//                    message.From = new System.Net.Mail.MailAddress(fromEmail, "Hệ thống xác thực tài khoản");
//                    message.To.Add(email);
//                    message.Subject = subject;
//                    message.Body = body;
//                    message.IsBodyHtml = true;

//                    smtp.Send(message);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
//            }
//        }

//        public void SendVerificationEmailsToUnconfirmedUsers()
//        {
//            var unconfirmedUsers = db.Users.Where(u => !u.IsEmailConfirmed).ToList();

//            foreach (var user in unconfirmedUsers)
//            {
//                string verificationLink = Url.Action("ConfirmEmail", "Auth", new { token = user.EmailVerificationToken }, Request.Url.Scheme);
//                SendVerificationEmail(user.Email, verificationLink);
//            }
//        }

//        private string HashPassword(string password)
//        {
//            using (SHA256 sha256 = SHA256.Create())
//            {
//                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
//                byte[] hashBytes = sha256.ComputeHash(inputBytes);
//                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
//            }
//        }

//        public ActionResult ConfirmEmail(string token)
//        {
//            var user = db.Users.FirstOrDefault(u => u.EmailVerificationToken == token);

//            if (user == null)
//            {
//                TempData["ErrorMessage"] = "Mã xác thực không hợp lệ hoặc đã hết hạn.";
//                return RedirectToAction("Login");
//            }

//            user.IsEmailConfirmed = true;
//            user.EmailVerificationToken = null; // Xóa token sau khi xác thực
//            db.SaveChanges();

//            TempData["SuccessMessage"] = "Xác thực email thành công! Bạn có thể đăng nhập ngay bây giờ.";
//            return RedirectToAction("Login");
//        }

//        // Hiển thị trang quên mật khẩu
//        [HttpGet]
//        public ActionResult ForgotPassword()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult ForgotPassword(string Email)
//        {
//            if (string.IsNullOrEmpty(Email))
//            {
//                ModelState.AddModelError("", "Vui lòng nhập email hợp lệ.");
//                return View();
//            }

//            var user = db.Users.FirstOrDefault(u => u.Email == Email);
//            if (user == null)
//            {
//                ModelState.AddModelError("", "Email không tồn tại trong hệ thống.");
//                return View();
//            }

//            // Tạo token đặt lại mật khẩu
//            user.PasswordResetToken = Guid.NewGuid().ToString();
//            user.ResetTokenExpiry = DateTime.Now.AddHours(1);
//            db.SaveChanges();

//            // Tạo link đặt lại mật khẩu
//            string resetLink = Url.Action("ResetPassword", "Auth", new { token = user.PasswordResetToken }, Request.Url.Scheme);


//            // Gửi email
//            SendResetPasswordEmail(Email, resetLink);

//            TempData["Message"] = "Hướng dẫn đặt lại mật khẩu đã được gửi đến email của bạn.";
//            return RedirectToAction("Login");
//        }
//        private void SendResetPasswordEmail(string email, string resetLink)
//        {
//            string fromEmail = "lethithuytram140803@gmail.com";
//            string password = "odfvscbjxifsmeqb"; // Sử dụng App Password thay vì mật khẩu tài khoản

//            string subject = "Đặt lại mật khẩu của bạn";
//            string body = $@"
//<p>Xin chào,</p>
//<p>Bạn đã yêu cầu đặt lại mật khẩu cho tài khoản của mình.</p>
//<p>Vui lòng nhấp vào liên kết sau để đặt lại mật khẩu:</p>
//<p><a href='{resetLink}' style='padding:10px;background-color:#007bff;color:white;text-decoration:none;border-radius:5px;'>Đặt lại mật khẩu</a></p>
//<p>Nếu bạn không yêu cầu đặt lại mật khẩu, vui lòng bỏ qua email này.</p>";

//            try
//            {
//                using (var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
//                {
//                    smtp.Port = 587;
//                    smtp.Credentials = new System.Net.NetworkCredential(fromEmail, password);
//                    smtp.EnableSsl = true;

//                    var message = new System.Net.Mail.MailMessage();
//                    message.From = new System.Net.Mail.MailAddress(fromEmail, "Hệ thống đặt lại mật khẩu");
//                    message.To.Add(email);
//                    message.Subject = subject;
//                    message.Body = body;
//                    message.IsBodyHtml = true;

//                    smtp.Send(message);
//                }
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
//            }
//        }


//        public ActionResult ResetPassword(string token)
//        {
//            // Tìm user có token hợp lệ và chưa hết hạn
//            var user = db.Users.FirstOrDefault(u => u.PasswordResetToken == token && u.ResetTokenExpiry > DateTime.Now);

//            if (user == null)
//            {
//                TempData["ErrorMessage"] = "Liên kết đặt lại mật khẩu không hợp lệ hoặc đã hết hạn.";
//                return RedirectToAction("ForgotPassword"); // Chuyển hướng về trang quên mật khẩu
//            }

//            // Gửi email của user qua ViewBag (không cần lấy từ URL)
//            ViewBag.Token = token;
//            ViewBag.Email = user.Email;

//            return View();
//        }


//        [HttpPost]
//        public ActionResult ResetPassword(ResetPasswordViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            var user = db.Users.FirstOrDefault(u => u.PasswordResetToken == model.Token);
//            if (user == null)
//            {
//                TempData["ErrorMessage"] = "Mã đặt lại mật khẩu không hợp lệ hoặc đã hết hạn.";
//                return RedirectToAction("Login");
//            }

//            user.PasswordHash = HashPassword(model.NewPassword); // Cần mã hóa mật khẩu
//            user.PasswordResetToken = null; // Xóa token sau khi đặt lại
//            db.SaveChanges();

//            TempData["SuccessMessage"] = "Mật khẩu đã được đặt lại thành công.";
//            return RedirectToAction("Login");
//        }
//        public ActionResult Logout()
//        {
//            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);


//            return RedirectToAction("Index", "Home"); // Quay về trang chủ
//        }

//        public ActionResult Profile()
//        {
//            if (!User.Identity.IsAuthenticated) // Kiểm tra nếu chưa đăng nhập
//            {
//                return RedirectToAction("Login", "Auth");
//            }

//            // Lấy tên đăng nhập từ Identity
//            string username = User.Identity.Name;

//            // Tìm thông tin người dùng theo Username
//            var user = db.Users.FirstOrDefault(u => u.FullName == username);
//            if (user == null)
//            {
//                return HttpNotFound();
//            }

//            // Truyền thông tin vào ViewModel
//            var model = new ProfileViewModel
//            {
//                Id = user.UserID, // ID của người dùng
//                FullName = user.FullName,
//                Email = user.Email,
//                DateOfBirth = user.DateOfBirth,
//                //Age = user.Age,
//                Gender = user.Gender,
//                Height = user.Height,
//                Weight = user.Weight,
//                ActivityLevel = user.ActivityLevel,
//                Goal = user.Goal,
//                PreferredDiet = user.PreferredDiet,
//                Allergy = user.Allergy,

//            };

//            return View(model);
//        }

//        [HttpPost]
//        public JsonResult Update(ProfileViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                string username = User.Identity.Name;
//                var user = db.Users.FirstOrDefault(u => u.FullName == username);

//                if (user == null)
//                {
//                    return Json(new { success = false, message = "Không tìm thấy người dùng!" });
//                }

//                // Cập nhật thông tin
//                user.FullName = model.FullName;
//                user.DateOfBirth = model.DateOfBirth;
//                //user.Age = model.Age;
//                user.Gender = model.Gender;
//                user.Height = model.Height;
//                user.Weight = model.Weight;
//                user.ActivityLevel = model.ActivityLevel;
//                user.Goal = model.Goal;
//                user.PreferredDiet = model.PreferredDiet;
//                user.Allergy = model.Allergy;
//                db.SaveChanges();

//                return Json(new { success = true });
//            }

//            return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
//        }

//    }
//}

using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using WebsiteQuanLyDinhDuongCaNhan.Models;

namespace WebsiteQuanLyDinhDuongCaNhan.Controllers
{
    public class AuthController : Controller
    {
        private dbQuanLyDinhDuong db = new dbQuanLyDinhDuong();

        // 1. Trang Login
        public ActionResult Login()
        {
            ViewData["HideHeader"] = true;
            return View();
        }

        [HttpPost]
        public JsonResult Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return Json(new { success = false, message = "Vui lòng nhập email và mật khẩu." });

            string redirectUrl = "";
            string userRole = "User";
            string userName = "";

            if (email.ToLower() == "admin@gmail.com" && password == "admin123")
            {
                userRole = "Admin";
                userName = "Quản trị viên";
                redirectUrl = Url.Action("Dashboard", "Admin");
            }
            else
            {
                var user = db.Users.FirstOrDefault(u => u.Email == email);
                if (user == null) return Json(new { success = false, message = "Email chưa được đăng ký." });
                if (!user.IsEmailConfirmed) return Json(new { success = false, message = "Tài khoản chưa được xác thực email." });

                string hashedPassword = HashPassword(password);
                if (user.PasswordHash != hashedPassword) return Json(new { success = false, message = "Mật khẩu không đúng." });

                userName = user.FullName;
                redirectUrl = Url.Action("Index", "Home");
            }

            // Đăng nhập Claims
            var claims = new ClaimsIdentity(new[] {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.Role, userRole)
            }, CookieAuthenticationDefaults.AuthenticationType);

            HttpContext.GetOwinContext().Authentication.SignIn(new AuthenticationProperties { IsPersistent = true }, claims);

            Session["UserEmail"] = email;
            Session["UserName"] = userName;

            return Json(new { success = true, redirectUrl = redirectUrl });
        }

        // 2. Trang Đăng ký (Sửa dựa trên hàm cũ của bạn)
        public ActionResult Register() => View();

        [HttpPost]
        public ActionResult Register(string FullName, string Email, string Password, string ConfirmPassword)
        {
            if (string.IsNullOrEmpty(FullName) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ thông tin.");
                return View();
            }

            if (Password != ConfirmPassword)
            {
                ModelState.AddModelError("", "Mật khẩu xác nhận không khớp.");
                return View();
            }

            if (db.Users.Any(u => u.Email == Email))
            {
                ModelState.AddModelError("", "Email đã được đăng ký.");
                return View();
            }

            string verificationToken = Guid.NewGuid().ToString();
            User newUser = new User
            {
                FullName = FullName,
                Email = Email,
                PasswordHash = HashPassword(Password),
                IsEmailConfirmed = false,
                EmailVerificationToken = verificationToken,
                CreatedAt = DateTime.Now
                // Các trường như Height, Weight sẽ để mặc định là null hoặc 0
            };

            db.Users.Add(newUser);
            db.SaveChanges(); // Lưu để có UserID

            // Gửi email
            string verificationLink = Url.Action("ConfirmEmail", "Auth", new { token = verificationToken }, Request.Url.Scheme);
            SendVerificationEmail(Email, verificationLink);

            TempData["SuccessMessage"] = "Đăng ký thành công! Hãy kiểm tra email để xác nhận.";
            return View();
        }

        // 3. Trang Profile
        public ActionResult Profile()
{
    if (!User.Identity.IsAuthenticated) return RedirectToAction("Login");

    string userEmail = Session["UserEmail"]?.ToString(); 
    if (string.IsNullOrEmpty(userEmail)) {
        // Nếu mất session, lấy từ Identity (nếu bạn cấu hình ClaimTypes.Name là email)
        userEmail = User.Identity.Name; 
    }

    var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
    if (user == null) return HttpNotFound();

            var model = new ProfileViewModel
            {
                Id = user.UserID,
                FullName = user.FullName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Height = (double?)user.Height,
                Weight = (double?)user.Weight,
                ActivityLevel = user.ActivityLevel,
                Goal = user.Goal,
                PreferredDiet = user.PreferredDiet,
                Allergy = user.Allergy
            };

            return View(model);
        }

        // 4. Xử lý Update Profile (AJAX)
        [HttpPost]
        public JsonResult Update(ProfileViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Tìm user dựa trên ID gửi từ View
                    var user = db.Users.Find(model.Id);

                    if (user == null)
                    {
                        return Json(new { success = false, message = "Không tìm thấy người dùng!" });
                    }

                    // Cập nhật thông tin từ model vào database
                    user.FullName = model.FullName;
                    user.DateOfBirth = model.DateOfBirth;
                    user.Gender = model.Gender;
                    user.Height = (decimal?)model.Height;
                    user.Weight = (decimal?)model.Weight;
                    user.ActivityLevel = model.ActivityLevel;
                    user.Goal = model.Goal;
                    user.PreferredDiet = model.PreferredDiet;
                    user.Allergy = model.Allergy;

                    // Đánh dấu là thực thể đã bị thay đổi để EF cập nhật
                    db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    return Json(new { success = true, message = "Cập nhật thành công!" });
                }

                return Json(new { success = false, message = "Dữ liệu không hợp lệ!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Lỗi: " + ex.Message });
            }
        }

        // --- Helpers ---
        public ActionResult ConfirmEmail(string token)
        {
            var user = db.Users.FirstOrDefault(u => u.EmailVerificationToken == token);
            if (user != null)
            {
                user.IsEmailConfirmed = true;
                user.EmailVerificationToken = null;
                db.SaveChanges();
                TempData["SuccessMessage"] = "Xác thực thành công! Mời bạn đăng nhập.";
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(CookieAuthenticationDefaults.AuthenticationType);
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }

        private void SendVerificationEmail(string email, string link)
        {
            // Logic gửi mail của bạn (Giữ nguyên phần SmtpClient cũ)
            string fromEmail = "lethithuytram140803@gmail.com";
            string password = "odfvscbjxifsmeqb";
            try
            {
                using (var smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com"))
                {
                    smtp.Port = 587;
                    smtp.Credentials = new System.Net.NetworkCredential(fromEmail, password);
                    smtp.EnableSsl = true;
                    var mess = new System.Net.Mail.MailMessage(fromEmail, email, "Xác nhận tài khoản", $"Click vào đây: {link}");
                    mess.IsBodyHtml = true;
                    smtp.Send(mess);
                }
            }
            catch { }
        }
    }
}