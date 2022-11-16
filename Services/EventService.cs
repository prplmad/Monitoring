using Domain.Entities;
using Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Services.Abstractions;
using Serilog;

namespace Services;

/// <inheritdoc />
public class EventService : IEventService
{
    private readonly IValidator<Event> _validator;
    private readonly ILogger _logger;
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="logger">Подключение логирования.</param>
    /// <param name="eventRepository">Подключение репозитория.</param>
    /// <param name="validator">Валидатор Event.</param>
    public EventService(ILogger logger, IEventRepository eventRepository, IValidator<Event> validator)
    {
        _logger = logger;
        _eventRepository = eventRepository;
        _validator = validator;
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
        ValidationResult result = await _validator.ValidateAsync(eventForCreation);
        if (!result.IsValid)
        {
            _logger.Error("Ошибка валидации {@Errors}", result.Errors.First());
            throw new ValidationException("Произошла ошибка валидации: " + result.Errors.First());
        }
        await _eventRepository.CreateAsync(eventForCreation, cancellationToken);
    }
}
