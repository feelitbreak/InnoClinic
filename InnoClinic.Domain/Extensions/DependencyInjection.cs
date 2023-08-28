using Microsoft.Extensions.DependencyInjection;
using InnoClinic.Domain.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InnoClinic.Domain.Extensions
{
    public static class DependencyInjection
    {
        private static readonly string jwtOptionsName = "Jwt";

        public static void AddProjectOptions(this IServiceCollection services)
        {
            services.AddOptions<JwtOptions>().BindConfiguration(jwtOptionsName);
            services.AddOptions<AesCipherKeyOptions>().BindConfiguration(nameof(AesCipherKeyOptions));
        }
    }
}
