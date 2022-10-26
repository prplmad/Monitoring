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
    public async Task CreateAsync(Statistics item, CancellationToken cancellationToken = default)
    {
        _statistics.Add(item);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistics item, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistics>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
