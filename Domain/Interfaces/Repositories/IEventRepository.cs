using Domain.Entities;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Содержит методы для операций с событиями в репозитории.
/// </summary>
public interface IEventRepository : IGenericRepository<Event>
{
    /// <summary>
    /// Получение коллекции событий по Id статистики.
    /// </summary>
    /// <param name="statisticId">Id статистики<see cref="Statistic"/>.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Коллекция событий одной статистики.</returns>
    Task<IReadOnlyCollection<Event>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken = default);
}
