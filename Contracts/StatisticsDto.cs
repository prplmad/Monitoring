﻿// <copyright file="StatisticsDto.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Contracts;

public class StatisticsDto<T>
{
    /// <summary>
    /// Gets or sets Gets or sets идентификатор статистики, полученный от мобильного приложения Connect.
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

    /// <summary>
    /// Gets or sets дата обновления статистики.
    /// </summary>
    public DateTime UpdateDate { get; set; }
}