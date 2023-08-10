using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Repositories;
using InnoClinic.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRep;

        public AuthorizationController(IConfiguration config, IUserRepository userRep)
        {
            _config = config;
            _userRep = userRep;
        }

        [HttpPost(Name = "SignUp")]
        public IActionResult Post([FromBody] User userSignUp)
        {
            if (userSignUp is null)
            {
                return BadRequest();
            }

            if (!userSignUp.Password.Equals(userSignUp.ReenteredPassword))
            {
                return BadRequest("The passwords you’ve entered don’t coincide");
            }

            _userRep.AddUser(userSignUp);
            var token = GenerateToken(userSignUp);

            return Ok(token);
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}