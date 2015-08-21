using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Hackaton.Boilerplate.Bind;
using DryIoc;
using DryIoc.WebApi;
using Hackaton.Boilerplate.API.Security;

[assembly: OwinStartup(typeof(Hackaton.Boilerplate.API.Startup))]

namespace Hackaton.Boilerplate.API
{
    public partial class Startup
    {
        public static volatile IContainer Container = new Container();

        public Startup()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            BusinessBinder.Setup(Container);
            DAOBinder.Setup(Container);
            InternalsBinder.Setup(Container);

            Container.WithWebApi(GlobalConfiguration.Configuration);
        }

        public void Configuration(IAppBuilder app)
        {
            //SecurityTokenBuilder.CreateFromKey(string)
            app.UseOAuthAuthorizationServer(new OAuthOptions());
            app.UseJwtBearerAuthentication(new JwtOptions());
            //app.UseCors(CorsOptions.AllowAll);
        }
    }
    
}
