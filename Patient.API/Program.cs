using Microsoft.EntityFrameworkCore;
using Patient.API;
using Patient.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureServices(builder.Configuration);
builder.WebHost.UseUrls("http://*:80");

var app = builder.Build();

app.UseSwagger();   
app.UseSwaggerUI();

ServicesConfiguration.MigrationInitialisation(app);
app.UseCors("PatientCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
