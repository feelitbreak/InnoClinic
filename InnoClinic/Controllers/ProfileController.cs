using AutoMapper;
using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Enums;
using InnoClinic.Domain.Exceptions;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProfileController : BaseController
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PatientProfileDto> _validatorPatientProfile;

        public ProfileController(ILogger<ProfileController> logger, IMapper mapper, IUnitOfWork unitOfWork, IValidator<PatientProfileDto> validatorPatientProfile) : base(logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validatorPatientProfile = validatorPatientProfile;
        }

        [HttpPost("creation")]
        [Authorize(Roles = nameof(Role.Patient))]
        public async Task<IActionResult> CreatePatientProfileAsync([FromBody] PatientProfileDto patientProfile, CancellationToken cancellationToken)
        {
            var userId = GetUserId();

            var user = await _unitOfWork.Users.GetAsync(userId, cancellationToken);
            if (user is null)
            {
                _logger.LogError("The user with the identifier {userId} was not found.", userId);
                throw new NotFoundException("The user was not found.");
            }

            var validationResult = await _validatorPatientProfile.ValidateAsync(patientProfile, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError(
                    "The input for the patient profile creation was invalid. Validation errors: {validationErrors}",
                    validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var matchingProfile = await _unitOfWork.Users.FindPatientProfileAsync(patientProfile, cancellationToken);
            if (matchingProfile is null)
            {
                _mapper.Map(patientProfile, user);
                _unitOfWork.Users.Update(user);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Ok(new { patientProfile });
            }

            return NotFound();
        }
    }
}
