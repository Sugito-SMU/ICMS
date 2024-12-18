using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace ICMS.Common.Security
{
    public class OAuthUtility
    {
        public static string GetLoginId(string accessToken = "")
        {
            DateTime startTime = DateTime.Now;
            Logger.LogDebug("GetLoginId - Access token from method parameter: " + accessToken);

            string userId = null;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DebugUsername"]))
            {
                Logger.LogDebug("Taking DebugUsername: " + ConfigurationManager.AppSettings["DebugUsername"]);
                userId = ConfigurationManager.AppSettings["DebugUsername"].Trim();
            }
            else
            {
                string tokenValue = string.IsNullOrEmpty(accessToken) ? GetAccessToken() : accessToken;
                Logger.LogDebug("GetLoginId - tokenValue: " + tokenValue);

                if (tokenValue != null)
                {
                    string decryptedText = Encryption.Decrypt(tokenValue, ConfigurationManager.AppSettings["JwtCertThumbprint"]);
                    string[] fields = decryptedText.Split('|');
                    DateTime expiryTime = DateTime.MinValue;
                    DateTime.TryParse(fields[1], out expiryTime);
                    if (expiryTime < DateTime.Now)
                    {
                        Logger.LogDebug("Token has expired: " + expiryTime.ToString());
                        throw new ArgumentException("Token has expired");
                    }
                    if (IsValidUserID(fields[0]))
                    {
                        userId = fields[0];
                    }
                }
                else
                {
                    var ssoConfig = ConfigurationManager.GetSection("system.identityModel");
                    if (ssoConfig != null)
                    {
                        Logger.LogDebug("Taking user from ClaimsPrincipal");
                        System.Security.Claims.ClaimsPrincipal claimsPrincipal = (System.Security.Claims.ClaimsPrincipal)Thread.CurrentPrincipal;
                        foreach (Claim claim in claimsPrincipal.Claims)
                        {
                            if (claim.Type.ToString().Trim().ToLower() == ConfigurationManager.AppSettings["UsernameClaimID"].Trim().ToLower())
                            {
                                if (IsValidUserID(claim.Value))
                                {
                                    userId = claim.Value;
                                    Logger.LogDebug("Username from claim: " + userId);
                                }
                            }
                        }
                    }
                    else
                    {
                        string id = HttpContext.Current.Request.ServerVariables["AUTH_USER"];
                        string[] fields = id.Split('\\');
                        if (fields.Length > 1)
                        {

                            if (IsValidUserID(fields[1].ToLower().Trim()))
                            {
                                userId = fields[1].ToLower().Trim();
                            }
                        }
                    }
                }
            }

            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            Logger.LogDebug("GetLoginId - Elapsed Time (ms)\t" + elapsed.TotalMilliseconds);

            return userId;
        }

        public static AuthenticationTicket GetAuthenticationTicket()
        {
            DateTime startTime = DateTime.Now;

            var identity = new ClaimsIdentity("JWT");
            string userId = string.Empty;

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["DebugUsername"]))
            {
                Logger.LogDebug("GetAuthenticationTicket - Taking DebugUsername: " + ConfigurationManager.AppSettings["DebugUsername"]);
                userId = ConfigurationManager.AppSettings["DebugUsername"];
            }
            else
            {
                Logger.LogDebug("GetAuthenticationTicket - Taking user from ClaimsPrincipal");
                ClaimsPrincipal claimsPrincipal = (ClaimsPrincipal)Thread.CurrentPrincipal;
                foreach (Claim claim in claimsPrincipal.Claims)
                {
                    if (claim.Type.ToString().Trim().ToLower() == ConfigurationManager.AppSettings["UsernameClaimID"].Trim().ToLower())
                    {
                        userId = Methods.GetUsernameWithoutDomain(claim.Value);
                        Logger.LogDebug("GetAuthenticationTicket - Username from claim: " + userId);
                    }
                }
            }

            string claimType = "app_token";
            string expiryTime = DateTime.Now.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["TokenExpiryInMins"])).ToString("dd-MMM-yyyy HH:mm:ss.fff");
            string randomString = CryptoRandom.GetRandomString();
            string stringToEncrypt = string.Format("{0}|{1}|{2}", userId, expiryTime, randomString);
            string encryptedToken = Encryption.Encrypt(stringToEncrypt, ConfigurationManager.AppSettings["JwtCertThumbprint"]);
            identity.AddClaim(new Claim(claimType, encryptedToken));

            Logger.LogDebug("GetAuthenticationTicket - Token generated: " + encryptedToken);

            var props = new AuthenticationProperties(new Dictionary<string, string>
            {
                {
                    "audience", ConfigurationManager.AppSettings["AudienceID"]
                }
            });

            var ticket = new AuthenticationTicket(identity, props);

            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            Logger.LogDebug("GetAuthenticationTicket - Elapsed Time (ms)\t" + elapsed.TotalMilliseconds);

            return ticket;
        }

        public static string ProtectAuthenticationTicket(AuthenticationTicket ticket)
        {
            DateTime startTime = DateTime.Now;

            X509Certificate2 signingCert = Encryption.GetCertificate(ConfigurationManager.AppSettings["JwtCertThumbprint"]);
            SigningCredentials signingCredentials = new X509SigningCredentials(signingCert);

            DateTime utcNow = DateTime.UtcNow;
            DateTime utcExpires = utcNow.AddMinutes(Convert.ToDouble(ConfigurationManager.AppSettings["TokenExpiryInMins"]));
            string issuer = ConfigurationManager.AppSettings["Issuer"];
            string audienceID = ConfigurationManager.AppSettings["AudienceID"];

            JwtSecurityToken token = new JwtSecurityToken(issuer, audienceID, ticket.Identity.Claims, utcNow, utcExpires, signingCredentials);

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string jwt = handler.WriteToken(token);

            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            Logger.LogDebug("ProtectAuthenticationTicket - Elapsed Time (ms)\t" + elapsed.TotalMilliseconds);

            return jwt;
        }

        public static string GetAccessToken()
        {
            DateTime startTime = DateTime.Now;

            string accessToken = null;
            if ((HttpContext.Current != null) && (HttpContext.Current.User != null))
            {
                string raw = HttpContext.Current.Request.Headers["Authorization"];
                Logger.LogDebug(string.Format("GetAccessToken - Authorization token: {0}", raw));
                if (!string.IsNullOrEmpty(raw) && raw.ToLower().Trim().StartsWith("bearer"))
                {
                    string bearer = raw.Replace("Bearer ", "").Trim();
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwt = handler.ReadJwtToken(bearer);
                    accessToken = jwt.Claims.Where(x => x.Type == "app_token").First().Value;
                }

            }

            DateTime endTime = DateTime.Now;
            TimeSpan elapsed = endTime - startTime;
            Logger.LogDebug("GetAccessToken - Elapsed Time (ms)\t" + elapsed.TotalMilliseconds);

            return accessToken;
        }

        public static bool IsValidUserID(string userID)
        {
            Regex _ReUserID = new Regex("^[a-zA-Z0-9\x2E\x5F]+$");
            if (_ReUserID.IsMatch(userID))
                return true;
            else
                throw new ArgumentException("Invalid user ID format");
        }
    }
}
