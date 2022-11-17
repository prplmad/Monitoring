using Domain.Entities;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Содержит методы для операций со статистикой в репозитории.
/// </summary>
public interface IStatisticRepository : IGenericRepository<Statistic>
{
    /// <summary>
    /// Обновление уже имеющейся статистики.
    /// </summary>
    /// <param name="statistic">Объект статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает Task.</returns>
    Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение всей имеющейся статистики.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает readonly коллекцию со всей имеющейся статистикой.</returns>
    Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получение статистики по Id.
    /// </summary>
    /// <param name="id">Id статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns><see cref="Statistic"/>.</returns>
    Task<Statistic> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
