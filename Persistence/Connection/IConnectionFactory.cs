using System.Data;

namespace Persistence.Connection;

/// <summary>
/// Cоздание подключения к БД.
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// Создает подключение к БД.
    /// </summary>
    /// <returns><see cref="IDbConnection"/>.</returns>
    IDbConnection CreateConnection();
}
