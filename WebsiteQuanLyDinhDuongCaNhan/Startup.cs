using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using System;

[assembly: OwinStartup(typeof(WebsiteQuanLyDinhDuongCaNhan.Startup))]

namespace WebsiteQuanLyDinhDuongCaNhan
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Cookie Authentication
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = CookieAuthenticationDefaults.AuthenticationType,
                LoginPath = new PathString("/Auth/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(60),
                SlidingExpiration = true
            });

            // Google Authentication (SAFE)
            var googleOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_ID"),
                ClientSecret = Environment.GetEnvironmentVariable("GOOGLE_CLIENT_SECRET"),
                CallbackPath = new PathString("/Auth/GoogleResponse"),
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                SignInAsAuthenticationType = CookieAuthenticationDefaults.AuthenticationType
            };

            app.UseGoogleAuthentication(googleOptions);
        }
    }
}
