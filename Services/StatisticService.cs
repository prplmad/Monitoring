using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Serilog;
using Services.Abstractions;


namespace Services;

/// <inheritdoc/>
public class StatisticService : IStatisticService
{
    private readonly IValidator<Statistic> _validator;
    private readonly ILogger _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Конструктор для подключения сервисов.
    /// </summary>
    /// <param name="logger">Подключение логгера.</param>
    /// <param name="validator">Валидатор статистики.</param>
    /// <param name="unitOfWork">Подключение UnitOfWork.</param>
    public StatisticService(ILogger logger, IValidator<Statistic> validator, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _validator = validator;
        _unitOfWork = unitOfWork;
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
        using (_unitOfWork)
        {
            await _unitOfWork.StatisticRepository.CreateAsync(statistic, cancellationToken);
            _unitOfWork.Commit();
        }
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
        using (_unitOfWork)
        {
            await _unitOfWork.StatisticRepository.UpdateAsync(statistic, cancellationToken);
            _unitOfWork.Commit();
        }

    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<Statistic>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        using (_unitOfWork)
        {
            var statistics = await _unitOfWork.StatisticRepository.GetAllAsync(cancellationToken);
            return statistics;
        }
    }

    /// <inheritdoc />
    public async Task<Statistic> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        try
        {
            using (_unitOfWork)
            {
                var statistic = await _unitOfWork.StatisticRepository.GetByIdAsync(id, cancellationToken);
                return statistic;
            }
        }
        catch (InvalidOperationException)
        {
            throw new StatisticNotFoundException(id);
        }

    }
}
