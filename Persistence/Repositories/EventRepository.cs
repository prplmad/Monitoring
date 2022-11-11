using Domain.Entities;
using Domain.Repositories;
using Persistence.Connection;
using Dapper;

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
    public async Task<Statistic> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken = default)
    {
        var selectStatisticQuery = $"select * from statistic where id = {statisticId}";
        var query = $"SELECT * FROM event where statistic_id = {statisticId}";
        using (var connection = _connectionFactory.CreateConnection())
        {
            Statistic statisticWithEvents = await connection.QuerySingleAsync<Statistic>(new CommandDefinition(selectStatisticQuery, cancellationToken: cancellationToken));
            var events = await connection.QueryAsync<Event>(new CommandDefinition(query, cancellationToken: cancellationToken));
            statisticWithEvents.Events = events;
            return statisticWithEvents;
        }
    }

    /// <inheritdoc />
    public async Task CreateAsync(Event eventForCreation, CancellationToken cancellationToken)
    {
        var query = "INSERT INTO event (statistic_id ,name, date) VALUES (@StatisticId, @Name, @Date)";

        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.ExecuteAsync(new CommandDefinition(query, eventForCreation, cancellationToken: cancellationToken));
        }
    }
}
