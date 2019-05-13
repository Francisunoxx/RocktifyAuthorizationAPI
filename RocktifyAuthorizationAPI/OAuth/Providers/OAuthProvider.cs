using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace RocktifyAuthorizationAPI
{
    public class OAuthProvider : OAuthAuthorizationServerProvider
    {
        /*public override Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                context.OwinContext.Authentication.SignIn(identity);
                context.RequestCompleted();
            });
        }*/

        /*public override Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        {
            return base.ValidateAuthorizeRequest(context);
        }*/

        /*public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                if (context.ClientId == "123456" && context.RedirectUri.Contains("localhost"))
                {
                    context.Validated();
                }
                else
                {
                    context.Rejected();
                }
            });
        }

        public override Task MatchEndpoint(OAuthMatchEndpointContext context)
        {
            return base.MatchEndpoint(context);
        }*/

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    string clientId = string.Empty;
                    string clientSecret = string.Empty;
                    string symmetricKeyAsBase64 = string.Empty;

                    if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
                    {
                        context.TryGetFormCredentials(out clientId, out clientSecret);
                    }

                    if (context.ClientId == null)
                    {
                        context.SetError("invalid_clientId", "client_Id is not set");
                        context.Rejected();
                    }
                    else if (context.ClientId == "QjQ3NjJCMjFFNzVGOTI5Q0E4RTRFQTg3RjVDQUE=")
                    {
                        context.Validated();
                    }

                }
                catch
                {
                    context.SetError("Server error");
                    context.Rejected();
                }
            });

        }

    public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
        return Task.Factory.StartNew(() =>
        {
            //Set ClaimsIdentity as JWT
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            
            if (context.UserName == "admin" && context.Password == "admin")
            {
                //Creating creating claims
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                
                //Store Audience information 
                var props = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "Audience", (context.ClientId == null) ? string.Empty : context.ClientId
                    }
                });

                var ticket = new AuthenticationTicket(identity, props);
                //Pass ticket to context
                context.Validated(ticket);
            }
        });
    }
}
}