using JsonWebTokenExample.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace JsonWebTokenExample.Logic
{
    public class JsonWebTokenLogic : IJsonWebTokenLogic
    {
        public JsonWebTokenLogic() 
        {

        }

        public string GenerateToken(string userName, string password)
        {
            //Security Key that makes our key unique
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey.SECRET_KEY));
           
            //The hashing function we use to prevent collisions and make our key uniform in size
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            
            int tokenValidDurationMinutes = 3;

            var identity = new ClaimsIdentity("JWT Token");
            identity.AddClaim(new Claim("user", userName));
            identity.AddClaim(new Claim("company", "Bob's Cooling"));

            var token = new JwtSecurityToken(
                issuer: "Bob's Cooling",
                audience: "localhost",
                claims: identity.Claims,
                expires : DateTime.Now.AddMinutes(tokenValidDurationMinutes),
                signingCredentials: credentials);

            var completedToken = new JwtSecurityTokenHandler().WriteToken(token);
            return completedToken;
        }

        public bool ValidateToken(string token)
        {
            TokenValidationParameters parameters = new TokenValidationParameters()
            {
                ValidateLifetime = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey.SECRET_KEY)),
                ValidAudience = "localhost",
                ValidIssuer = "Bob's Cooling"
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            IPrincipal principal = tokenHandler.ValidateToken(token, parameters, out validatedToken);

            return true;
        }
    }
}
