using System.Data;
using System.Data.Common;
using System.Transactions;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence.Connection;

/// <inheritdoc />
public class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;
    private DbConnection _dbConnection;
    private DbTransaction _dbTransaction;

    /// <summary>
    /// Инициализация строки подключения и конфигурации.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения.</param>
    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MyDb");
    }

    public DbConnection Connection => _dbConnection;
    public DbTransaction Transaction => _dbTransaction;

    /// <inheritdoc />
    public IDbConnection CreateConnection()
    {
        _dbConnection = new NpgsqlConnection(_connectionString);
        return _dbConnection;
    }

    public async Task<DbTransaction> CreateTransactionAsync()
    {
        await Connection.OpenAsync();
        _dbTransaction = await Connection.BeginTransactionAsync();
        return _dbTransaction;
    }
}
