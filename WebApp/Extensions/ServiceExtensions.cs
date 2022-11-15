using FluentMigrator.Runner;
using Persistence.Migrations;

namespace WebApp.Extensions;

/// <summary>
/// Методы расширения для сервисов.
/// </summary>
public static class ServiceExtensions
{
    /// <summary>
    /// Подключение Cors.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="myAllowSpecificOrigins"></param>
    public static void ConfigureCors(this IServiceCollection services, string myAllowSpecificOrigins)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: myAllowSpecificOrigins,
                policy =>
                {
                    policy.WithOrigins("https://localhost:7130",
                        "https://localhost:28983");
                });
        });
    }

    /// <summary>
    /// Подключение мигратора.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void ConfigureFluentMigrator(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddLogging(c => c.AddFluentMigratorConsole())
            .AddFluentMigratorCore()
            .ConfigureRunner(c => c.AddPostgres11_0()
                .WithGlobalConnectionString(configuration.GetConnectionString("MyDb"))
                .ScanIn(typeof(AddStatisticTable_20221107).Assembly));
    }
}
