using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebDemo.Authentication
{
    public class Authen 
    {
        private readonly JwtSecurityTokenHandler JwtTokenHandler;
        internal static readonly SymmetricSecurityKey SecurityKey = new SymmetricSecurityKey(Guid.NewGuid().ToByteArray());
        public Authen()
        {
            JwtTokenHandler = new JwtSecurityTokenHandler();
        }
        internal string GenerateJwtToken(string paraName)
        {
            if(string.IsNullOrEmpty(paraName))
            {
                throw new InvalidOperationException("Name is not specified");
            }
            var claims = new[] { new Claim(ClaimTypes.Name, paraName)};
            var credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken("HaServer", "HaClients", claims, expires: DateTime.Now.AddSeconds(60), signingCredentials: credentials);
            return JwtTokenHandler.WriteToken(token);


        }
    }
}
