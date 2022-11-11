using Contracts;

namespace Services.Abstractions;

/// <summary>
/// Сервис для операций с событиями.
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Получение коллеции событий по Id статистики.
    /// </summary>
    /// <param name="statisticId">Id статистики <see cref="StatisticDto"/>.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Коллекция событий одной статистики.</returns>
    Task<StatisticWithEventsDto> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken);

    /// <summary>
    /// Создание события.
    /// </summary>
    /// <param name="eventForCreationDto">ДТО для создания события.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task CreateAsync(EventForCreationDto eventForCreationDto, CancellationToken cancellationToken);
}
