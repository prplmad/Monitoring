using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace IntegrationTests.Extensions;

internal class ApiWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Is be called after the `ConfigureServices` from the Startup
        // which allows you to overwrite the DI with mocked instances
        builder.ConfigureTestServices(services =>
        {
        });
    }

    public IConfiguration Configuration { get; private set; }
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration(config =>
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("integrationsettings.json")
                .Build();
            config.AddConfiguration(Configuration);
        });
}
