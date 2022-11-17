using Domain.Interfaces.Repositories;

namespace Domain.Interfaces;

public interface IUnitOfWork
{
    IStatisticRepository StatisticRepository { get; }
    IEventRepository EventRepository { get; }
    void Commit();

    void Dispose();
}
