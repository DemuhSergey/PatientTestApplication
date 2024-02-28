// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PatientGenerator.Models;
using PatientGenerator.Services;
using PatientGenerator.Services.Abstractions;
using System;

var host = GetHost(args);
var fakeData = Patient.FakeData.Generate(100).ToList();
var logger = host.Services.GetService<ILoggerFactory>();

try
{
    Console.WriteLine("Started!!!");
    var service = host.Services.GetRequiredService<IPatientService>();
    var ids = await service.Insert(fakeData);

    for (int i = 0; i < ids.Count(); i++)
    {
        var counter = i++;
        Console.WriteLine($"{counter}){fakeData[i].Family} -- inserted");
    }
    Console.WriteLine("Completed!!!");
} 
catch (Exception ex)
{
    Console.WriteLine(ex);
}

static IHost GetHost(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        services.AddLogging(loggerBuilder => {
            loggerBuilder.ClearProviders();
            loggerBuilder.AddConsole()
                .AddFilter(level => level >= LogLevel.Error);
        });
        services.AddTransient<IRestfullService<Patient>, RestfullService<Patient>>();
        services.AddTransient<IPatientService, PatientService>();
    }).Build();
