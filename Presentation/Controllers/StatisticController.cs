using Contracts;
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
    /// <param name="statisticForCreationDto">ДТО для создания.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateStatisticAsync([FromBody] StatisticForCreationDto statisticForCreationDto, CancellationToken cancellationToken = default)
    {
        _logger.Debug("Получен запрос на добавление статистики мобильного приложения Connect {@StatisticForCreationDto}", statisticForCreationDto);
        await _statisticService.CreateAsync(statisticForCreationDto, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Метод для обновления имеющейся статистики.
    /// </summary>
    /// <param name="statisticForUpdatingDto">ДТО для обновления.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateStatisticAsync([FromBody] StatisticForUpdatingDto statisticForUpdatingDto, CancellationToken cancellationToken = default)
    {
        _logger.Debug("Получен запрос на обновление статистики {@StatisticForUpdatingDto}", statisticForUpdatingDto);
        await _statisticService.UpdateAsync(statisticForUpdatingDto, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Метод для получения всей имеющейся статистики.
    /// </summary>
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
