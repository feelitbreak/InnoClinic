﻿using InnoClinic.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using InnoClinic.Services.Validators;

namespace InnoClinic.Services.Extensions
{
    public static class Extensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();

            services.AddValidatorsFromAssemblyContaining<UserSignUpValidator>();
        }
    }
}