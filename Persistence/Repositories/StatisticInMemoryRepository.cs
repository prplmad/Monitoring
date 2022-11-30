using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Persistence.Repositories;

/// <inheritdoc />
public class StatisticInMemoryRepository : IStatisticRepository
{
    private readonly List<Statistic> _statistic;
    private int _id;

    /// <summary>
    /// Создание объекта коллекции со статистикой.
    /// </summary>
    public StatisticInMemoryRepository()
    {
        _statistic = new List<Statistic>();
        _id = 1;
    }

    /// <inheritdoc />
    public async Task<int> CreateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        statistic.Id = _id;
        _statistic.Add(statistic);
        _id++;
        return await Task.FromResult(statistic.Id);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        Statistic statisticForUpdating = _statistic.First(s => s.ExternalId == statistic.ExternalId);
        statisticForUpdating.Os = statistic.Os;
        statisticForUpdating.ClientVersion = statistic.ClientVersion;
        statisticForUpdating.UpdateDate = statistic.UpdateDate;
        statisticForUpdating.UserName = statistic.UserName;
        await Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyCollection<Statistic> sortedStatistics = _statistic.OrderBy(x => x.UpdateDate).ToList();
        return await Task.FromResult(sortedStatistics);
    }

    /// <inheritdoc />
    public Task<Statistic> GetByIdAsync(int id, CancellationToken cancellationToken = default) => throw new NotImplementedException();
}
