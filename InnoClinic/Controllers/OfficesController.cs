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
    [Route("office-management")]
    [Authorize(Roles = nameof(Role.Receptionist))]
    public class OfficesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<OfficeDto> _validatorOffice;

        public OfficesController(IMapper mapper, IUnitOfWork unitOfWork, IValidator<OfficeDto> validatorOffice)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _validatorOffice = validatorOffice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            var offices = await _unitOfWork.Offices.GetAsync(cancellationToken);

            return Ok(new { offices });
        }

        [HttpGet]
        [Route("{officeId:int}")]
        public async Task<IActionResult> GetAsync(int officeId, CancellationToken cancellationToken)
        {
            var office = await _unitOfWork.Offices.GetAsync(officeId, cancellationToken);

            return office is null ? throw new NotFoundException("The office was not found.") : Ok(new { office });
        }

        [HttpPut]
        [Route("{officeId:int}")]
        public async Task<IActionResult> PutAsync(int officeId, [FromBody] OfficeDto officeInput,
            CancellationToken cancellationToken)
        {
            var userId = GetUserIdFromContext();

            var office = await _unitOfWork.Offices.GetAsync(officeId, userId, cancellationToken) ??
                         throw new NotFoundException("The office was not found.");

            _mapper.Map(officeInput, office);
            office.Users.ForEach(u => u.IsActive = office.IsActive);

            _unitOfWork.Offices.Update(office);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }

        [HttpPatch]
        [Route("{officeId:int}/status")]
        public async Task<IActionResult> PatchAsync(int officeId, CancellationToken cancellationToken)
        {
            var userId = GetUserIdFromContext();

            var office = await _unitOfWork.Offices.GetAsync(officeId, userId, cancellationToken) ??
                         throw new NotFoundException("The office was not found.");

            office.IsActive = !office.IsActive;
            office.Users.ForEach(u => u.IsActive = office.IsActive);

            _unitOfWork.Offices.Update(office);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }

        [HttpPost("creation")]
        public async Task<IActionResult> PostAsync([FromBody] OfficeDto officeInput, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorOffice.ValidateAsync(officeInput, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var office = _mapper.Map<Office>(officeInput);

            var userId = GetUserIdFromContext();

            var user = await _unitOfWork.Users.GetAsync(userId, cancellationToken) ??
                       throw new NotFoundException("The user was not found.");

            if (user.OfficeId is not null)
            {
                throw new BadRequestException("You are already tied to an office.");
            }

            office.Users.Add(user);

            await _unitOfWork.Offices.AddAsync(office, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }
    }
}
