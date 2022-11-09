using System.Data;

namespace Persistence.Connection;

/// <summary>
/// Cоздание подключения к БД.
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// Создание подключения.
    /// </summary>
    /// <returns>IDbConnection.</returns>
    public IDbConnection CreateConnection();
}
