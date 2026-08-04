using Microsoft.AspNetCore.Mvc;
using ReportingService.Interfaces;

namespace ReportingService.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService reporttinService;

        public ReportController(IReportService service)
        {
            this.reporttinService = service;
        }


        [HttpGet("weekly/{nic}")]
        public async Task<IActionResult> WeeklySummary(
            string nic,
            [FromQuery] DateTime weekStart)
        {
            var report = await reporttinService.GetWeeklySummaryAsync(
                nic,
                weekStart);

            return Ok(report);
        }
    }
}