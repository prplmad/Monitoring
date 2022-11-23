using System.Net;
using IntegrationTests.Extensions;
using Xunit;

namespace IntegrationTests;

/// <summary>
/// Тесты Event контроллера.
/// </summary>
public class EventControllerTest : IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    /// <summary>
    /// <see cref="EventControllerTest"/>.
    /// </summary>
    /// <param name="application"><see cref="ApiWebApplicationFactory"/>.</param>
    internal EventControllerTest(ApiWebApplicationFactory application)
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
        HttpResponseMessage response = await _client.GetAsync("api/Event/GetEventsByStatisticId?statisticId=1");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}
