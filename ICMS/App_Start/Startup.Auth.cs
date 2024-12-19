using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Configuration;
using Microsoft.Owin.Security;

using Microsoft.Identity.Web.OWIN;
using Microsoft.Identity.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.Identity.Web.TokenCacheProviders.InMemory;

namespace ICMS
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
			//OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
			//{
			//    AllowInsecureHttp = true,
			//    TokenEndpointPath = new PathString("/oauth/token"),
			//    AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["TokenExpiryInMins"])),
			//    Provider = new CustomJwtProvider(),
			//    AccessTokenFormat = new CustomJwtFormat()
			//};

			//app.UseOAuthAuthorizationServer(OAuthServerOptions);

			var clientId = ConfigurationManager.AppSettings["AzureAd:ClientId"];
			var tenantId = ConfigurationManager.AppSettings["AzureAd:TenantId"];
			var clientSecret = ConfigurationManager.AppSettings["AzureAd:ClientSecret"];
			var instance = ConfigurationManager.AppSettings["AzureAd:Instance"];
			var redirectUri = ConfigurationManager.AppSettings["AzureAd:RedirectUri"];

			app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);
			app.UseCookieAuthentication(new CookieAuthenticationOptions());

			// Get an TokenAcquirerFactory specialized for OWIN
			OwinTokenAcquirerFactory owinTokenAcquirerFactory = TokenAcquirerFactory.GetDefaultInstance<OwinTokenAcquirerFactory>();
			owinTokenAcquirerFactory.Configuration.GetSection("AzureAD:ClientId").Value = clientId;
			owinTokenAcquirerFactory.Configuration.GetSection("AzureAD:TenantId").Value = tenantId;
			owinTokenAcquirerFactory.Configuration.GetSection("AzureAD:ClientSecret").Value = clientSecret;
			owinTokenAcquirerFactory.Configuration.GetSection("AzureAD:RedirectUri").Value = redirectUri;
			owinTokenAcquirerFactory.Configuration.GetSection("AzureAD:Instance").Value = instance;

			// Configure the web app.
			app.AddMicrosoftIdentityWebApp(owinTokenAcquirerFactory,
										   updateOptions: options => { });
			// Add the services you need.
			owinTokenAcquirerFactory.Services
				 .Configure<ConfidentialClientApplicationOptions>(options =>
				 { options.RedirectUri = redirectUri; })
				//.AddMicrosoftGraph()
				.AddInMemoryTokenCaches();

			owinTokenAcquirerFactory.Build();

		}
    }
}