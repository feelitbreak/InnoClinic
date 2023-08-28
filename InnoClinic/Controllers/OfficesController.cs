using AutoMapper;
using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Enums;
using InnoClinic.Domain.Entities;
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

            if (office is null)
            {
                return NotFound();
            }

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
            if (userId is null)
            {
                return BadRequest(new { errorMessage = "Couldn't find current user" });
            }

            var user = await _unitOfWork.Users.GetAsync(userId.Value, cancellationToken);
            if (user is null)
            {
                return BadRequest(new { errorMessage = "Couldn't find current user" });
            }

            if (user.Office is not null)
            {
                return BadRequest(new { errorMessage = "You are already tied to an office" });
            }

            office.UserList.Add(user);

            await _unitOfWork.Offices.AddAsync(office, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }

        [HttpPatch]
        [Route("{id:int}/status")]
        public async Task<IActionResult> PatchAsync(int id, CancellationToken cancellationToken)
        {
            var userId = GetUserIdFromContext();
            if (userId is null)
            {
                return BadRequest(new { errorMessage = "Couldn't find current user" });
            }

            var user = await _unitOfWork.Users.GetAsync(userId.Value, cancellationToken);
            if (user is null)
            {
                return BadRequest(new { errorMessage = "Couldn't find current user" });
            }

            if (user.OfficeId != id)
            {
                return BadRequest(new { errorMessage = "You can only edit your own office" });
            }

            var office = user.Office;
            if (office is null)
            {
                return NotFound();
            }

            office.IsActive = !office.IsActive;
            office.UserList.ForEach(u => u.IsActive = office.IsActive);

            _unitOfWork.Offices.Update(office);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }
    }
}
