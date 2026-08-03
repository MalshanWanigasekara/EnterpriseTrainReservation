using Shared.DTOs;

namespace PredictionService.Interfaces
{
    public interface IPredictionService
    {
        Task<PredictionNotificationDto> PredictAsync(
            int trainId,
            DateTime travelDate);
    }
}