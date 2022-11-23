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
public class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Флаг, показывающий было ли особождение ресурсов.
    /// </summary>
    private readonly ILogger _logger;
    private readonly IDbConnection _connection;
    private readonly IDbTransaction _transaction;
    /// <summary>
    /// Инициализация подключения и начало транзакции.
    /// </summary>
    /// <param name="connectionFactory">Создает соединения и транзакции.</param>
    /// <param name="logger">Подключение логгера.</param>
    public UnitOfWork(IConnectionFactory connectionFactory, ILogger logger)
    {
        _logger = logger;

        _connection = connectionFactory.CreateConnection();
        if (_connection == null)
        {
            _logger.Error("Ошибка при создании подключения к базе данных");
            throw new Exception("Ошибка при создании подключения к базе данных");
        }
        _connection.Open();
        _transaction = _connection.BeginTransaction();
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
            _connection.Close();
        }
        catch (Exception ex)
        {
            _logger.Error("{Message}", ex.Message);
            throw new TransactionException(ex.Message);
        }
    }

    /// <summary>
    /// Удаление объектов Connection и Transaction.
    /// </summary>
    public void Dispose()
    {
        if (_connection.State == ConnectionState.Open)
        {
            _transaction.Rollback();
        }
        _transaction.Dispose();
        _connection.Dispose();
    }
}
