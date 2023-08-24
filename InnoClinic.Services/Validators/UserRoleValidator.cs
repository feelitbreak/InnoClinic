using FluentValidation;
using InnoClinic.Domain.DTOs;
using InnoClinic.Domain.Entities;
using InnoClinic.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
