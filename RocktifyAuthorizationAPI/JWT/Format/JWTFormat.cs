using BLL;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using Thinktecture.IdentityModel.Tokens;

namespace RocktifyAuthorizationAPI.Providers
{
    public class JWTFormat : ISecureDataFormat<AuthenticationTicket>
    {
        private const string AudienceKey = "Audience";
        private const string Issuer = "http://localhost:15891";
        public string Protect(AuthenticationTicket data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("data");
            }
            else
            {
                string audienceId = data.Properties.Dictionary.ContainsKey(AudienceKey) ? data.Properties.Dictionary[AudienceKey] : null;

                if (string.IsNullOrWhiteSpace(audienceId))
                {
                    throw new InvalidOperationException("AuthenticationTicket.Properties does not include audience");
                }
                else
                {
                    Client client = ClientService.FindClientId(audienceId);

                    //Decode Client Secret
                    var keyByteArray = TextEncodings.Base64Url.Decode(client.ClientSecret);
                    //Create Symmetric Security Key
                    var securityKey = new SymmetricSecurityKey(keyByteArray);
                    var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
                    var issued = data.Properties.IssuedUtc;
                    var expires = data.Properties.ExpiresUtc;
                    
                    //Create Token
                    var token = new JwtSecurityToken(Issuer, audienceId, 
                            data.Identity.Claims, issued.Value.UtcDateTime, 
                            expires.Value.UtcDateTime, signingCredentials);

                    //Create JWT Token
                    var handler = new JwtSecurityTokenHandler();
                    //Serializing JWT Token to JWT format
                    var jwt = handler.WriteToken(token);

                    return jwt;
                }
            }
        }

        public AuthenticationTicket Unprotect(string protectedText)
        {
            throw new NotImplementedException();
        }
    }
}