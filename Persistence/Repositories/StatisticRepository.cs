using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Interfaces.Repositories;

namespace Persistence.Repositories;

/// <inheritdoc />
public class StatisticRepository : IStatisticRepository
{
    private readonly IDbTransaction _transaction;
    private readonly IDbConnection _connection;
    /// <summary>
    /// Инициализация connectionFactory.
    /// </summary>
    /// <param name="connection">Соединение.</param>>
    /// <param name="transaction">Транзакция.</param>>
    public StatisticRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    /// <inheritdoc />
    public async Task<int> CreateAsync(Statistic statistic, CancellationToken cancellationToken)
    {
        var query = "INSERT INTO statistic (external_id, username, client_version, os, update_date) VALUES (@ExternalId, @UserName, @ClientVersion, @Os, NOW()) returning id";
        var id = await _connection.QuerySingleAsync<int>(new CommandDefinition(query, statistic, cancellationToken: cancellationToken, transaction: _transaction));
        return id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken)
    {
        var query = "UPDATE statistic SET username = @UserName, client_version = @ClientVersion, os = @Os, update_date = NOW() WHERE external_id = @ExternalId";
        await _connection.ExecuteAsync(new CommandDefinition(query, statistic, cancellationToken: cancellationToken, transaction: _transaction));
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM statistic";
        var statistics = await _connection.QueryAsync<Statistic>(new CommandDefinition(query, cancellationToken: cancellationToken, transaction:_transaction));
        return statistics.ToList();

    }

    /// <inheritdoc />
    public async Task<Statistic> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        var query = $"SELECT * FROM statistic WHERE id = {id}";
        var statistic = await _connection.QuerySingleAsync<Statistic>(new CommandDefinition(query, cancellationToken: cancellationToken, transaction:_transaction));
        return statistic;
    }
}
