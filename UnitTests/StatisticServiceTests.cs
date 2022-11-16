using Domain.Entities;
using Domain.Exceptions;
using Moq;
using Domain.Interfaces.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Serilog;
using Services;
using Xunit;

namespace UnitTests;

/// <summary>
/// Тесты сервиса StatisticService.
/// </summary>
public class StatisticServiceTests
{
    private StatisticService _statisticService;
    private Mock<IStatisticRepository> _statisticRepository;
    private Mock<ILogger> _logger;
    private Mock<IValidator<Statistic>> _validator;
    private Statistic _statistic;

    /// <summary>
    /// Конструктор для инициализации объектов, необходимых для тестов сервиса StatisticService.
    /// </summary>
    public StatisticServiceTests()
    {
        _statisticRepository = new Mock<IStatisticRepository>();
        _logger = new Mock<ILogger>();
        _statistic = new Statistic();
        _validator = new Mock<IValidator<Statistic>>();
        _statisticService = new StatisticService(_statisticRepository.Object, _logger.Object, _validator.Object);
    }

    /// <summary>
    /// Ожидается, что метод UpdateAsync сгенерирует исключение StatisticNotFoundException при отсутствии записи в репозитории.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task UpdateAsync_RecordNotFoundInRepository_ThrowsStatisticNotFoundException()
    {
        // Arrange
        _statisticRepository.Setup(sr => sr.UpdateAsync(It.IsAny<Statistic>(), It.IsAny<CancellationToken>())).ThrowsAsync(new InvalidOperationException());
        _validator.Setup((v => v.ValidateAsync(It.IsAny<Statistic>(), It.IsAny<CancellationToken>()))).ReturnsAsync(new ValidationResult());
        // Act, Assert
        await Assert.ThrowsAsync<StatisticNotFoundException>( async () => await _statisticService.UpdateAsync(_statistic, It.IsAny<CancellationToken>()));
    }
}
