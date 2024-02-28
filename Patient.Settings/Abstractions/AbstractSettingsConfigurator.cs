using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Patient.Settings.Abstractions
{
    public abstract class AbstractSettingsConfigurator<T>
    : AbstractValidator<T>
    , IConfigureOptions<T> where T : class
    {
        protected readonly IConfiguration _configuration;

        protected abstract string ConfigurationSectionName
        {
            get;
        }

        public AbstractSettingsConfigurator(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public virtual void Configure(T options)
        {
            this._configuration.GetSection(this.ConfigurationSectionName);
            var result = this.Validate(options);
        }
    }
}