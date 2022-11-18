using System.Data;
using System.Transactions;
using Domain.Entities;
using Domain.Interfaces.Repositories;
using Persistence.Connection;
using Dapper;
using Npgsql;

namespace Persistence.Repositories;

/// <inheritdoc />
public class EventRepository : IEventRepository
{
    private readonly IConnectionFactory _connectionFactory;

    /// <summary>
    /// Инициализация connectionFactory.
    /// </summary>
    /// <param name="connectionFactory">Соединение с БД.</param>
    public EventRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Event>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT * FROM event where statistic_id = {statisticId}";
        using (var connection = _connectionFactory.CreateConnection())
        {
            var events = await connection.QueryAsync<Event>(new CommandDefinition(query, cancellationToken: cancellationToken));
            return events.ToList();
        }
    }

    /// <inheritdoc />
    public async Task CreateAsync(Event eventForCreation, CancellationToken cancellationToken)
    {
        var query = "INSERT INTO event (statistic_id ,name, date) VALUES (@StatisticId, @Name, @Date)";
        var connection = _connectionFactory.CreateConnection();
        await connection.ExecuteAsync(new CommandDefinition(query, eventForCreation, cancellationToken: cancellationToken, transaction:await _connectionFactory.CreateTransactionAsync()));
    }
}
