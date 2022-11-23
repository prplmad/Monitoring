using System.Net;
using IntegrationTests.Extensions;
using Xunit;


namespace IntegrationTests;

/// <summary>
/// Тесты Statistic контроллера.
/// </summary>
public class StatisticControllerTest : IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    /// <summary>
    /// Создает объект HttpClient.
    /// </summary>
    /// <param name="application"><see cref="ApiWebApplicationFactory"/>.</param>
    public StatisticControllerTest(ApiWebApplicationFactory application)
    {
        _client = application.CreateClient();
    }

    /// <summary>
    /// Тест метода GetAllAsync.
    /// </summary>
    /// <returns><see cref="Task"/>.</returns>
    [Fact]
    public async Task GetAllAsync_SendRequest_ShouldReturnOk()
    {
        //Act
        HttpResponseMessage response = await _client.GetAsync("api/Statistic/GetAll");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
