using Domain.Entities;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Содержит методы для операций с событиями в репозитории.
/// </summary>
public interface IEventRepository
{
    /// <summary>
    /// Получение коллекции событий по Id статистики.
    /// </summary>
    /// <param name="statisticId">Id статистики<see cref="Statistic"/>.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Коллекция событий одной статистики.</returns>
    Task<IReadOnlyCollection<Event>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Создание события.
    /// </summary>
    /// <param name="eventForCreation">Объект события.</param>
    /// <param name="statisticExternalId">Id статистики в приложении Connect.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает Task.</returns>
    Task CreateAsync (Event eventForCreation, CancellationToken cancellationToken);
}
