using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstractions;
using Serilog;

namespace Services;

/// <inheritdoc />
public class EventService : IEventService
{
    private readonly ILogger _logger;
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="logger">Подключение логирования.</param>
    /// <param name="eventRepository">Подключение репозитория.</param>
    public EventService(ILogger logger, IEventRepository eventRepository)
    {
        _logger = logger;
        _eventRepository = eventRepository;
    }

    /// <inheritdoc/>
    public async Task<IReadOnlyCollection<Event>> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken)
    {
        var events = await _eventRepository.GetEventsByStatisticIdAsync(statisticId,cancellationToken);
        return events;
    }

    /// <inheritdoc/>
    public async Task CreateAsync(Event eventForCreation, CancellationToken cancellationToken)
    {
        await _eventRepository.CreateAsync(eventForCreation, cancellationToken);
    }
}
