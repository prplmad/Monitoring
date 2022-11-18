using System.Data;
using Domain.Interfaces;
using System.Data.Common;
using Domain.Interfaces.Repositories;
using Persistence.Connection;

namespace Persistence.UoW;

public class UnitOfWork : IUnitOfWork
{
    public IStatisticRepository StatisticRepository { get; }
    public IEventRepository EventRepository { get; }
    private readonly IConnectionFactory _connectionFactory;

    public UnitOfWork(IStatisticRepository statisticRepository, IEventRepository eventRepository, IConnectionFactory connectionFactory)
    {
        StatisticRepository = statisticRepository;
        EventRepository = eventRepository;
        _connectionFactory = connectionFactory;
    }

    public DbTransaction Transaction => _connectionFactory.Transaction;

    public DbConnection Connection => _connectionFactory.Connection;

    public async Task CommitAsync()
    {
        try
        {
            Transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Transaction.RollbackAsync();
        }
    }
}
