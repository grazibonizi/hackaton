using System.Web.Http;
using DryIoc;
using DryIoc.WebApi;
using Hackaton.Boilerplate.Bind;

namespace Hackaton.Boilerplate.API
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static volatile IContainer Container = new Container();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            BusinessBinder.Setup(Container);
            DAOBinder.Setup(Container);
            InternalsBinder.Setup(Container);

            Container.WithWebApi(GlobalConfiguration.Configuration);
        }
    }
}
