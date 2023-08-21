using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
using InnoClinic.Services.Abstractions;
using AutoMapper;
using InnoClinic.Domain.DTOs;
using FluentValidation;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IValidator<UserSignInDTO> _validatorUserSignIn;
        private readonly IValidator<UserSignUpDTO> _validatorUserSignUp;

        public AuthorizationController(IMapper mapper,
            IUnitOfWork unitOfWork,
            ITokenService tokenService,
            IValidator<UserSignInDTO> validatorUserSignIn,
            IValidator<UserSignUpDTO> validatorUserSignUp)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _validatorUserSignIn = validatorUserSignIn;
            _validatorUserSignUp = validatorUserSignUp;
        }

        [HttpPost("signin", Name = "Sign In")]
        public async Task<IActionResult> PostAsync([FromBody] UserSignInDTO userSignIn)
        {
            var validationResult = await _validatorUserSignIn.ValidateAsync(userSignIn);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(userSignIn.Email);

            var role = "User";
            var token = _tokenService.GenerateToken(user!, role);

            return Ok(new { token });
        }

        [HttpPost("signup", Name = "SignUp")]
        public async Task<IActionResult> PostAsync([FromBody] UserSignUpDTO userSignUp)
        {
            var validationResult = await _validatorUserSignUp.ValidateAsync(userSignUp);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = _mapper.Map<User>(userSignUp);

            await _unitOfWork.Users.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var role = "User";
            var token = _tokenService.GenerateToken(user, role);

            return Ok(new { token });
        }
    }
}