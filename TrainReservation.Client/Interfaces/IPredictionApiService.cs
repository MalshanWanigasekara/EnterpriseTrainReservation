using Shared.DTOs;

namespace TrainReservation.Client.Interfaces
{
    public interface IPredictionApiService
    {
        Task<PredictionNotificationDto?> GetPredictionAsync(
            int trainId,
            DateTime travelDate);
    }
}