using System.Runtime.CompilerServices;
using Domain.Repositories;
using Persistence.Repositories;
using Presentation;
using Services;
using Serilog;
using Services.Abstractions;
using FluentMigrator.Runner;
using Persistence.Extensions.DependencyInjection;
using Persistence.Migrations;
using WebApp.Extensions;


var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables("Mobile_");

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("https://localhost:7130",
                "https://localhost:28983");
        });
});

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);


builder.Services.AddLogging(c => c.AddFluentMigratorConsole())
    .AddFluentMigratorCore()
    .ConfigureRunner(c => c.AddPostgres11_0()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("MyDb"))
        .ScanIn(typeof(AddStatisticTable_20221107).Assembly));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerDocument();

builder.Services
    .AddPersistence();
builder.Services
    .AddScoped<IStatisticService, StatisticService>();
builder.Services
    .AddScoped<IStatisticRepository, StatisticRepository>();
builder.Services
    .AddScoped<IEventService, EventService>();
builder.Services
    .AddScoped<IEventRepository, EventRepository>();
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

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapFallbackToFile("index.html");

app.MigrateDatabase();

app.Run();
