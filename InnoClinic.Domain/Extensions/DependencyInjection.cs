using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Domain.Options;

namespace InnoClinic.Domain.Extensions
{
    public static class DependencyInjection
    {
        public static void AddProjectOptions(this IServiceCollection services)
        {
            services.AddOptions<JwtOptions>().BindConfiguration(nameof(JwtOptions));
        }
    }
}
