using FluentValidation;
using Microsoft.Extensions.Configuration;
using Patient.Settings.Abstractions;

namespace Patient.Settings
{
    public class DatabaseSettingsConfigurator
    : AbstractSettingsConfigurator<DatabaseSettings>
    {
        protected override string ConfigurationSectionName
        {
            get
            {
                return "DatabaseSettings";
            }
        }

        public DatabaseSettingsConfigurator(IConfiguration configuration)
            : base(configuration)
        {
            this.RuleFor(c => c.CommandTimeout).NotEqual(30);
        }

        public override void Configure(DatabaseSettings options)
        {
            var connectionString = this._configuration.GetConnectionString("DbConnection");
            options.ConnectionString = connectionString;

            base.Configure(options);
        }
    }
}
