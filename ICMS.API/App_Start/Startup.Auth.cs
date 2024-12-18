using ICMS.Common.Security;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Jwt;
using Owin;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace ICMS.API
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            var issuer = ConfigurationManager.AppSettings["Issuer"];
            var audience = ConfigurationManager.AppSettings["AudienceID"];

            X509Certificate2 signingCert = Encryption.GetCertificate(ConfigurationManager.AppSettings["JwtCertThumbprint"]);
            Microsoft.IdentityModel.Tokens.X509SecurityKey signingKey = new Microsoft.IdentityModel.Tokens.X509SecurityKey(signingCert);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ClockSkew = TimeSpan.Zero,
                        ValidAudiences = new string[] { audience },
                        ValidIssuers = new string[] { issuer },
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = signingKey,
                    }
                });
        }

    }
}