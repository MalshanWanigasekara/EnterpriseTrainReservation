using Shared.DTOs;
using TrainReservation.Client.Interfaces;

namespace TrainReservation.Client.Services
{
    public class ReportApiService : IReportApiService
    {
        private readonly IGatewayClient gateway;

        public ReportApiService(
            IGatewayClient gateway)
        {
            this.gateway = gateway;
        }

        public async Task<WeeklySummaryDto?> GetWeeklySummaryAsync(
    string nic,
    DateTime selectedDate)
        {
            return await gateway.GetAsync<WeeklySummaryDto>(
                $"/reports/api/report/weekly/{nic}?weekStart={selectedDate:yyyy-MM-dd}");
        }
    }
}