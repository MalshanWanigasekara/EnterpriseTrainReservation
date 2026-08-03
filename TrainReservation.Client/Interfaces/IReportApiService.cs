using Shared.DTOs;

namespace TrainReservation.Client.Interfaces
{
    public interface IReportApiService
    {
        Task<WeeklySummaryDto?> GetWeeklySummaryAsync(
    string nic,
    DateTime selectedDate);
    }
}