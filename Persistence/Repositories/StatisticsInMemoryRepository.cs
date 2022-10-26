using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

/// <inheritdoc />
public class StatisticsInMemoryRepository : IStatisticsRepository
{
    private readonly List<Statistics> _statistics;

    /// <summary>
    /// Создание объекта коллекции со статистикой.
    /// </summary>
    public StatisticsInMemoryRepository()
    {
        _statistics = new List<Statistics>();
    }

    /// <inheritdoc />
    public async Task CreateAsync(Statistics statistics, CancellationToken cancellationToken = default)
    {
        _statistics.Add(statistics);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistics statistics, CancellationToken cancellationToken = default)
    {
        Statistics statisticsForUpdating = _statistics.First(s => s.ExternalId == statistics.ExternalId);
        statisticsForUpdating.Os = statistics.Os;
        statisticsForUpdating.ClientVersion = statistics.ClientVersion;
        statisticsForUpdating.UpdateDate = statistics.UpdateDate;
        statisticsForUpdating.UserName = statistics.UserName;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistics>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Statistics> sortedStatistics = _statistics.OrderBy(x => x.UpdateDate).ToList();
        return sortedStatistics;
    }
}
