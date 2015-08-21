using Hackaton.Boilerplate.Abstraction.Business;
using Hackaton.Boilerplate.Model;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hackaton.Boilerplate.API.Security
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUserAccountBusinessAsync _userAccountBusinessAsync;

        public OAuthProvider(IUserAccountBusinessAsync userAccountBusinessAsync)
        {
            _userAccountBusinessAsync = userAccountBusinessAsync;
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

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            try
            {
                var username = context.Parameters["username"];
                var password = context.Parameters["password"];

                UserAccount userFound = null;

                if (!string.IsNullOrWhiteSpace(username)
                    && !string.IsNullOrWhiteSpace(password)
                    && (userFound = await _userAccountBusinessAsync.IdentifyUser(username, password)) != null
                )
                {
                    context.OwinContext.Set("otc:username", userFound.Email);
                    context.Validated();
                }
                else
                {
                    context.SetError("Invalid credentials");
                    context.Rejected();
                }
            }
            catch(Exception ex)
            {
                context.SetError("Server error");
                context.Rejected();
            }
        }
    }
}