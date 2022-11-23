using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Extensions;

/// <summary>
/// Класс для конфигурации сервисов и БД для интеграционных тестов.
/// </summary>
public class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    /// <summary>
    /// Конфигурация тестового проекта.
    /// </summary>
    public IConfiguration Configuration { get; private set; }

    /// <inheritdoc/>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();
            config.AddConfiguration(Configuration);
        });
    }
}
