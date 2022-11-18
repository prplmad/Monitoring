using System.Data;
using System.Data.Common;
using Npgsql;

namespace Persistence.Connection;

/// <summary>
/// Cоздание подключения к БД.
/// </summary>
public interface IConnectionFactory
{
    IDbConnection CreateConnection();
    Task<DbTransaction> CreateTransactionAsync();
    DbConnection Connection { get; }
    DbTransaction Transaction { get; }
}
