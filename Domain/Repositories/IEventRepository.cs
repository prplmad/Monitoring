using Domain.Entities;

namespace Domain.Repositories;

public interface IEventRepository
{
    Task<IReadOnlyCollection<Event>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken = default);
}
