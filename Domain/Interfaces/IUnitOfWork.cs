using Domain.Interfaces.Repositories;

namespace Domain.Interfaces;

/// <summary>
/// Содержит метод для коммита транзакции.
/// </summary>
public interface IUnitOfWork : IDisposable
{
    /// <summary>
    /// Свойство для получения репозитория статистики.
    /// </summary>
    public IStatisticRepository StatisticRepository { get; }
    /// <summary>
    /// Свойство для получения репозитория событий.
    /// </summary>
    public IEventRepository EventRepository { get; }
    /// <summary>
    /// Сохранение транзакции.
    /// </summary>
    void Commit();
}
