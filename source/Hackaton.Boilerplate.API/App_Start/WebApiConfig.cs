using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using Swashbuckle.Application;

namespace Hackaton.Boilerplate.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API enablle swagger docs & interface
            config.
                EnableSwagger(c => { c.SingleApiVersion("v1", "Hackaton"); })
                .EnableSwaggerUi();

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
