using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Moq;
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
    private Mock<IUnitOfWork> _unitOfWork;
    private Mock<ILogger> _logger;
    private Mock<IValidator<Statistic>> _validator;
    private Statistic _statistic;

    /// <summary>
    /// Конструктор для инициализации объектов, необходимых для тестов сервиса StatisticService.
    /// </summary>
    public StatisticServiceTests()
    {
        _unitOfWork = new Mock<IUnitOfWork>();
        _logger = new Mock<ILogger>();
        _statistic = new Statistic();
        _validator = new Mock<IValidator<Statistic>>();
        _statisticService = new StatisticService(_logger.Object, _validator.Object, _unitOfWork.Object);
    }

    /// <summary>
    /// Ожидается, что метод UpdateAsync сгенерирует исключение StatisticNotFoundException при отсутствии записи в репозитории.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task GetByIdAsync_RecordNotFoundInRepository_ThrowsStatisticNotFoundException()
    {
        // Arrange
        _unitOfWork.Setup(uow => uow.StatisticRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>())).ThrowsAsync(new InvalidOperationException());
        _validator.Setup((v => v.ValidateAsync(It.IsAny<Statistic>(), It.IsAny<CancellationToken>()))).ReturnsAsync(new ValidationResult());
        // Act, Assert
        await Assert.ThrowsAsync<StatisticNotFoundException>( async () => await _statisticService.GetByIdAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()));
    }
}
