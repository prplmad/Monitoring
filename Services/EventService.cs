using Contracts;
using Domain.Repositories;
using Services.Abstractions;
using Serilog;
using Mapster;

namespace Services;

public class EventService : IEventService
{
    private readonly ILogger _logger;
    private readonly IEventRepository _eventRepository;

    public EventService(ILogger logger, IEventRepository eventRepository)
    {
        _logger = logger;
        _eventRepository = eventRepository;
    }

    public async Task<IReadOnlyCollection<EventDto>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetEventsByStatisticIdAsync(statisticId,cancellationToken);
        var eventDtos = events.Adapt<IReadOnlyCollection<EventDto>>();
        return eventDtos;
    }
}
