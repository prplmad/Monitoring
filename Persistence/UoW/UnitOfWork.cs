using System.Data;
using Domain.Interfaces;
using System.Transactions;
using Domain.Interfaces.Repositories;
using Persistence.Connection;
using Persistence.Repositories;
using Serilog;

namespace Persistence.UoW;

/// <summary>
/// Содержит метод для коммита транзакции.
/// </summary>
public class UnitOfWork : IUnitOfWork, IDisposable
{
    protected bool disposed;
    private readonly ILogger _logger;
    private readonly IConnectionFactory _connectionFactory;
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;

    /// <summary>
    /// Инициализация подключения и начало транзакции.
    /// </summary>
    /// <param name="connectionFactory">Создает соединения и транзакции.</param>
    /// <param name="logger">Подключение логгера.</param>
    public UnitOfWork(IConnectionFactory connectionFactory, ILogger logger)
    {
        _connectionFactory = connectionFactory;
        _logger = logger;

        _connection = _connectionFactory.CreateConnection();
        if (_connection == null)
        {
            _logger.Error("Ошибка при создании подключения к базе данных");
            throw new Exception("Ошибка при создании подключения к базе данных");
        }

        if (_connection.State != ConnectionState.Open)
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }
    }

    /// <inheritdoc />
    public IStatisticRepository StatisticRepository { get => new StatisticRepository(_connection, _transaction); }

    /// <inheritdoc />
    public IEventRepository EventRepository { get => new EventRepository(_connection, _transaction); }

    /// <summary>
    /// Сохранение в БД.
    /// </summary>
    /// <exception cref="TransactionException">Ошибка при сохранении.</exception>
    public void Commit()
    {
        try
        {
            _transaction.Commit();
        }
        catch (Exception ex)
        {
            _logger.Error("{Message}", ex.Message);
            _transaction.Rollback();
            throw new TransactionException(ex.Message);
        }
    }

    /// <summary>
    /// Удаление объектов Connection и Transaction.
    /// </summary>
    public void Dispose()
    {
        if (!disposed)
        {
            _transaction.Dispose();
            _connection.Dispose();
            disposed = true;
        }
    }
}
