using Domain.Interfaces.Repositories;
using Persistence.Connection;

namespace Persistence.Repositories;

public abstract class GenericRepository<T>: IGenericRepository<T> where T:class
{
    private readonly string _tableName;
    private readonly IConnectionFactory _connectionFactory;

    protected GenericRepository(string tableName, IConnectionFactory connectionFactory)
    {
        _tableName = tableName;
        _connectionFactory = connectionFactory;
    }

    public Task CreateAsync(T entity, CancellationToken cancellationToken) => throw new NotImplementedException();
}
