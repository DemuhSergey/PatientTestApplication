using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Patient.Domain.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Patient.Settings;
using Microsoft.Extensions.Configuration;

namespace Patient.Persistence
{
    public static class DependenciesBootstrapper
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddDbContext<PatientDbContext>((serviceProvider, dbContextOptionsBuilder) =>
            {
                var databaseSettings = serviceProvider.GetService<IOptions<DatabaseSettings>>()!.Value;
                var configuration = serviceProvider.GetService<IConfiguration>();

                dbContextOptionsBuilder.UseSqlServer(databaseSettings.ConnectionString, sqlServerAction =>
                {
                    sqlServerAction.MigrationsAssembly(typeof(PatientDbContext).Assembly.FullName);
                });

                dbContextOptionsBuilder.EnableDetailedErrors(databaseSettings.EnableDetailedErrors);
                dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseSettings.EnableSensitiveDataLogging);
            });

            services.AddScoped<IPatientDbContext>(provider => provider.GetRequiredService<PatientDbContext>());

            return services;
        }
    }
}
