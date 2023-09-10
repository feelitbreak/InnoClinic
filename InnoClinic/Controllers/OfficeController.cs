using AutoMapper;
using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Enums;
using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Exceptions;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [Authorize(Roles = nameof(Role.Receptionist))]
    public class OfficeController : BaseController
    {
        private readonly ILogger<OfficeController> _logger;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<OfficeDto> _validatorOffice;

        public OfficeController(ILogger<OfficeController> logger, IMapper mapper, IUnitOfWork unitOfWork, IValidator<OfficeDto> validatorOffice) : base(logger)
        {
            _logger = logger;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validatorOffice = validatorOffice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
        {
            var offices = await _unitOfWork.Offices.GetAllAsync(cancellationToken);

            if (offices.Any())
            {
                return Ok(new { offices });
            }

            return NoContent();
        }

        [HttpGet]
        [Route("{officeId:int}")]
        public async Task<IActionResult> GetAsync([FromRoute] int officeId, CancellationToken cancellationToken)
        {
            var office = await _unitOfWork.Offices.GetAsync(officeId, cancellationToken);

            if (office is not null)
            {
                return Ok(new { office });
            }

            _logger.LogError("The office with the identifier {officeId} was not found.", officeId);
            throw new NotFoundException("The office was not found.");
        }

        [HttpPut]
        [Route("{officeId:int}")]
        public async Task<IActionResult> UpdateOfficeAsync([FromRoute] int officeId, [FromBody] OfficeDto officeDtoRequest,
            CancellationToken cancellationToken)
        {
            var userId = GetUserId();

            var office = await _unitOfWork.Offices.GetOfficeAsync(officeId, userId, cancellationToken);
            if (office is null)
            {
                _logger.LogError(
                    "The office with the identifier {officeId}, tied to the user with the identifier {userId}, was not found.",
                    officeId, userId);
                throw new NotFoundException("The office was not found.");
            }

            OfficeDto.ToDomain(officeDtoRequest, office);

            _unitOfWork.Offices.Update(office);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("The office with the identifier {officeId} has been updated.", officeId);

            return Ok(new { office });
        }

        [HttpPatch]
        [Route("{officeId:int}/status")]
        public async Task<IActionResult> PatchOfficeStatusAsync([FromRoute] int officeId, CancellationToken cancellationToken)
        {
            var userId = GetUserId();

            var office = await _unitOfWork.Offices.GetOfficeAsync(officeId, userId, cancellationToken);
            if (office is null)
            {
                _logger.LogError(
                    "The office with the identifier {officeId}, tied to the user with the identifier {userId}, was not found.",
                    officeId, userId);
                throw new NotFoundException("The office was not found.");
            }

            office.IsActive = !office.IsActive;
            office.Receptionists.ForEach(r => r.IsActive = office.IsActive);
            office.Doctors.ForEach(d => d.IsActive = office.IsActive);

            _unitOfWork.Offices.Update(office);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "The activity status of the office with the identifier {officeId} has been changed to {IsActive}.",
                officeId, office.IsActive);

            return Ok(new { office });
        }

        [HttpPost("creation")]
        public async Task<IActionResult> AddOfficeAsync([FromBody] OfficeDto officeDtoRequest, CancellationToken cancellationToken)
        {
            var userId = GetUserId();

            var receptionist = await _unitOfWork.Receptionists.GetReceptionistAsync(userId, cancellationToken);
            if (receptionist is null)
            {
                _logger.LogError("The receptionist with the user identifier {userId} was not found.", userId);
                throw new NotFoundException("The user was not found.");
            }

            if (receptionist.OfficeId is not null)
            {
                _logger.LogError(
                    "The receptionist with the identifier {receptionistId} is already tied to the office with the identifier {officeId}.",
                    receptionist.Id, receptionist.OfficeId);
                throw new BadRequestException("You are already tied to an office.");
            }

            var validationResult = await _validatorOffice.ValidateAsync(officeDtoRequest, cancellationToken);

            if (!validationResult.IsValid)
            {
                _logger.LogError(
                    "The input for the office creation was invalid. Validation errors: {validationErrors}",
                    validationResult.Errors);
                return BadRequest(validationResult.Errors);
            }

            var office = _mapper.Map<Office>(officeDtoRequest);

            office.Receptionists.Add(receptionist);

            await _unitOfWork.Offices.AddAsync(office, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "A new office with the identifier {officeId} has been added to the database.",
                office.Id);

            return Ok(new { office });
        }
    }
}
