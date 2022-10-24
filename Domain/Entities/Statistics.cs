// <copyright file="StatisticsEntity.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Domain.Entities;

public class Statistics<T>
{
    public T? ExternalId { get; set; }

    public string? UserName { get; set; }

    public string? ClientVersion { get; set; }

    public string? Os { get; set; }

    public DateTime UpdateDate { get; set; }
}