using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using System;

namespace Hackaton.Boilerplate.API.Security
{
    public class OAuthOptions : OAuthAuthorizationServerOptions
    {
        public OAuthOptions()
        {
            TokenEndpointPath = new PathString("/token");
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60);
            AccessTokenFormat = new JwtFormat(this);
            Provider = new OAuthProvider();
#if DEBUG
            AllowInsecureHttp = true;
#endif
        }
    }
}