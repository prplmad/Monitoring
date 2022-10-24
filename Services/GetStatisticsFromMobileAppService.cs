using Contracts;
using Services.Abstractions;


namespace Services;

public class GetStatisticsFromMobileAppService<T> : IGetStatisticsFromMobileAppService<T>
{
    public async Task GetStatisticsFromMobileAppAsync(StatisticsDto<T> statistics)
    {
        throw new NotImplementedException();
    }
}