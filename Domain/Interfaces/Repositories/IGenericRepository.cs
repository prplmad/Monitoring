using Domain.Entities;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Общий интерфейс для интерфейсов репозиториев.
/// </summary>
/// <typeparam name="T">Параметр для сущности.</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Метод для создания в БД.
    /// </summary>
    /// <param name="entity">Создаваемая сущность.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает Id сущности.</returns>
    Task<int> CreateAsync(T entity, CancellationToken cancellationToken);
}
