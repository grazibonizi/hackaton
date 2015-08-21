using Microsoft.Owin.Security.Jwt;
using System;

namespace Hackaton.Boilerplate.API.Security
{
    public class JwtOptions : JwtBearerAuthenticationOptions
    {
        public JwtOptions()
        {
            var issuer = "localhost";
            var audience = "all";
            var key = Convert.FromBase64String("YQBiAGMAZABlAGYAZwBoAGkAagBrAGwAbQBuAG8AcABxAHIAcwB0AHUAdgB3AHgAeQB6ADAAMQAyADMANAA1AA=="); ;

            AllowedAudiences = new[] { audience };
            IssuerSecurityTokenProviders = new[]
            {
                new SymmetricKeyIssuerSecurityTokenProvider(issuer, key)
            };
        }
    }
}