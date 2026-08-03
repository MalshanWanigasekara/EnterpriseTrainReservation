using Microsoft.AspNetCore.Mvc;
using ReportingService.Interfaces;

namespace ReportingService.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService service;

        public ReportController(IReportService service)
        {
            this.service = service;
        }


        [HttpGet("weekly/{nic}")]
        public async Task<IActionResult> WeeklySummary(
            string nic,
            [FromQuery] DateTime weekStart)
        {
            var report = await service.GetWeeklySummaryAsync(
                nic,
                weekStart);

            return Ok(report);
        }
    }
}