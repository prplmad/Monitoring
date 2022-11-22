using System.Net;
using IntegrationTests.Extensions;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;


namespace IntegrationTests;

public class StatisticControllerTest : IClassFixture<ApiWebApplicationFactory>
{
    readonly HttpClient _client;

    public StatisticControllerTest(ApiWebApplicationFactory application)
    {
        _client = application.CreateClient();
    }

    [Fact]
    public async Task GetAllAsync_SendRequest_ShouldReturnOk()
    {
        //Act
        HttpResponseMessage response = await _client.GetAsync("api/Statistic/GetAll");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
