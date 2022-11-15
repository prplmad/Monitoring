using System.Net;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;


namespace IntegrationTests;

public class StatisticControllerTest
{
    private  WebApplicationFactory<Program> _factory;

    public StatisticControllerTest()
    {
        _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(_ => { });
    }

    [Fact]
    public async Task GetAllAsync_SendRequest_ShouldReturnOk()
    {
        //Arrange
        var httpClient = _factory.CreateClient();

        //Act
        HttpResponseMessage response = await httpClient.GetAsync("api/Statistic/GetAll");

        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
