using Domain.Validators;
using FluentValidation;
using Presentation;
using Services;
using Serilog;
using Services.Abstractions;
using Persistence.Extensions.DependencyInjection;
using WebApp.Extensions;


var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables("Mobile_");

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.ConfigureCors(myAllowSpecificOrigins);
// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<StatisticValidator>();
builder.Services.ConfigureFluentMigrator(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument();
builder.Services
    .AddPersistence();
builder.Services
    .AddScoped<IStatisticService, StatisticService>();
builder.Services
    .AddScoped<IEventService, EventService>();
builder.Services
    .AddSingleton(Log.Logger);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.UseOpenApi();
app.UseSwaggerUi3();

app.MapControllers();

app.UseHttpsRedirection();

app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapFallbackToFile("index.html");

app.MigrateDatabase();

app.Run();

public partial class Program
{
}
