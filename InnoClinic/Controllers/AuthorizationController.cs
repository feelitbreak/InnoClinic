using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Repositories;
using InnoClinic.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InnoClinic.Services.Abstractions;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRep;
        private readonly ITokenService _tokenService;

        public AuthorizationController(IConfiguration config, IUserRepository userRep, ITokenService tokenService)
        {
            _config = config;
            _userRep = userRep;
            _tokenService = tokenService;
        }

        [HttpPost("login", Name = "Sign In")]
        public IActionResult Post(string email, string password)
        {
            if (email is null || password is null)
            {
                return BadRequest();
            }

            var user = _userRep.GetUserByEmail(email);

            if (user is null || !user.Password.Equals(password))
            {
                return BadRequest("Either an email or a password is incorrect");
            }

            var token = _tokenService.GenerateToken(_config, user);

            return Ok("You've signed in successfully. Token: " + token);
        }

        [HttpPost("signup", Name = "SignUp")]
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

            if (_userRep.CheckUser(userSignUp.Email))
            {
                return BadRequest("User with this email already exists");
            }

            _userRep.AddUser(userSignUp);
            var token = _tokenService.GenerateToken(_config, userSignUp);

            return Ok("You've signed up successfully. Token: " + token);
        }
    }
}