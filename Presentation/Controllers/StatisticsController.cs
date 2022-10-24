using Contracts;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace Presentation.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StatisticsController<T>  : ControllerBase 
{
    private readonly IGetStatisticsFromMobileAppService<T> _getStatisticsFromMobileAppService;
    
    public StatisticsController(IGetStatisticsFromMobileAppService<T> getStatisticsFromMobileAppService)
    {
        _getStatisticsFromMobileAppService = getStatisticsFromMobileAppService;
    }
    
    [HttpPost]
    public async Task<IActionResult> GetStatisticsFromMobileAppAsync([FromBody] StatisticsDto<T> statisticsDto)
    {
        await _getStatisticsFromMobileAppService.GetStatisticsFromMobileAppAsync(statisticsDto);
        return Ok();
    }
}