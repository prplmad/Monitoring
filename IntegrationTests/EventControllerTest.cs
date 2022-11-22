using System.Net;
using IntegrationTests.Extensions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace IntegrationTests;

public class EventControllerTest : IClassFixture<ApiWebApplicationFactory>
{
    readonly HttpClient _client;

    public EventControllerTest(ApiWebApplicationFactory application)
    {
        _client = application.CreateClient();
    }

    [Fact]
    public async Task GetAllAsync_SendRequest_ShouldReturnOk()
    {
        //Act
        HttpResponseMessage response = await _client.GetAsync("api/Event/GetEventsByStatisticId?statisticId=1");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

}
