using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Moq;
using Domain.Repositories;
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
    private StatisticForUpdatingDto _statisticForUpdatingDto;

    /// <summary>
    /// Конструктор для инициализации объектов, необходимых для тестов сервиса StatisticService.
    /// </summary>
    public StatisticServiceTests()
    {
        _statisticRepository = new Mock<IStatisticRepository>();
        _logger = new Mock<ILogger>();
        _statisticService = new StatisticService(_statisticRepository.Object, _logger.Object);
        _statisticForUpdatingDto = new StatisticForUpdatingDto();
    }

    /// <summary>
    /// Ожидается, что метод UpdateAsync сгенерирует исключение StatisticNotFoundException при отсутствии записи в репозитории.
    /// </summary>
    /// <returns>Task.</returns>
    [Fact]
    public async Task UpdateAsync_RecordNotFoundInRepository_ThrowsStatisticNotFoundException()
    {
        // Arrange
        _statisticRepository.Setup(sr => sr.UpdateAsync(It.IsAny<Statistic>(), It.IsAny<CancellationToken>())).Throws(new InvalidOperationException());
        // Act, Assert
        await Assert.ThrowsAsync<StatisticNotFoundException>( async () => await _statisticService.UpdateAsync(_statisticForUpdatingDto, It.IsAny<CancellationToken>()));
    }
}
