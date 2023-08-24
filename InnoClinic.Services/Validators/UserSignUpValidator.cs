using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Services.Validators
{
    public class UserSignUpValidator : AbstractValidator<UserSignUpDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserSignUpValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(u => u.Email)
                .NotNull()
                .EmailAddress()
                .MustAsync(IsUniqueEmailAsync)
                .WithMessage("User with this email already exists");

            RuleFor(u => u.Password)
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(15);

            RuleFor(u => u.ReenteredPassword)
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(15)
                .Equal(u => u.Password)
                .WithMessage("The passwords you’ve entered don’t coincide");
        }

        private async Task<bool> IsUniqueEmailAsync(string email, CancellationToken cancellationToken)
        {
            var userWithSameEmail = await _unitOfWork.Users.GetByEmailAsync(email, cancellationToken);

            return userWithSameEmail is null;
        }
    }
}
