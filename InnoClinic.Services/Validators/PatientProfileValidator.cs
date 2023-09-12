using FluentValidation;
using InnoClinic.Domain.DTOs;

namespace InnoClinic.Services.Validators
{
    public class PatientProfileValidator : AbstractValidator<PatientProfileDto>
    {
        public PatientProfileValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull();

            RuleFor(p => p.LastName)
                .NotNull();

            RuleFor(p => p.PhoneNumber)
                .NotNull();

            RuleFor(p => p.DateOfBirth)
                .NotNull();
        }
    }
}