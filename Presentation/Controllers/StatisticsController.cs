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
public class StatisticsController : ControllerBase
{
    private readonly IStatisticsService _statisticsService;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="statisticsService">Подключение сервиса статистики.</param>
    public StatisticsController(IStatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

    /// <summary>
    /// Метод для получения статистики от мобильного приложения Connect.
    /// </summary>
    /// <param name="statisticsForCreationDto">ДТО для создания.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    public async Task<IActionResult> CreateStatisticsAsync([FromBody] StatisticsForCreationDto statisticsForCreationDto, CancellationToken cancellationToken = default)
    {
        Log.Information($"Получена статистика от мобильного приложения Connect");
        await _statisticsService.CreateAsync(statisticsForCreationDto, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Метод для обновления имеющейся статистики.
    /// </summary>
    /// <param name="statisticsForUpdatingDto">ДТО для обновления.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResult в ответ на запрос.</returns>
    [HttpPost]
    public async Task<IActionResult> UpdateStatisticsAsync([FromBody] StatisticsForUpdatingDto statisticsForUpdatingDto, CancellationToken cancellationToken = default)
    {
        await _statisticsService.UpdateAsync(statisticsForUpdatingDto, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Метод для получения всей имеющейся статистики.
    /// </summary>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает всю имеющуюся статистику в формате JSON.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAllStatisticsAsync(CancellationToken cancellationToken = default)
    {
        var statisticsDto = await _statisticsService.GetAllAsync(cancellationToken);
        return Ok(statisticsDto);
    }
}
