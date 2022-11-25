using System.Net;
using IntegrationTests.Extensions;
using Xunit;


namespace IntegrationTests;

/// <summary>
/// Тесты Statistic контроллера.
/// </summary>
public class StatisticControllerTest : IClassFixture<ApiWebApplicationFactory>
{
    /// <summary>
    /// Тест метода GetAllAsync.
    /// </summary>
    /// <returns><see cref="Task"/>.</returns>
    [Fact]
    public async Task GetAllAsync_SendRequest_ShouldReturnOk()
    {
        //Act
        var application = new ApiWebApplicationFactory();
        var client = application.CreateClient();
        HttpResponseMessage response = await client.GetAsync("api/Statistic/GetAll");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
