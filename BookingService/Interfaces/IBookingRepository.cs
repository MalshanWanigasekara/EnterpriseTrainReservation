using BookingService.Entities;
using Shared.DTOs;

namespace BookingService.Interfaces
{
    public interface IBookingRepository
    {
        Task<List<Train>> GetAllTrainsAsync();

        Task<Train?> GetTrainByIdAsync(int trainId);

        Task<List<Request>> GetAllRequestsAsync();

        Task<List<Seat>> GetSeatsByTrainAsync(int trainId);

        Task<List<Seat>> GetAvailableSeatsAsync(
            int trainId,
            DateTime travelDate);

        Task<bool> IsSeatAvailableAsync(
            int trainId,
            int seatId,
            DateTime travelDate);

        Task<List<Booking>> GetBookingsByUserAsync(
            string userNic);

        Task<Booking?> GetBookingAsync(
            int bookingId);

        Task AddBookingAsync(
            Booking booking);

        Task UpdateBookingAsync(
            Booking booking);

        Task CancelBookingAsync(
            int bookingId);

        Task SaveChangesAsync();

        Task<TrainOccupancyDto> GetTrainOccupancyAsync(
    int trainId,
    DateTime travelDate);
    }
}