namespace Domain.Interfaces.Repositories;

public interface IGenericRepository<T> where T : class
{
    Task CreateAsync(T entity, CancellationToken cancellationToken);
}
