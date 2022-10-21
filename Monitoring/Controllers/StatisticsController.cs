using Microsoft.AspNetCore.Mvc;
using Monitoring.Models;

namespace Monitoring.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class StatisticsController : ControllerBase
{
    [HttpPost]
    public IActionResult GetStatisticsFromMobileApp([FromBody] StatisticsItem statistics)
    {
        //передача модели в BL, а затем в DAL для сохранения In Memory
        return Ok();
    }
}