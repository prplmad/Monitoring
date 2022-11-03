using Contracts;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Services.Abstractions;

namespace Presentation.Controllers;

/// <summary>
/// Контроллер, обрабатывающий запросы на создание/обновление статистики.
/// </summary>
[ApiController]
[Route("api/[controller]/[action]")]
public class StatisticController : ControllerBase
{
    private readonly IStatisticService _statisticService;
    private readonly ILogger _logger;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="statisticService">Подключение сервиса статистики.</param>
    /// <param name="logger">Подключение логирования.</param>
    public StatisticController(IStatisticService statisticService, ILogger logger)
    {
        _statisticService = statisticService;
        _logger = logger;
    }

    /// <summary>
    /// Метод для получения статистики от мобильного приложения Connect.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /CreateStatistic
    ///     {
    ///        "ExternalId":1,
    ///        "ClientVersion":"5.19",
    ///        "UserName":"Pavel Ivanov",
    ///        "OS":"Windows"
    ///     }.
    ///
    /// </remarks>
    /// <response code="201">Объект статистики успешно создан.</response>
    /// <response code="400">Не все параметры были заполнены или какие-то параметры были введены некорректно.</response>
    /// <param name="statisticForCreationDto">ДТО для создания.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateStatisticAsync([FromBody] StatisticForCreationDto statisticForCreationDto, CancellationToken cancellationToken = default)
    {
        _logger.Debug("Получен запрос на добавление статистики мобильного приложения Connect {@StatisticForCreationDto}", statisticForCreationDto);
        await _statisticService.CreateAsync(statisticForCreationDto, cancellationToken);
        return StatusCode(201);
    }

    /// <summary>
    /// Метод для обновления имеющейся статистики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     POST /UpdateStatistic
    ///     {
    ///        "ExternalId":1,
    ///        "ClientVersion":"5.20",
    ///        "UserName":"Pavel Ivanov",
    ///        "OS":"Android"
    ///     }.
    ///
    /// </remarks>
    /// <response code="200">Объект статистики успешно обновлен.</response>
    /// <response code="400">Не все параметры были заполнены или какие-то параметры были введены некорректно.</response>
    /// <response code="404">Объект статистики не найден.</response>
    /// <response code="500">Ошибка сервера.</response>
    /// <param name="statisticForUpdatingDto">ДТО для обновления.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateStatisticAsync([FromBody] StatisticForUpdatingDto statisticForUpdatingDto, CancellationToken cancellationToken = default)
    {
        _logger.Debug("Получен запрос на обновление статистики {@StatisticForUpdatingDto}", statisticForUpdatingDto);
        try
        {
            await _statisticService.UpdateAsync(statisticForUpdatingDto, cancellationToken);
            return StatusCode(200);
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

    /// <summary>
    /// Метод для получения всей имеющейся статистики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET /GetAllStatistics
    ///     {
    ///
    ///     }.
    ///
    /// </remarks>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает всю имеющуюся статистику в формате JSON.</returns>
    [HttpGet]
    public async Task<IReadOnlyCollection<StatisticDto>> GetAllStatisticsAsync(CancellationToken cancellationToken = default)
    {
        Log.Information("Получен запрос на получение всей имеющейся статистики");
        var statisticDto = await _statisticService.GetAllAsync(cancellationToken);
        return statisticDto;
    }
}
