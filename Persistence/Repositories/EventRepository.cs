using System.Data;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Dapper;

namespace Persistence.Repositories;

/// <inheritdoc />
public class EventRepository : IEventRepository
{
    private readonly IDbTransaction _transaction;
    private readonly IDbConnection _connection;

    /// <summary>
    /// Инициализация connectionFactory.
    /// </summary>
    /// <param name="connection">Соединение.</param>>
    /// <param name="transaction">Транзакция.</param>>
    public EventRepository(IDbConnection connection, IDbTransaction transaction)
    {
        _connection = connection;
        _transaction = transaction;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Event>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT * FROM event where statistic_id = {statisticId}";
        var events = await _connection.QueryAsync<Event>(new CommandDefinition(query, cancellationToken: cancellationToken, transaction:_transaction));
        return events.ToList();
    }

    /// <inheritdoc />
    public async Task<int> CreateAsync(Event eventForCreation, CancellationToken cancellationToken)
    {
        var query = "INSERT INTO event (statistic_id ,name, date) VALUES (@StatisticId, @Name, @Date) returning id";
        int id = await _connection.QuerySingleAsync<int>(new CommandDefinition(query, eventForCreation, cancellationToken: cancellationToken, transaction:_transaction));
        return id;
    }
}
