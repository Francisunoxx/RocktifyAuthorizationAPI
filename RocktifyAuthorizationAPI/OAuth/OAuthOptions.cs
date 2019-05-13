using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using RocktifyAuthorizationAPI.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RocktifyAuthorizationAPI
{
    public class OAuthOptions : OAuthAuthorizationServerOptions
    {
        public OAuthOptions()
        {
            AllowInsecureHttp = true;
            TokenEndpointPath = new PathString("/Token");
            AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30);
            AccessTokenFormat = new JWTFormat();
            Provider = new OAuthProvider();
        }
    }
}