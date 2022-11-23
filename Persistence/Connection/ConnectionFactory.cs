using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence.Connection;

/// <inheritdoc />
public class ConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;
    /// <summary>
    /// Инициализация строки подключения и конфигурации.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    public ConnectionFactory(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("MyDb");
    }

    /// <inheritdoc />
    public IDbConnection CreateConnection()
    {
        IDbConnection dbConnection = new NpgsqlConnection(_connectionString);
        return dbConnection;
    }
}
