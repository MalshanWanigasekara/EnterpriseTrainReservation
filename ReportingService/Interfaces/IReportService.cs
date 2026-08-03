using Shared.DTOs;

namespace ReportingService.Interfaces
{
    public interface IReportService
    {
        Task<WeeklySummaryDto> GetWeeklySummaryAsync(
            string nic,
            DateTime selectedDate);
    }
}