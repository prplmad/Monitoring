using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Persistence.Context;

namespace Persistence.Repositories;

/// <inheritdoc />
public class StatisticRepository : IStatisticRepository
{
    private readonly DapperContext _context;

    public StatisticRepository(DapperContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public Task CreateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var query = "SELECT * FROM Statistic";
        using (var connection = _context.CreateConnection())
        {
            var statistics = await connection.QueryAsync<Statistic>(query);
            return statistics.ToList();
        }
    }
}
