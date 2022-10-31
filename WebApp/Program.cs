using Domain.Repositories;
using Persistence.Repositories;
using Presentation;
using Services;
using Serilog;
using Serilog.Core;
using Services.Abstractions;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddEndpointsApiExplorer();


builder.Services
    .AddSingleton<IStatisticRepository, StatisticInMemoryRepository>();
builder.Services
    .AddScoped<IStatisticService, StatisticService>();
builder.Services
    .AddSingleton(Log.Logger);

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();

app.MapControllers();

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapFallbackToFile("index.html");

app.Run();
