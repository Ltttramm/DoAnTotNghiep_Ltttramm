using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using WebsiteQuanLyDinhDuongCaNhan.Models;

[assembly: OwinStartup(typeof(WebsiteQuanLyDinhDuongCaNhan.Startup))]

namespace WebsiteQuanLyDinhDuongCaNhan
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Cấu hình Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/Auth/Login"),
                ExpireTimeSpan = System.TimeSpan.FromMinutes(60),
                SlidingExpiration = true
            });

            // Cấu hình Google Authentication
            var googleOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "600351066363-6c5fadje9iabfvaf8p6s1fn89kltfgov.apps.googleusercontent.com",
                ClientSecret = "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",
                CallbackPath = new PathString("/Auth/GoogleResponse"),
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            };

            app.UseGoogleAuthentication(googleOptions);



        }

    }
}
