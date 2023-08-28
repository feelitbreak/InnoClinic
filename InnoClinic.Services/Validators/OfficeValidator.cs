using FluentValidation;
using InnoClinic.Domain.DTOs;

namespace InnoClinic.Services.Validators
{
    public class OfficeValidator : AbstractValidator<OfficeDto>
    {
        public OfficeValidator()
        {
            RuleFor(u => u.City)
                .NotNull();

            RuleFor(u => u.Street)
                .NotNull();

            RuleFor(u => u.HouseNumber)
                .NotNull();

            RuleFor(u => u.OfficeNumber)
                .NotNull();

            RuleFor(u => u.RegistryPhoneNumber)
                .NotNull();
        }
    }
}
