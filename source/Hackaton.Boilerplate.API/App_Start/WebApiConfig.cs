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
