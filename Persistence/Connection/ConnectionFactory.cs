using System.Data;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence.Connection;

/// <inheritdoc />
public class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private IDbConnection _dbConnection;

    /// <summary>
    /// Инициализация строки подключения и конфигурации.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MyDb");
    }

    /// <inheritdoc />
    public IDbConnection CreateConnection()
    {
        _dbConnection = new NpgsqlConnection(_connectionString);
        return _dbConnection;
    }
}
