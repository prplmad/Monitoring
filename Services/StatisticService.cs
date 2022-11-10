using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Serilog;
using Services.Abstractions;


namespace Services;

/// <inheritdoc/>
public class StatisticService : IStatisticService
{
    private readonly IStatisticRepository _statisticRepository;
    private readonly ILogger _logger;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="statisticRepository">Подключение репозитория.</param>
    /// <param name="logger">Подключение логгера.</param>
    public StatisticService(IStatisticRepository statisticRepository, ILogger logger)
    {
        _statisticRepository = statisticRepository;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task CreateAsync(StatisticForCreationDto statisticForCreationDto, CancellationToken cancellationToken = default)
    {
        var statistic = statisticForCreationDto.Adapt<Statistic>();
        statistic.UpdateDate = DateTime.Now;
        await _statisticRepository.CreateAsync(statistic, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(StatisticForUpdatingDto statisticForUpdatingDto, CancellationToken cancellationToken = default)
    {
        var statistic = statisticForUpdatingDto.Adapt<Statistic>();
        statistic.UpdateDate = DateTime.Now;
        try
        {
            await _statisticRepository.UpdateAsync(statistic, cancellationToken);
        }
        catch (InvalidOperationException)
        {
            _logger.Warning("Статистика с Id {@ExternalId} не найдена", statisticForUpdatingDto.ExternalId);
            throw new StatisticNotFoundException(statisticForUpdatingDto.ExternalId);
        }
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<StatisticDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var statistics = await _statisticRepository.GetAllAsync(cancellationToken);
        var statisticDtos = statistics.Adapt<IReadOnlyCollection<StatisticDto>>();
        return statisticDtos;
    }
}
