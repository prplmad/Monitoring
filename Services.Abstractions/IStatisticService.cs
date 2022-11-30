using Domain.Entities;

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
    Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Создание статистики.
    /// </summary>
    /// <param name="statistic">Модель статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task<int> CreateAsync(Statistic statistic, CancellationToken cancellationToken);

    /// <summary>
    /// Обновление статистики.
    /// </summary>
    /// <param name="statistic">Модель статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken);

    /// <summary>
    /// Получение статистики по Id.
    /// </summary>
    /// <param name="statisticId">Id статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns><see cref="Statistic"/>.</returns>
    Task<Statistic> GetByIdAsync(int statisticId, CancellationToken cancellationToken);
}
