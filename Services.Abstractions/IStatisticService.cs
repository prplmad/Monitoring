using Contracts;
using Serilog;

namespace Services.Abstractions;

/// <summary>
/// Сервис для операций со статистикой.
/// </summary>
public interface IStatisticService
{
    /// <summary>
    /// Получение всех элементов статистики.
    /// </summary>
    /// <param name="cancellationToken"> Токен для отмены задачи.</param>
    /// <returns>Возвращается коллекция с элементами статистики.</returns>
    Task<IReadOnlyCollection<StatisticDto>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Создание статистики.
    /// </summary>
    /// <param name="statisticsForCreationDto">ДТО статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task CreateAsync(StatisticForCreationDto statisticsForCreationDto, CancellationToken cancellationToken = default);

    /// <summary>
    /// Обновление статистики.
    /// </summary>
    /// <param name="statisticsForUpdatingDto">ДТО статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task UpdateAsync(StatisticForUpdatingDto statisticsForUpdatingDto, CancellationToken cancellationToken = default);
}
