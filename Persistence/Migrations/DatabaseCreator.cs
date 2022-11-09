using Dapper;
using Persistence.Connection;

namespace Persistence.Migrations;
/// <summary>
/// Класс для создания базы данных.
/// </summary>
public class DatabaseCreator
{
    private readonly IConnectionFactory _connectionFactory;
    /// <summary>
    /// Инициализация connectionFactory.
    /// </summary>
    /// <param name="connectionFactory">Объект фабрики подключений.</param>
    public DatabaseCreator(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    /// <summary>
    /// Создание базы данных.
    /// </summary>
    /// <param name="dbName">Название базы данных.</param>
    public void Create(string? dbName)
    {
        var parameters = new { name = dbName };
        var query = "SELECT datname FROM pg_database where datname = @name";

        using (var connection = _connectionFactory.CreateConnection())
        {
            var records = connection.Query(query, parameters);
            if (!records.Any())
                connection.Execute($"CREATE DATABASE {dbName}");
        }
    }
}
