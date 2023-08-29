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
        [Route("{id:int}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var office = await _unitOfWork.Offices.GetAsync(id, cancellationToken);

            return office is null ? throw new OfficeNotFoundException(id) : Ok(new { office });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> PutAsync(int id, [FromBody] OfficeDto officeInput,
            CancellationToken cancellationToken)
        {
            var userId = GetUserIdFromContext();

            var user = await _unitOfWork.Users.GetAsync(userId, cancellationToken) ??
                       throw new UserNotFoundException(userId);

            if (user.OfficeId != id)
            {
                throw new UserDoesNotBelongToOfficeException(userId, id);
            }

            var office = user.Office ?? throw new OfficeNotFoundException(id);

            _mapper.Map(officeInput, office);
            office.UserList.ForEach(u => u.IsActive = office.IsActive);

            _unitOfWork.Offices.Update(office);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }

        [HttpPatch]
        [Route("{id:int}/status")]
        public async Task<IActionResult> PatchAsync(int id, CancellationToken cancellationToken)
        {
            var userId = GetUserIdFromContext();

            var user = await _unitOfWork.Users.GetAsync(userId, cancellationToken) ??
                       throw new UserNotFoundException(userId);

            if (user.OfficeId != id)
            {
                throw new UserDoesNotBelongToOfficeException(userId, id);
            }

            var office = user.Office ?? throw new OfficeNotFoundException(id);

            office.IsActive = !office.IsActive;
            office.UserList.ForEach(u => u.IsActive = office.IsActive);

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
                       throw new UserNotFoundException(userId);

            if (user.OfficeId is not null)
            {
                throw new UserAlreadyBelongsToOfficeException(userId, user.OfficeId.Value);
            }

            office.UserList.Add(user);

            await _unitOfWork.Offices.AddAsync(office, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }
    }
}
