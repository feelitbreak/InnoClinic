﻿using AutoMapper;
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

        [HttpPost]
        [Authorize(Roles = nameof(Role.Patient))]
        public async Task<IActionResult> CreatePatientProfileAsync([FromBody] PatientProfileDto patientProfileDtoRequest, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorPatientProfile.ValidateAsync(patientProfileDtoRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError(
                    "The input for the patient profile creation was invalid. Validation errors: {validationErrors}",
                    validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var userId = GetUserId();

            var user = await _unitOfWork.Users.GetPatientUserAsync(userId, cancellationToken);
            if (user is null)
            {
                _logger.LogError("The user with the identifier {userId} was not found.", userId);
                throw new ClinicException("The user was not found.", ExceptionCode.UserNotFoundException);
            }

            user.IsEmailVerified = true;
            _unitOfWork.Users.Update(user);

            if (user.Patient is not null)
            {
                _logger.LogError(
                    "The user with the identifier {userId} already has a profile with the identifier {patientId}.",
                    userId, user.Patient.Id);
                throw new ClinicException("You already have a profile.", ExceptionCode.UserIsLinkedToProfileException);
            }

            var matchingProfile = await _unitOfWork.Patients.FindMatchingAsync(patientProfileDtoRequest, cancellationToken);
            if (matchingProfile is not null)
            {
                _logger.LogInformation(
                    "A matching profile with the identifier {matchingProfileId} has been found for the user with the identifier {userId}.",
                    matchingProfile.Id, userId);

                return Ok(new
                {
                    message = "A similar profile has been found, you might have already visited one of our clinics?",
                    patientId = matchingProfile.Id
                });
            }

            var patient = _mapper.Map<Patient>(patientProfileDtoRequest);
            patient.UserId = userId;
            patient.IsLinkedToAccount = true;

            await _unitOfWork.Patients.AddAsync(patient, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "A new patient profile with the identifier {patientId} has been added to the database.",
                patient.Id);

            return Ok(patientProfileDtoRequest);
        }

        [HttpPost("new")]
        [Authorize(Roles = nameof(Role.Patient))]
        public async Task<IActionResult> CreateNewProfileAsync([FromBody] PatientProfileDto patientProfileDtoRequest, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorPatientProfile.ValidateAsync(patientProfileDtoRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError(
                    "The input for the patient profile creation was invalid. Validation errors: {validationErrors}",
                    validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var userId = GetUserId();

            var user = await _unitOfWork.Users.GetPatientUserAsync(userId, cancellationToken);
            if (user is null)
            {
                _logger.LogError("The user with the identifier {userId} was not found.", userId);
                throw new ClinicException("The user was not found.", ExceptionCode.UserNotFoundException);
            }

            user.IsEmailVerified = true;
            _unitOfWork.Users.Update(user);

            if (user.Patient is not null)
            {
                _logger.LogError(
                    "The user with the identifier {userId} already has a profile with the identifier {patientId}.",
                    userId, user.Patient.Id);
                throw new ClinicException("You already have a profile.", ExceptionCode.UserIsLinkedToProfileException);
            }

            var patient = _mapper.Map<Patient>(patientProfileDtoRequest);
            patient.UserId = userId;
            patient.IsLinkedToAccount = true;

            await _unitOfWork.Patients.AddAsync(patient, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "A new patient profile with the identifier {patientId} has been added to the database.",
                patient.Id);

            return Ok(patientProfileDtoRequest);
        }

        [HttpPatch("confirm")]
        [Authorize(Roles = nameof(Role.Patient))]
        public async Task<IActionResult> ConfirmProfileAsync([FromQuery] int patientId, CancellationToken cancellationToken)
        {
            var userId = GetUserId();

            var user = await _unitOfWork.Users.GetPatientUserAsync(userId, cancellationToken);
            if (user is null)
            {
                _logger.LogError("The user with the identifier {userId} was not found.", userId);
                throw new ClinicException("The user was not found.", ExceptionCode.UserNotFoundException);
            }

            user.IsEmailVerified = true;
            _unitOfWork.Users.Update(user);

            if (user.Patient is not null)
            {
                _logger.LogError(
                    "The user with the identifier {userId} already has a profile with the identifier {existingPatientId}.",
                    userId, user.Patient.Id);
                throw new ClinicException("You already have a profile.", ExceptionCode.UserIsLinkedToProfileException);
            }

            var patient = await _unitOfWork.Patients.GetAsync(patientId, cancellationToken);
            if (patient is null)
            {
                _logger.LogError("The patient with the identifier {patientId} was not found.", patientId);
                throw new ClinicException("The patient profile was not found.", ExceptionCode.PatientNotFoundException);
            }

            patient.UserId = userId;
            patient.IsLinkedToAccount = true;

            _unitOfWork.Patients.Update(patient);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "The patient profile with the identifier {patientId} has been linked to the user with the identifier {userId}.",
                patient.Id, userId);

            return Ok(_mapper.Map<PatientProfileDto>(patient));
        }
    }
}