using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using NUnit.Framework;
using Moq;
using Domain.Repositories;
using Serilog;
using Services;

namespace Tests;

[TestFixture]
public class StatisticServiceTests
{
    private StatisticService _statisticService;
    private Mock<IStatisticRepository> _statisticRepository;
    private Mock<ILogger> _logger;

    [SetUp]
    public void Setup()
    {
        _statisticRepository = new Mock<IStatisticRepository>();
        _logger = new Mock<ILogger>();
        _statisticService = new StatisticService(_statisticRepository.Object, _logger.Object);
    }

    [Test]
    public void UpdateAsync_StatisticRepositoryThrowsException()
    {
        // Arrange
        _statisticRepository.Setup(sr => sr.UpdateAsync(Moq.It.IsAny<Statistic>(), Moq.It.IsAny<CancellationToken>())).Throws(new Exception());
        // Act
        _statisticService.UpdateAsync(It.IsAny<StatisticForUpdatingDto>(), It.IsAny<CancellationToken>());
        // Assert
        Assert.Throws<StatisticNotFoundException>(() => { throw new StatisticNotFoundException(It.IsAny<int>()); });
    }
}
