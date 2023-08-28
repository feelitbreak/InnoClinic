using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Domain.Options;

namespace InnoClinic.Domain.Extensions
{
    public static class DependencyInjection
    {
        private const string JwtOptionsName = "Jwt";

        public static void AddProjectOptions(this IServiceCollection services)
        {
            services.AddOptions<JwtOptions>().BindConfiguration(JwtOptionsName);
        }
    }
}
