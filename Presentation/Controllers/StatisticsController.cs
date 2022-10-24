// <copyright file="StatisticsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Presentation.Controllers;

using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

    /// <summary>
    /// Контроллер, обрабатывающий запросы на создание/обновление статистики
    /// </summary>
    /// <typeparam name="T">Тип идентификатора.</typeparam>
[ApiController]
[Route("api/[controller]/[action]")]
public class StatisticsController<T> : ControllerBase
{
    private readonly IStatisticsService<T> statisticsService;

    /// <summary>
    /// Initializes a new instance of the <see cref="StatisticsController{T}"/> class.
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="statisticsService">Подключение сервиса статистики.</param>
    public StatisticsController(IStatisticsService<T> statisticsService)
    {
        this.statisticsService = statisticsService;
    }

    /// <summary>
    /// Метод для получения статистики от мобильного приложения Connect.
    /// </summary>
    /// <param name="statisticsForCreationDto">ДТО для создания.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращает IActionResul в ответ на запрос.</returns>
    [HttpPost]
    public async Task<IActionResult> GetStatisticsFromMobileAppAsync([FromBody] StatisticsForCreationDto<T> statisticsForCreationDto, CancellationToken cancellationToken = default)
    {
        await this.statisticsService.CreateAsync(statisticsForCreationDto, cancellationToken);
        return this.Ok();
    }
}