using AutoMapper;
using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("office-management")]
    [Authorize(Roles = "Receptionist, Administrator")]
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

        [HttpPost("creation", Name = "Office Creation")]
        public async Task<IActionResult> PostAsync([FromBody] OfficeDto officeInput)
        {
            var validationResult = await _validatorOffice.ValidateAsync(officeInput);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var office = _mapper.Map<Office>(officeInput);

            await _unitOfWork.Offices.AddAsync(office);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { office });
        }
        
    }
}
