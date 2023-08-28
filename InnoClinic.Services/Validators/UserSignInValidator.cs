using FluentValidation;
using InnoClinic.Domain.DTOs;

namespace InnoClinic.Services.Validators
{
    public class UserSignInValidator : AbstractValidator<UserSignInDto>
    {
        public UserSignInValidator()
        {
            RuleFor(u => u.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(15);
        }
    }
}
