using Domain.Entities;
using Domain.Repositories;
using Persistence.Connection;
using Dapper;

namespace Persistence.Repositories;

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
    public async Task<IReadOnlyCollection<Event>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken = default)
    {
        var query = $"SELECT * FROM event where statistic_id = {statisticId}";
        using (var connection = _connectionFactory.CreateConnection())
        {
            var events = await connection.QueryAsync<Event>(new CommandDefinition(query, cancellationToken: cancellationToken));
            return events.ToList();
        }
    }
}
