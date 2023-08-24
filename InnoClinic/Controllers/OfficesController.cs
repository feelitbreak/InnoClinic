using AutoMapper;
using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Enums;
using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("office-management")]
    [Authorize(Roles = nameof(Role.Receptionist) + "," + nameof(Role.Administrator))]
    public class OfficesController : ControllerBase
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

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
            var user = await _unitOfWork.Users.GetAsync(int.Parse(userId), cancellationToken);
            office.UserList.Add(user!);

            await _unitOfWork.Offices.AddAsync(office, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Ok(new { office });
        }
    }
}
