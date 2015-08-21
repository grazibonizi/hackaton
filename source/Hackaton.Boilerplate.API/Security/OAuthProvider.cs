using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.API.Security
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        public OAuthProvider()
        {

        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var identity = new ClaimsIdentity("otc");
            var username = context.OwinContext.Get<string>("otc:username");
            
            identity.AddClaim(new Claim(ClaimTypes.Name, username));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            context.Validated(identity);
            return Task.FromResult(0);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];

                if (!string.IsNullOrWhiteSpace(username)
                    && !string.IsNullOrWhiteSpace(password)
                    && username.Equals("admin")
                    && password.Equals("1234")
                )
                {
                    context.OwinContext.Set("otc:username", username);
                    context.Validated();
                }
                else
                {
                    context.SetError("Invalid credentials");
                    context.Rejected();
                }
            }
            catch
            {
                context.SetError("Server error");
                context.Rejected();
            }
            return Task.FromResult(0);
        }
    }
}