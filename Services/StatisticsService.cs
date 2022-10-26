using Contracts;
using Domain.Entities;
using Domain.Repositories;
using Mapster;
using Services.Abstractions;

namespace Services;

/// <inheritdoc/>
public class StatisticsService : IStatisticsService
{
    private readonly IStatisticsRepository _statisticsRepository;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="statisticsRepository">Объект, реализующий интерфейс IStatisticsRepository.</param>
    public StatisticsService(IStatisticsRepository statisticsRepository)
    {
        _statisticsRepository = statisticsRepository;
    }

    /// <inheritdoc />
    public async Task CreateAsync(StatisticsForCreationDto statisticsForCreationDto, CancellationToken cancellationToken = default)
    {
        var statistics = statisticsForCreationDto.Adapt<Statistics>();
        statistics.UpdateDate = DateTime.Now;
        await _statisticsRepository.CreateAsync(statistics, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(StatisticsForUpdatingDto statisticsForUpdatingDto, CancellationToken cancellationToken = default)
    {
        var statistics = statisticsForUpdatingDto.Adapt<Statistics>();
        statistics.UpdateDate = DateTime.Now;
        await _statisticsRepository.UpdateAsync(statistics, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<StatisticsDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var statistics = await _statisticsRepository.GetAllAsync(cancellationToken);
        var statisticsDto = statistics.Adapt<IReadOnlyCollection<StatisticsDto>>();
        return statisticsDto;
    }
}
