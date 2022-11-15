using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using FluentValidation;
using Mapster;
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
    ///     POST /Create
    ///     {
    ///        "ExternalId":1,
    ///        "ClientVersion":"5.19",
    ///        "UserName":"Pavel Ivanov",
    ///        "OS":"Windows"
    ///     }.
    /// </remarks>
    /// <response code="201">Объект статистики успешно создан.</response>
    /// <response code="400">Не все параметры были заполнены или какие-то параметры были введены некорректно.</response>
    /// <param name="statisticForCreationRequest">ДТО для создания.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateAsync([FromBody] StatisticForCreationRequest statisticForCreationRequest, CancellationToken cancellationToken = default)
    {
        _logger.Debug("Получен запрос на добавление статистики мобильного приложения Connect {@StatisticForCreationRequest}", statisticForCreationRequest);
        try
        {
            var statistic = statisticForCreationRequest.Adapt<Statistic>();
            await _statisticService.CreateAsync(statistic, cancellationToken);
            return StatusCode(201);
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error("{Message}", e.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Метод для обновления имеющейся статистики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     PUT /Update
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
    /// <param name="statisticForUpdatingRequest">ДТО для обновления.</param>
    /// <param name="id">Id статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateAsync([FromBody] StatisticForUpdatingRequest statisticForUpdatingRequest, int id, CancellationToken cancellationToken = default)
    {
        try
        {
            _logger.Debug("Получен запрос на обновление статистики {@StatisticForUpdatingRequest}", statisticForUpdatingRequest);
            var statisticEntity = await _statisticService.GetByIdAsync(id, cancellationToken);
            var statistic = statisticForUpdatingRequest.Adapt<Statistic>();
            await _statisticService.UpdateAsync(statistic, cancellationToken);
            return Ok();
        }
        catch (ValidationException e)
        {
            return BadRequest(e.Message);
        }
        catch (StatisticNotFoundException e)
        {
            _logger.Error("Статистика с Id {@Id} не найдена", id);
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            _logger.Error("Что-то пошло не так внутри UpdateAsync Action {Message}", e.Message);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Метод для получения всей имеющейся статистики.
    /// </summary>
    /// <remarks>
    /// Пример запроса:
    ///
    ///     GET /GetAll
    ///     {
    ///
    ///     }.
    ///
    /// </remarks>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает всю имеющуюся статистику.</returns>
    [HttpGet]
    public async Task<IReadOnlyCollection<StatisticResponse>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.Information("Получен запрос на получение всей имеющейся статистики");
        var statistics = await _statisticService.GetAllAsync(cancellationToken);
        var statisticDtos = statistics.Adapt<IReadOnlyCollection<StatisticResponse>>();
        return statisticDtos;
    }
}
