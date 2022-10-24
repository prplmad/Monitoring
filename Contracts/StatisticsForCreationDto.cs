// <copyright file="StatisticsForCreationDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Contracts;

public class StatisticsForCreationDto<T>
{
    /// <summary>
    /// Gets or sets идентификатор статистики, полученный от мобильного приложения Connect.
    /// </summary>
    public T? ExternalId { get; set; }

    /// <summary>
    /// Gets or sets имя пользователя.
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// Gets or sets версия приложения.
    /// </summary>
    public string? ClientVersion { get; set; }

    /// <summary>
    /// Gets or sets операционная система.
    /// </summary>
    public string? Os { get; set; }
}