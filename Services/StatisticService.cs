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

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="statisticRepository">Объект, реализующий интерфейс IStatisticRepository.</param>
    public StatisticService(IStatisticRepository statisticRepository)
    {
        _statisticRepository = statisticRepository;
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
        catch (Exception)
        {
            Log.Warning("Статистика с Id {@ExternalId} не найдена", statisticForUpdatingDto.ExternalId);
            throw new StatisticNotFoundException(statisticForUpdatingDto.ExternalId);
        }

    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<StatisticDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var statistic = await _statisticRepository.GetAllAsync(cancellationToken);
        var statisticDto = statistic.Adapt<IReadOnlyCollection<StatisticDto>>();
        return statisticDto;
    }
}
