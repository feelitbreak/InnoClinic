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

            RuleFor(u => u.Status)
                .NotNull()
                .IsInEnum();
        }
    }
}
