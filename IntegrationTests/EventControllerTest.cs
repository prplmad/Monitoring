using System.Net;
using IntegrationTests.Extensions;
using Xunit;

namespace IntegrationTests;

/// <summary>
/// Тесты Event контроллера.
/// </summary>
public class EventControllerTest : IClassFixture<ApiWebApplicationFactory>
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
        HttpResponseMessage response = await client.GetAsync("api/Event/GetEventsByStatisticId?statisticId=1");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}
