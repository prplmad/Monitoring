using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstractions;

namespace Presentation.Controllers;

/// <summary>
/// Контроллер, обрабатывающий запросы по событиям.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class EventController : ControllerBase
{
    private readonly ILogger _logger;
    private readonly IEventService _eventService;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="logger">Подключение логирования.</param>
    /// <param name="eventService">Подключение сервиса событий.</param>
    public EventController(ILogger logger, IEventService eventService)
    {
        _eventService = eventService;
        _logger = logger;
    }

    /// <summary>
    /// Метод для получения событий по Id статистики.
    /// </summary>
    /// <param name="statisticId">Id статистики<see cref="Statistic"/>.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Коллекция событий одной статистики.</returns>
    [HttpGet]
    public async Task<StatisticWithEventsDto> GetEventsByStatisticIdAsync(int statisticId,CancellationToken cancellationToken)
    {
        Log.Information("Получен запрос на получение событий статистики");
        var statisticWithEventsDto = await _eventService.GetEventsByStatisticIdAsync(statisticId, cancellationToken);
        return statisticWithEventsDto;
    }

    /// <summary>
    /// Метод для создания события.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /Create
    ///     {
    ///         "statisticExternalId": 1,
    ///         "name": "start",
    ///         "date": "2022-11-11T10:15:42.930Z"
    ///     }.
    /// </remarks>
    /// <param name="eventForCreationDto">ДТО события для создания.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync ([FromBody] EventForCreationDto eventForCreationDto,CancellationToken cancellationToken)
    {
        try
        {
            Log.Information("Получен запрос на добавление события");
            await _eventService.CreateAsync(eventForCreationDto, cancellationToken);
            return StatusCode(201);
        }
        catch (StatisticNotFoundException)
        {
            return StatusCode(404);
        }
        catch (Exception e)
        {
            _logger.Error("{Message}", e.Message);
            return StatusCode(500);
        }
    }
}
