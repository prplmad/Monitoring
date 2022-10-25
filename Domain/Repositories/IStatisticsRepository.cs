namespace Domain.Repositories;

using Domain.Entities;

/// <summary>
/// Содержит методы для операций со статистикой в репозитории.
/// </summary>
public interface IStatisticsRepository
{

    /// <summary>
    /// Метод для добавления новой статистики в коллецию.
    /// </summary>
    /// <param name="statistics">Объект статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает Task.</returns>
    Task CreateAsync(Statistics statistics, CancellationToken cancellationToken = default);

    /// <summary>
    /// Метод для обновления уже имеющейся статистики.
    /// </summary>
    /// <param name="statistics">Объект статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает Task.</returns>
    Task UpdateAsync(Statistics statistics, CancellationToken cancellationToken = default);

    /// <summary>
    /// Метод для получения всей имеющейся статистики.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает readonly коллекцию со всей имеющейся статистикой.</returns>
    Task<IReadOnlyCollection<Statistics>> GetAllAsync(CancellationToken cancellationToken = default);
}
