// <copyright file="IStatisticsService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Services.Abstractions;

using Contracts;

public interface IStatisticsService<T>
{
    /// <summary>
    /// Получение всех элементов статистики.
    /// </summary>
    /// <param name="cancellationToken"> Токен для отмены задачи.</param>
    /// <returns>Возвращается коллекция с элементами статистики</returns>
    Task<IEnumerable<StatisticsDto<T>>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Создание статистики.
    /// </summary>
    /// <param name="statisticsForCreationDto">ДТО статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается Task.</returns>
    Task CreateAsync(StatisticsForCreationDto<T> statisticsForCreationDto, CancellationToken cancellationToken);

    /// <summary>
    /// Получение статистики по ExternalID.
    /// </summary>
    /// <param name="statisticsDto">ДТО статистики.</param>
    /// <param name="cancellationToken">Токен для отмены задачи.</param>
    /// <returns>Возвращается элемент статистики.</returns>
    Task<StatisticsDto<T>> GetByExternalIdAsync(StatisticsDto<T> statisticsDto, CancellationToken cancellationToken);
}