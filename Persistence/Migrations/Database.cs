using Dapper;
using Persistence.Connection;

namespace Persistence.Migrations;
/// <summary>
/// Класс для создания базы данных.
/// </summary>
public class Database
{
    private readonly ConnectionFactory _connectionFactory;
    /// <summary>
    /// Инициализация connectionFactory.
    /// </summary>
    /// <param name="connectionFactory"></param>
    public Database(ConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void CreateDatabase(string dbName)
    {
        var query = "SELECT datname FROM pg_database where \"datname\" = @name";

        var parameters = new DynamicParameters();
        parameters.Add("name", dbName);
        using (var connection = _connectionFactory.CreateConnection())
        {
            var records = connection.Query(query, parameters);
            if (!records.Any())
                connection.Execute($"CREATE DATABASE {dbName}");
        }
    }
}
