using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InnoClinic.Services.Abstractions;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;

        public AuthorizationController(IConfiguration config, IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService)
        {
            _config = config;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
        }

        [HttpPost("signin", Name = "Sign In")]
        public async Task<IActionResult> PostAsync([FromBody] SignInUserModel userSignIn)
        {
            if (ModelState.IsValid)
            {
                if (userSignIn is null)
                {
                    return BadRequest();
                }

                var user = await _unitOfWork.Users.GetByEmailAsync(userSignIn.Email);

                if (user is null || !user.Password.Equals(userSignIn.Password))
                {
                    return BadRequest("Either an email or a password is incorrect");
                }

                string role = "User";
                var token = _tokenService.GenerateToken(_config, user, role);

                return Ok("You've signed in successfully. Token: " + token);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("signup", Name = "SignUp")]
        public async Task<IActionResult> PostAsync([FromBody] SignUpUserModel userSignUp)
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

                var userWithSameEmail = await _unitOfWork.Users.GetByEmailAsync(userSignUp.Email);
                if (userWithSameEmail is not null)
                {
                    return BadRequest("User with this email already exists");
                }

                var user = _mapper.Map<User>(userSignUp);

                await _unitOfWork.Users.AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                string role = "User";
                var token = _tokenService.GenerateToken(_config, user, role);

                return Ok("You've signed up successfully. Token: " + token);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}