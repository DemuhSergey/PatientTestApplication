using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Patient.API.Extentions;
using Patient.API.Filters;
using System.Reflection;
using Patient.Application;
using Patient.Persistence;
using Microsoft.AspNetCore.Authentication.Certificate;
using Microsoft.EntityFrameworkCore;
using Patient.Application.Common.Constants;

namespace Patient.API
{
    public static class ServicesConfiguration
    {

        public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureApplicationSettings(configuration);

            services.AddApplication();
            services.AddPersistence();

            services.AddAuthentication(CertificateAuthenticationDefaults.AuthenticationScheme)
                .AddCertificate();

            //services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddHttpContextAccessor();

            
            services.Configure<ApiBehaviorOptions>(options =>
               options.SuppressModelStateInvalidFilter = true);

            
            services.AddControllers(options =>
            {
                options.Filters.Add<ApiExceptionFilterAttribute>();
            }).AddFluentValidation(config =>
            {
                config.AutomaticValidationEnabled = false;
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
                options.SerializerSettings.DateFormatString = ApplicationConstants.DateTimeFormatConst;
            });

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Patient API"
                });

                options.CustomSchemaIds(x => x.FullName);
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "PatientCorsPolicy", policy =>
                {
                    policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                });
            });

            return services;
        }



        public static void MigrationInitialisation(WebApplication app)
        {
            using (var serviceScope = app.Services.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<PatientDbContext>().Database.Migrate();
            }
        }
    }
}
