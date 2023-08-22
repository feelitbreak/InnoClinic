using InnoClinic.Domain.Interfaces;
using InnoClinic.Infrastructure.Implementation;
using InnoClinic.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InnoClinic.Infrastructure.Extensions
{
    public static class Extensions
    {
        private static readonly string connectionStringName = "ClinicDbContext";

        public static void AddSqlServerDb(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ClinicDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(connectionStringName)));
        }

        public static void AddUnitOfWork(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}
