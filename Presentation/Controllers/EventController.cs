using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class EventController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IEventService _eventService;

    public EventController(ILogger logger, IEventService eventService)
    {
        _eventService = eventService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IReadOnlyCollection<EventDto>> GetEventsByStatisticIdAsync(int statisticId,CancellationToken cancellationToken)
    {
        Log.Information("Получен запрос на получение событий статистики.");
        var eventDtos = await _eventService.GetEventsByStatisticIdAsync(statisticId, cancellationToken);
        return eventDtos;
    }
}
