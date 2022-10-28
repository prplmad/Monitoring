using Domain.Repositories;
using Microsoft.OpenApi.Models;
using Persistence.Repositories;
using Presentation;
using Services;
using Serilog;
using Services.Abstractions;
using Microsoft.AspNetCore.SpaServices.AngularCli;


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(AssemblyReference).Assembly);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSpaStaticFiles(configuration =>
{
    configuration.RootPath = "client/dist";
});

builder.Services
    .AddSingleton<IStatisticRepository, StatisticInMemoryRepository>();
builder.Services
    .AddScoped<IStatisticService, StatisticService>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
if (!app.Environment.IsDevelopment())
{
    app.UseSpaStaticFiles();
}
app.MapControllers();
app.UseSpa(spa =>
{
    spa.Options.SourcePath = "client";

    if (app.Environment.IsDevelopment())
    {
        spa.UseAngularCliServer(npmScript: "start");
    }
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
