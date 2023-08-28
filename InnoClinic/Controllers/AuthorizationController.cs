using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
using InnoClinic.Services.Abstractions;
using AutoMapper;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Models;
using InnoClinic.Domain.Enums;
using FluentValidation;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("authorization")]
    public class AuthorizationController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHashingService _hashingService;
        private readonly ITokenService _tokenService;
        private readonly IValidator<UserSignInDto> _validatorUserSignIn;
        private readonly IValidator<UserSignUpDto> _validatorUserSignUp;

        public AuthorizationController(IMapper mapper,
            IUnitOfWork unitOfWork,
            IPasswordHashingService hashingService,
            ITokenService tokenService,
            IValidator<UserSignInDto> validatorUserSignIn,
            IValidator<UserSignUpDto> validatorUserSignUp)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _hashingService = hashingService;
            _tokenService = tokenService;
            _validatorUserSignIn = validatorUserSignIn;
            _validatorUserSignUp = validatorUserSignUp;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> PostAsync([FromBody] UserSignInDto userSignIn, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUserSignIn.ValidateAsync(userSignIn, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(userSignIn.Email, cancellationToken);

            if (user is null)
            {
                return BadRequest(new { errorMessage = "The email is incorrect" });
            }

            var passwordModel = new PasswordModel
            {
                Key = user.HashedPassword,
                Salt = user.Salt
            };

            if (!_hashingService.IsValidPassword(userSignIn.Password, passwordModel))
            {
                return BadRequest(new { errorMessage = "The password is incorrect" });
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> PostAsync([FromBody] UserSignUpDto userSignUp, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUserSignUp.ValidateAsync(userSignUp, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = _mapper.Map<User>(userSignUp);
            var passwordModel = _hashingService.EncodePassword(userSignUp.Password);
            user.HashedPassword = passwordModel.Key;
            user.Salt = passwordModel.Salt;
            user.Role = Role.User;

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}