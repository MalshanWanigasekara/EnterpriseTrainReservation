using Shared.DTOs;

namespace PredictionService.Interfaces
{
    public interface IBookingClient
    {
        // obtaining current occupancy for prediction purposes
        Task<TrainOccupancyDto> GetTrainOccupancyAsync( int trainId, DateTime travelDate);
    }
}