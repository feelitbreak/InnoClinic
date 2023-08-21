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
    public class UserSignInValidator : AbstractValidator<UserSignInDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserSignInValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(u => u.Email)
                .NotNull()
                .EmailAddress();

            RuleFor(u => u.Password)
                .NotNull()
                .MinimumLength(6)
                .MaximumLength(15);

            RuleFor(u => u)
                .MustAsync(IsCorrectSignInAsync)
                .WithMessage("Either the email or password is incorrect");
        }

        private async Task<bool> IsCorrectSignInAsync(UserSignInDto user, CancellationToken token)
        {
            var userWithSameEmail = await _unitOfWork.Users.GetByEmailAsync(user.Email);

            return userWithSameEmail is not null && user.Password.Equals(userWithSameEmail.Password);
        }
    }
}
