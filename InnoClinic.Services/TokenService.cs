using InnoClinic.Domain.Entities;
using InnoClinic.Services.Abstractions;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using InnoClinic.Domain.Options;
using Microsoft.Extensions.Options;

namespace InnoClinic.Services
{
    public class TokenService : ITokenService
    {
        public string GenerateToken(JwtOptions _jwtOptions, User user, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(_jwtOptions.Issuer,
                _jwtOptions.Audience,
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}