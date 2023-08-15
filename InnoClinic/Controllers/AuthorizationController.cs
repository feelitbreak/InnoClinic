using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Repositories;
using InnoClinic.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InnoClinic.Services.Abstractions;
using AutoMapper;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRep;
        private readonly ITokenService _tokenService;

        public AuthorizationController(IConfiguration config, IMapper mapper, IUserRepository userRep, ITokenService tokenService)
        {
            _config = config;
            _mapper = mapper;
            _userRep = userRep;
            _tokenService = tokenService;
        }

        [HttpPost("signin", Name = "Sign In")]
        public IActionResult Post([FromBody] SignInUserModel userSignIn)
        {
            if (ModelState.IsValid)
            {
                if (userSignIn is null)
                {
                    return BadRequest();
                }

                var user = _userRep.GetUserByEmail(userSignIn.Email);

                if (user is null || !user.Password.Equals(userSignIn.Password))
                {
                    return BadRequest("Either an email or a password is incorrect");
                }

                var token = _tokenService.GenerateToken(_config, user);

                return Ok("You've signed in successfully. Token: " + token);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("signup", Name = "SignUp")]
        public IActionResult Post([FromBody] SignUpUserModel userSignUp)
        {
            if (ModelState.IsValid)
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

                var user = _mapper.Map<User>(userSignUp);

                _userRep.AddUser(user);
                var token = _tokenService.GenerateToken(_config, user);

                return Ok("You've signed up successfully. Token: " + token);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}