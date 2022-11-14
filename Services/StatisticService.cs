using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
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
    public async Task CreateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        statistic.UpdateDate = DateTime.Now;
        await _statisticRepository.CreateAsync(statistic, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        statistic.UpdateDate = DateTime.Now;
        await _statisticRepository.UpdateAsync(statistic, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var statistics = await _statisticRepository.GetAllAsync(cancellationToken);
        return statistics;
    }

    /// <inheritdoc />
    public async Task<Statistic> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            var statistic = await _statisticRepository.GetByIdAsync(id, cancellationToken);
            return statistic;
        }
        catch (InvalidOperationException e)
        {
            throw new StatisticNotFoundException(id);
        }

    }
}
