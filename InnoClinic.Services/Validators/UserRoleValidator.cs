using FluentValidation;
using InnoClinic.Domain.DTOs;

namespace InnoClinic.Services.Validators
{
    public class UserRoleValidator : AbstractValidator<UserRoleDto>
    {
        public UserRoleValidator()
        {
            RuleFor(u => u.UserEmail)
                .NotNull()
                .EmailAddress();

            RuleFor(u => u.Role)
                .NotNull()
                .IsInEnum();
        }
    }
}
