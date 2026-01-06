using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebsiteQuanLyDinhDuongCaNhan.Models;
using System.Data.Entity;
using System.Web.Helpers; // Thư viện bắt buộc để sửa lỗi AntiForgery

namespace WebsiteQuanLyDinhDuongCaNhan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer<dbQuanLyDinhDuong>(null);

            // GIẢI PHÁP TRIỆT ĐỂ:
            // Báo cho ASP.NET dùng trường "Name" thay vì "NameIdentifier" 
            // Điều này giúp AntiForgeryToken hoạt động được với hệ thống đăng nhập dùng Session
            AntiForgeryConfig.UniqueClaimTypeIdentifier = System.Security.Claims.ClaimTypes.Name;
        }
    }
}