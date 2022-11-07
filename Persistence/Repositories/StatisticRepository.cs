using System.Data;
using Dapper;
using Domain.Entities;
using Domain.Repositories;
using Persistence.Connection;

namespace Persistence.Repositories;

/// <inheritdoc />
public class StatisticRepository : IStatisticRepository
{
    private readonly ConnectionFactory _connectionFactory;

    /// <summary>
    /// Инициализация connectionFactory.
    /// </summary>
    /// <param name="connectionFactory">Соединение с БД.</param>
    public StatisticRepository(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <inheritdoc />
    public async Task CreateAsync(Statistic statistic, CancellationToken cancellationToken)
    {
        var query = "INSERT INTO statistic (external_id, username, client_version, os, update_date) VALUES (@ExternalId, @UserName, @ClientVersion, @Os, NOW())";
        var parameters = new DynamicParameters();
        parameters.Add("ExternalId", statistic.ExternalId, DbType.Int64);
        parameters.Add("UserName", statistic.UserName, DbType.String);
        parameters.Add("ClientVersion", statistic.ClientVersion, DbType.String);
        parameters.Add("Os", statistic.Os, DbType.String);
        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.ExecuteAsync(new CommandDefinition(query, parameters, cancellationToken: cancellationToken));
        }
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken)
    {
        var query = "UPDATE statistic SET username = @UserName, client_version = @ClientVersion, os = @Os, update_date = NOW() WHERE external_id = @ExternalId";
        var parameters = new DynamicParameters();
        parameters.Add("ExternalId", statistic.ExternalId, DbType.Int64);
        parameters.Add("UserName", statistic.UserName, DbType.String);
        parameters.Add("ClientVersion", statistic.ClientVersion, DbType.String);
        parameters.Add("Os", statistic.Os, DbType.String);

        using (var connection = _connectionFactory.CreateConnection())
        {
            await connection.ExecuteAsync(new CommandDefinition(query, parameters, cancellationToken: cancellationToken));
        }
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken)
    {
        var query = "SELECT * FROM statistic";
        using (var connection = _connectionFactory.CreateConnection())
        {
            var statistics = await connection.QueryAsync<Statistic>(new CommandDefinition(query, cancellationToken: cancellationToken));
            return statistics.ToList();
        }
    }
}
