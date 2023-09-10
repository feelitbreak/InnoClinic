using AutoMapper;
using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Entities;
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
    public class PatientController : BaseController
    {
        private readonly ILogger<PatientController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<PatientProfileDto> _validatorPatientProfile;

        public PatientController(ILogger<PatientController> logger, IMapper mapper, IUnitOfWork unitOfWork, IValidator<PatientProfileDto> validatorPatientProfile) : base(logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validatorPatientProfile = validatorPatientProfile;
        }

        [HttpPost("creation")]
        [Authorize(Roles = nameof(Role.Patient))]
        public async Task<IActionResult> CreatePatientProfileAsync([FromBody] PatientProfileDto patientProfileDtoRequest, CancellationToken cancellationToken)
        {
            var userId = GetUserId();

            var user = await _unitOfWork.Users.GetPatientUserAsync(userId, cancellationToken);
            if (user is null)
            {
                _logger.LogError("The user with the identifier {userId} was not found.", userId);
                throw new NotFoundException("The user was not found.");
            }

            if (user.Patient is not null)
            {
                _logger.LogError(
                    "The user with the identifier {userId} already has a profile with the identifier {patientId}.",
                    userId, user.Patient.Id);
                throw new BadRequestException("You already have a profile.");
            }

            var validationResult = await _validatorPatientProfile.ValidateAsync(patientProfileDtoRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError(
                    "The input for the patient profile creation was invalid. Validation errors: {validationErrors}",
                    validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var matchingProfile = await _unitOfWork.Patients.FindMatchingAsync(patientProfileDtoRequest, cancellationToken);
            if (matchingProfile is null)
            {
                var patient = _mapper.Map<Patient>(patientProfileDtoRequest);
                patient.UserId = userId;
                patient.IsLinkedToAccount = true;

                await _unitOfWork.Patients.AddAsync(patient, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return Ok(patientProfileDtoRequest);
            }

            return RedirectToAction("ConfirmProfile", new { userId, patientProfileDtoRequest, matchingProfile });
        }

        [HttpPost("creation/popup")]
        [Authorize(Roles = nameof(Role.Patient))]
        public async Task<IActionResult> ConfirmProfileAsync([FromQuery] bool isMyProfile, [FromRoute] int userId, [FromRoute] PatientProfileDto patientProfileDtoRequest, [FromRoute] Patient matchingProfile, CancellationToken cancellationToken)
        {
            if (isMyProfile)
            {
                matchingProfile.UserId = userId;
                matchingProfile.IsLinkedToAccount = true;

                _unitOfWork.Patients.Update(matchingProfile);
            }
            else
            {
                var patient = _mapper.Map<Patient>(patientProfileDtoRequest);
                patient.UserId = userId;
                patient.IsLinkedToAccount = true;

                await _unitOfWork.Patients.AddAsync(patient, cancellationToken);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return Ok(patientProfileDtoRequest);
        }
    }
}
