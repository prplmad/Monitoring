using Domain.Entities;

namespace Domain.Interfaces.Repositories;

/// <summary>
/// Содержит методы для операций со статистикой в репозитории.
/// </summary>
public interface IStatisticRepository
{
    /// <summary>
    /// Метод для добавления новой статистики в коллецию.
    /// </summary>
    /// <param name="statistic">Объект статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает Task.</returns>
    Task CreateAsync(Statistic statistic, CancellationToken cancellationToken = default);

    /// <summary>
    /// Метод для обновления уже имеющейся статистики.
    /// </summary>
    /// <param name="statistic">Объект статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает Task.</returns>
    Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken = default);

    /// <summary>
    /// Метод для получения всей имеющейся статистики.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает readonly коллекцию со всей имеющейся статистикой.</returns>
    Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<Statistic> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
