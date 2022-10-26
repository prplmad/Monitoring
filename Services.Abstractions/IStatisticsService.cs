using Contracts;
using Serilog;

namespace Services.Abstractions;

/// <summary>
/// Сервис для операций со статистикой.
/// </summary>
public interface IStatisticsService
{
    /// <summary>
    /// Получение всех элементов статистики.
    /// </summary>
    /// <param name="cancellationToken"> Токен для отмены задачи.</param>
    /// <returns>Возвращается коллекция с элементами статистики.</returns>
    Task<IReadOnlyCollection<StatisticsDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Создание статистики.
    /// </summary>
    /// <param name="statisticsForCreationDto">ДТО статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task CreateAsync(StatisticsForCreationDto statisticsForCreationDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновление статистики.
    /// </summary>
    /// <param name="statisticsForUpdatingDto">ДТО статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task UpdateAsync(StatisticsForUpdatingDto statisticsForUpdatingDto, CancellationToken cancellationToken = default);
}
