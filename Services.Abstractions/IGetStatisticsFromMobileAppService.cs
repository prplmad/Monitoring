using Contracts;

namespace Services.Abstractions;

public interface IGetStatisticsFromMobileAppService<T>
{
    public Task GetStatisticsFromMobileAppAsync(StatisticsDto<T> statistics);
}