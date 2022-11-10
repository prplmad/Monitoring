using Contracts;

namespace Services.Abstractions;

public interface IEventService
{
    Task<IReadOnlyCollection<EventDto>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken);
}
