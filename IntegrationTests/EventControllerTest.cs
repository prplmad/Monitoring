using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public class EventControllerTest
{
    private WebApplicationFactory<Program> _factory;

    public EventControllerTest()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
    }

    [Fact]
    public async Task GetAllAsync_SendRequest_ShouldReturnOk()
    {
        //Arrange
        var httpClient = _factory.CreateClient();

        //Act
        HttpResponseMessage response = await httpClient.GetAsync("api/Event/GetEventsByStatisticId?statisticId=1");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}
