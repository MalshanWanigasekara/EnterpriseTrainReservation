using Shared.DTOs;
using Shared.Requests;

namespace BookingService.Interfaces
{
    public interface IBookingService
    {
        Task<List<TrainDto>> GetAllTrainsAsync();

        Task<List<RequestDto>> GetAllRequestsAsync();

        Task<List<SeatDto>> GetAvailableSeatsAsync(
            int trainId,
            DateTime travelDate);

        Task<List<BookingDto>> GetBookingsByUserAsync(
            string userNic);

        Task<BookingDto?> GetBookingAsync(
            int bookingId);

        Task<int> CreateBookingAsync(
            CreateBookingDto request);

        Task UpdateBookingAsync(
            int bookingId,
            CreateBookingDto request);

        Task CancelBookingAsync(
            int bookingId);

        Task<TrainOccupancyDto> GetTrainOccupancyAsync(
    int trainId,
    DateTime travelDate);
    }
}