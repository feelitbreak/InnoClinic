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
    [Authorize(Roles = nameof(Role.Administrator))]
    public class RoleManagerController : BaseController
    {
        private readonly ILogger<RoleManagerController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserRoleDto> _validatorUserRole;

        public RoleManagerController(ILogger<RoleManagerController> logger, IUnitOfWork unitOfWork, IValidator<UserRoleDto> validatorUserRole) : base(logger)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _validatorUserRole = validatorUserRole;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeRoleAsync([FromBody] UserRoleDto userRole, CancellationToken cancellationToken)
        {
            var validationResult = await _validatorUserRole.ValidateAsync(userRole, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var user = await _unitOfWork.Users.GetByEmailAsync(userRole.UserEmail, cancellationToken);
            if (user is null)
            {
                _logger.LogError("The user with the email {userEmail} was not found or is not active.", userRole.UserEmail);
                throw new ClinicException("The user was not found.", ExceptionCode.UserNotFoundException);
            }

            user.Role = userRole.Role;
            _unitOfWork.Users.Update(user);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation(
                "The role of the user with the email {userEmail} has been changed to {@userRole}.",
                userRole.UserEmail, userRole.Role);

            return NoContent();
        }
    }
}
