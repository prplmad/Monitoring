using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories;

/// <inheritdoc />
public class StatisticInMemoryRepository : IStatisticRepository
{
    private readonly List<Statistic> _statistic;

    /// <summary>
    /// Создание объекта коллекции со статистикой.
    /// </summary>
    public StatisticInMemoryRepository()
    {
        _statistic = new List<Statistic>();
    }

    /// <inheritdoc />
    public async Task CreateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        _statistic.Add(statistic);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        Statistic statisticForUpdating = _statistic.First(s => s.ExternalId == statistic.ExternalId);
        statisticForUpdating.Os = statistic.Os;
        statisticForUpdating.ClientVersion = statistic.ClientVersion;
        statisticForUpdating.UpdateDate = statistic.UpdateDate;
        statisticForUpdating.UserName = statistic.UserName;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Statistic> sortedStatistics = _statistic.OrderBy(x => x.UpdateDate).ToList();
        return sortedStatistics;
    }
}
