using InnoClinic.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using InnoClinic.Services.Validators;
using InnoClinic.Services.Implementation;

namespace InnoClinic.Services.Extensions
{
    public static class Extensions
    {
        public static void AddProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IPasswordEncryptionService, PasswordEncryptionService>();
            services.AddScoped<ITokenService, TokenService>();

            services.AddValidatorsFromAssemblyContaining<UserSignUpValidator>();
        }
    }
}
