using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteQuanLyDinhDuongCaNhan.Models
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }

    public class ResetPasswordRequest
    {
        public string Token { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }

}