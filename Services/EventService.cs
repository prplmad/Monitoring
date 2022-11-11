using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Services.Abstractions;
using Serilog;
using Mapster;

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
    public async Task<StatisticWithEventsDto> GetEventsByStatisticIdAsync(int statisticId, CancellationToken cancellationToken)
    {
        try
        {
            var statisticWithEvents = await _eventRepository.GetEventsByStatisticIdAsync(statisticId,cancellationToken);
            var statisticWithEventsDtos = statisticWithEvents.Adapt<StatisticWithEventsDto>();
            return statisticWithEventsDtos;
        }
        catch (InvalidOperationException)
        {
            _logger.Warning("Статистика с Id {@Id} не найдена", statisticId);
            throw new StatisticNotFoundException(statisticId);
        }

    }

    /// <inheritdoc/>
    public async Task CreateAsync(EventForCreationDto eventForCreationDto, CancellationToken cancellationToken)
    {
        try
        {
            var eventForCreation = eventForCreationDto.Adapt<Event>();
            var statisticExternalId = eventForCreationDto.StatisticId;
            await _eventRepository.CreateAsync(eventForCreation, cancellationToken);
        }
        catch (InvalidOperationException)
        {
            _logger.Warning("Статистика с Id {@ExternalId} не найдена", eventForCreationDto.StatisticId);
            throw new StatisticNotFoundException(eventForCreationDto.StatisticId);
        }
    }
}
