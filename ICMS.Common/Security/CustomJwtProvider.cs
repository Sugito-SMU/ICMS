
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;

namespace ICMS.Common.Security
{
    public class CustomJwtProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }


        public override Task GrantClientCredentials(OAuthGrantClientCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var ticket = OAuthUtility.GetAuthenticationTicket();
            context.Validated(ticket);
            return Task.FromResult<object>(null);
        }

    }
}
