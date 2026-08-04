using Shared.DTOs;

namespace ReportingService.Interfaces
{
    public interface IReportService
    {
        // weekly report given start date
        Task<WeeklySummaryDto> GetWeeklySummaryAsync(
            string nic,
            DateTime selectedDate);
    }
}