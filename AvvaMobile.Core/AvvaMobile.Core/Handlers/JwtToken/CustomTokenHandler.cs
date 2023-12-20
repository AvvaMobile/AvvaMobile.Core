using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AvvaMobile.Core.Handlers.JwtToken
{
    public class CustomTokenHandler
    {
        private readonly IConfiguration _configuration;
        public CustomTokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Token CreateAccessToken(List<Claim> claims)
        {
            Token token = new();

            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["Token:SecurityKey"]));

            SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            if (string.IsNullOrEmpty(_configuration["Token:DaysToExpire"]))
            {
                token.Expiration = DateTime.UtcNow.AddDays(30);
            }
            else
            {
                token.Expiration = DateTime.UtcNow.AddDays(int.Parse(_configuration["Token:DaysToExpire"]));
            }
            

            JwtSecurityToken securityToken = new(
                issuer: _configuration["Token:Issuer"],
                audience: _configuration["Token:Audience"],
                expires: token.Expiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: signingCredentials,
                claims: claims
                );

            JwtSecurityTokenHandler tokenHandler = new();

            token.AccessToken = tokenHandler.WriteToken(securityToken);

            token.RefreshToken = Guid.NewGuid().ToString();

            return token;
        }
    }
}