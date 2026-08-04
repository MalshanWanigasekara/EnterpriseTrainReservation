using Shared.DTOs;

namespace PredictionService.Interfaces
{
    public interface IPredictionService
    {
        // non blocking predic funcntion
        Task<PredictionNotificationDto> PredictAsync( int trainId,  DateTime travelDate);
    }
}