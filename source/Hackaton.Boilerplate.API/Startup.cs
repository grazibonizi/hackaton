using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Hackaton.Boilerplate.Bind;
using DryIoc;
using DryIoc.WebApi;
using Hackaton.Boilerplate.API.Security;
using Hackaton.Boilerplate.Abstraction.Business;

[assembly: OwinStartup(typeof(Hackaton.Boilerplate.API.Startup))]

namespace Hackaton.Boilerplate.API
{
    public partial class Startup
    {
        public static volatile IContainer _container = new Container();

        public Startup()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            BusinessBinder.Setup(_container);
            DAOBinder.Setup(_container);
            InternalsBinder.Setup(_container);

            _container.WithWebApi(GlobalConfiguration.Configuration);
        }
        
        public void Configuration(IAppBuilder app)
        {
            var userAccountBusiness = _container.Resolve<IUserAccountBusinessAsync>();
            app.UseOAuthAuthorizationServer(new OAuthOptions(userAccountBusiness));
            app.UseJwtBearerAuthentication(new JwtOptions());
        }
    }

}
