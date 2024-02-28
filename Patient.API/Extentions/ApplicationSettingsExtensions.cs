using Patient.Settings;

namespace Patient.API.Extentions
{
    internal static class ApplicationSettingsExtensions
    {
        internal static IServiceCollection ConfigureApplicationSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureOptions<DatabaseSettingsConfigurator>();

            return services;
        }
    }
}
