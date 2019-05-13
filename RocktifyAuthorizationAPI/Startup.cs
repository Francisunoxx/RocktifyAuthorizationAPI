using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using RocktifyAuthorizationAPI.Providers;

[assembly: OwinStartup(typeof(RocktifyAuthorizationAPI.Startup))]

namespace RocktifyAuthorizationAPI
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            var webApiConfiguration = new HttpConfiguration();
            // Enable attribute routing
            webApiConfiguration.MapHttpAttributeRoutes();
            webApiConfiguration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            app.UseOAuthAuthorizationServer(new OAuthOptions());
            app.UseWebApi(webApiConfiguration);
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
