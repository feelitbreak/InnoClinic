using Microsoft.AspNetCore.Mvc;
using InnoClinic.Domain.Interfaces;
using InnoClinic.Domain.Entities;
using InnoClinic.Services.Abstractions;
using AutoMapper;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Models;
using InnoClinic.Domain.Enums;
using FluentValidation;
using InnoClinic.Domain.Exceptions;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthorizationController : BaseController
    {
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEncryptionService _encryptionService;
        private readonly ITokenService _tokenService;
        private readonly IValidator<UserSignInDto> _validatorUserSignIn;
        private readonly IValidator<UserSignUpDto> _validatorUserSignUp;

        public AuthorizationController(ILogger<AuthorizationController> logger,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            IEncryptionService encryptionService,
            ITokenService tokenService,
            IValidator<UserSignInDto> validatorUserSignIn,
            IValidator<UserSignUpDto> validatorUserSignUp) : base(logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _encryptionService = encryptionService;
            _tokenService = tokenService;
            _validatorUserSignIn = validatorUserSignIn;
            _validatorUserSignUp = validatorUserSignUp;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignInAsync([FromBody] UserSignInDto userSignIn, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUserSignIn.ValidateAsync(userSignIn, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError("The input for sign-in was invalid. Validation errors: {validationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(userSignIn.Email, cancellationToken);
            if (user is null)
            {
                _logger.LogError("The user with the email {userEmail} was not found or is not active.", userSignIn.Email);
                throw new NotFoundException("The user was not found.");
            }

            var passwordModel = new PasswordModel
            {
                Key = user.HashedPassword,
                Salt = user.Salt
            };

            if (!_encryptionService.IsValidPassword(userSignIn.Password, passwordModel))
            {
                _logger.LogError("Entered incorrect password for the user with the email {userEmail}.", userSignIn.Email);
                throw new BadRequestException("The password you've entered is incorrect.");
            }

            var token = _tokenService.GenerateToken(user);

            return Ok(new { token });
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUpAsync([FromBody] UserSignUpDto userSignUp, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUserSignUp.ValidateAsync(userSignUp, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError("The input for sign-in was invalid. Validation errors: {validationErrors}", validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var user = _mapper.Map<User>(userSignUp);
            var passwordModel = _encryptionService.EncodePassword(userSignUp.Password);
            user.HashedPassword = passwordModel.Key;
            user.Salt = passwordModel.Salt;
            user.Role = Role.Patient;

            await _unitOfWork.Users.AddAsync(user, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("A new user with the email {userEmail} has been added to the database.", user.Email);

            return NoContent();
        }
    }
}