using System.Data;
using Domain.Interfaces;
using System.Data.Common;
using Domain.Interfaces.Repositories;
using Persistence.Connection;

namespace Persistence.UoW;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    public IStatisticRepository StatisticRepository { get; }
    public IEventRepository EventRepository { get; }
    private readonly IDbTransaction _dbTransaction;

    public UnitOfWork(IStatisticRepository statisticRepository, IEventRepository eventRepository, IDbTransaction dbTransaction)
    {
        StatisticRepository = statisticRepository;
        EventRepository = eventRepository;
        _dbTransaction = dbTransaction;
    }

    public void Commit()
    {
        try
        {
            _dbTransaction.Commit();
            _dbTransaction.Connection.BeginTransaction();
        }
        catch (Exception ex)
        {
            _dbTransaction.Rollback();
        }
    }

    public void Dispose()
    {
        _dbTransaction.Connection?.Close();
        _dbTransaction.Connection?.Dispose();
        _dbTransaction.Dispose();
    }
}
