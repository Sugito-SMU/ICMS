using ICMS.Common.Security;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Configuration;

namespace ICMS
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["TokenExpiryInMins"])),
                Provider = new CustomJwtProvider(),
                AccessTokenFormat = new CustomJwtFormat()
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);

        }
    }
}