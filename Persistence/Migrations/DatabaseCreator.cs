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
    /// <param name="connectionFactory"></param>
    public DatabaseCreator(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void CreateDatabase(string? dbName)
    {
        Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
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
