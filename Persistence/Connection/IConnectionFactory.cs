using System.Data;
using Npgsql;

namespace Persistence.Connection;

/// <summary>
/// Cоздание подключения к БД.
/// </summary>
public interface IConnectionFactory
{
    IDbConnection CreateConnection();
}
