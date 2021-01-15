using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;

[assembly: OwinStartup(typeof(OwinWebApp.Startup))]

namespace OwinWebApp
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions { 
                AuthenticationType = "ApplicationCookie",
                LoginPath = new PathString("/Authentication/Login"),
                ExpireTimeSpan = TimeSpan.FromMinutes(60)
            });
        }
    }
}
