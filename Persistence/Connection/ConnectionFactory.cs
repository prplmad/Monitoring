using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence.Connection;

/// <summary>
/// Класс для создания подключения к БД.
/// </summary>
public class ConnectionFactory
{
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    /// <summary>
    /// Инициализация строки подключения и конфигурации.
    /// </summary>
    /// <param name="configuration">Конфигурация приложения appsettings.json.</param>
    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MyDb");
    }

    /// <summary>
    /// Создание подключения.
    /// </summary>
    /// <returns>IDbConnection.</returns>
    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
}
