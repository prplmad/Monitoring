using Domain.Interfaces;
using System.Data.Common;
using System.Transactions;
using Domain.Interfaces.Repositories;
using Persistence.Connection;

namespace Persistence.UoW;

/// <summary>
/// Содержит метод для коммита транзакции.
/// </summary>
public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly IConnectionFactory _connectionFactory;

    /// <summary>
    /// Получение репозиториев и connectionFactory.
    /// </summary>
    /// <param name="statisticRepository">Репозиторий со статистикой.</param>
    /// <param name="eventRepository">Репозиторий с событиями.</param>
    /// <param name="connectionFactory">Создает соединения и транзакции.</param>
    public UnitOfWork(IStatisticRepository statisticRepository, IEventRepository eventRepository, IConnectionFactory connectionFactory)
    {
        StatisticRepository = statisticRepository;
        EventRepository = eventRepository;
        _connectionFactory = connectionFactory;
    }

    /// <inheritdoc />
    public IStatisticRepository StatisticRepository { get; }

    /// <inheritdoc />
    public IEventRepository EventRepository { get; }


    /// <summary>
    /// Транзакция.
    /// </summary>
    public DbTransaction Transaction => _connectionFactory.Transaction;

    /// <summary>
    /// Подключение.
    /// </summary>
    public DbConnection Connection => _connectionFactory.Connection;

    /// <summary>
    /// Сохранение в БД.
    /// </summary>
    /// <exception cref="TransactionException">Ошибка при сохранении.</exception>
    /// <returns>Task.</returns>
    public async Task CommitAsync()
    {
        try
        {
            await Transaction.CommitAsync();
            Dispose();
        }
        catch (Exception ex)
        {
            await Transaction.RollbackAsync();
            Dispose();
            throw new TransactionException(ex.Message);
        }
    }

    /// <summary>
    /// Удаление объектов Connection и Transaction.
    /// </summary>
    public void Dispose()
    {
        Connection.Close();
        Connection.Dispose();
        if (Transaction != null)
        {
            Transaction.Dispose();
        }
    }
}
