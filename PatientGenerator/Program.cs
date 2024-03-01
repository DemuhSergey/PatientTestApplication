// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PatientGenerator.Exceptions;
using PatientGenerator.Models;
using PatientGenerator.Services;
using PatientGenerator.Services.Abstractions;

var host = GetHost(args);
var fakeData = Patient.FakeData.Generate(100).ToList();
//var logger = host.Services.GetService<ILoggerFactory>();

try
{
    Console.WriteLine("Started!!!");
    var service = host.Services.GetRequiredService<IPatientService>();
    
    var ids = await service.Insert(fakeData);

    Console.WriteLine("Completed!!!");
} 
catch (PatientGeneratorException ex)
{
    //TODO: Add logger
    Console.WriteLine($"{ex.Message}");
}

static IHost GetHost(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddLogging(loggerBuilder => {
            loggerBuilder.ClearProviders();
            loggerBuilder.AddConsole();
        });
        services.AddTransient<IRestfullService<Patient>, RestfullService<Patient>>();
        services.AddTransient<IPatientService, PatientService>();
    }).Build();
