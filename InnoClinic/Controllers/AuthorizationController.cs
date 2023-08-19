using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
using InnoClinic.Services.Abstractions;
using AutoMapper;
using InnoClinic.Domain.Options;
using Microsoft.Extensions.Options;
using InnoClinic.Domain.DTOs;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly JwtOptions _jwtOptions;

        public AuthorizationController(IMapper mapper, IUnitOfWork unitOfWork, ITokenService tokenService, IOptions<JwtOptions> jwtOptions)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost("signin", Name = "Sign In")]
        public async Task<IActionResult> PostAsync([FromBody] UserSignInDTO userSignIn)
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

                var role = "User";
                var token = _tokenService.GenerateToken(_jwtOptions, user, role);

                return Ok(new { token } );
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPost("signup", Name = "SignUp")]
        public async Task<IActionResult> PostAsync([FromBody] UserSignUpDTO userSignUp)
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

                var role = "User";
                var token = _tokenService.GenerateToken(_jwtOptions, user, role);

                return Ok(new { token });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}