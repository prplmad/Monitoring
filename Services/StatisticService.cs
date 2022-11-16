using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Serilog;
using Services.Abstractions;


namespace Services;

/// <inheritdoc/>
public class StatisticService : IStatisticService
{
    private readonly IValidator<Statistic> _validator;
    private readonly IStatisticRepository _statisticRepository;
    private readonly ILogger _logger;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="statisticRepository">Подключение репозитория.</param>
    /// <param name="logger">Подключение логгера.</param>
    /// <param name="validator">Валидатор статистики.</param>
    public StatisticService(IStatisticRepository statisticRepository, ILogger logger, IValidator<Statistic> validator)
    {
        _statisticRepository = statisticRepository;
        _logger = logger;
        _validator = validator;
    }

    /// <inheritdoc />
    public async Task CreateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        ValidationResult result = await _validator.ValidateAsync(statistic);
        if (!result.IsValid)
        {
            _logger.Error("Ошибка валидации {@Errors}", result.Errors.First());
            throw new ValidationException("Произошла ошибка валидации: " + result.Errors.First());
        }
        await _statisticRepository.CreateAsync(statistic, cancellationToken);
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Statistic statistic, CancellationToken cancellationToken = default)
    {
        ValidationResult result = await _validator.ValidateAsync(statistic);
        if (!result.IsValid)
        {
            _logger.Error("Ошибка валидации {@Errors}", result.Errors.First());
            throw new ValidationException("Произошла ошибка валидации: " + result.Errors.First());
        }
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
        catch (InvalidOperationException)
        {
            throw new StatisticNotFoundException(id);
        }

    }
}
