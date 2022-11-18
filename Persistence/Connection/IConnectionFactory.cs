using System.Data;
using System.Data.Common;

namespace Persistence.Connection;

/// <summary>
/// Cоздание подключения к БД.
/// </summary>
public interface IConnectionFactory
{
    /// <summary>
    /// Свойство для получения DbConnection.
    /// </summary>
    DbConnection Connection { get; }
    /// <summary>
    /// Свойство для получения DbTransaction.
    /// </summary>
    DbTransaction Transaction { get; }
    /// <summary>
    /// Создает подключение к БД.
    /// </summary>
    /// <returns><see cref="IDbConnection"/>.</returns>
    IDbConnection CreateConnection();
    /// <summary>
    /// Создает транзакцию.
    /// </summary>
    /// <returns><see cref="DbTransaction"/>.</returns>
    Task<DbTransaction> CreateTransactionAsync();

}
