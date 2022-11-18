using System.Data;
using Domain.Interfaces.Repositories;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    public IStatisticRepository StatisticRepository { get; }
    public IEventRepository EventRepository { get; }
    Task CommitAsync();
}
