﻿using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Enums;
using InnoClinic.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace InnoClinic.Controllers
{
    [ApiController]
    [Route("role-manager")]
    [Authorize(Roles = nameof(Role.Administrator))]
    public class RoleManagerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserRoleDto> _validatorUserRole;

        public RoleManagerController(IUnitOfWork unitOfWork, IValidator<UserRoleDto> validatorUserRole)
        {
            _unitOfWork = unitOfWork;
            _validatorUserRole = validatorUserRole;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] UserRoleDto userRole, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUserRole.ValidateAsync(userRole, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(userRole.UserEmail, cancellationToken);

            if (user is null)
            {
                return BadRequest(new { errorMessage = "User not found" });
            }

            user.Role = userRole.Role;
            _unitOfWork.Users.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}