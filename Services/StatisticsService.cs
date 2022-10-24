// <copyright file="StatisticsService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Services;

using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

public class StatisticsService<T> : IStatisticsService<T>
{
    private readonly IStatisticsInMemoryRepository<T> statisticsInMemoryRepository;

    public StatisticsService(IStatisticsInMemoryRepository<T> statisticsInMemoryRepository)
    {
        this.statisticsInMemoryRepository = statisticsInMemoryRepository;
    }

    /// <inheritdoc/>
    public Task<IEnumerable<StatisticsDto<T>>> GetAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public async Task CreateAsync(StatisticsForCreationDto<T> statisticsForCreationDto, CancellationToken cancellationToken)
    {
        var statistics = statisticsForCreationDto.Adapt<Statistics<T>>();
        await this.statisticsInMemoryRepository.Create(statistics);
    }

    /// <inheritdoc/>
    public Task<StatisticsDto<T>> GetByExternalIdAsync(StatisticsDto<T> statistics, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}