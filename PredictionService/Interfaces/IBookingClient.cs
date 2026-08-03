using Shared.DTOs;

namespace PredictionService.Interfaces
{
    public interface IBookingClient
    {
        Task<TrainOccupancyDto> GetTrainOccupancyAsync(
            int trainId,
            DateTime travelDate);
    }
}