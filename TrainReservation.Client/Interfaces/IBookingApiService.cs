using Shared.DTOs;
using Shared.Requests;

namespace TrainReservation.Client.Interfaces
{
    public interface IBookingApiService
    {
        Task<List<TrainDto>?> GetAllTrainsAsync();

        Task<List<RequestDto>?> GetAllRequestsAsync();

        Task<List<SeatDto>?> GetAvailableSeatsAsync(
            int trainId,
            DateTime travelDate);

        Task<List<BookingDto>?> GetMyBookingsAsync(
            string nic);

        Task<BookingDto?> GetBookingAsync(
            int bookingId);

        Task<int?> CreateBookingAsync(
            CreateBookingDto request);

        Task UpdateBookingAsync(
            int bookingId,
            CreateBookingDto request);

        Task CancelBookingAsync(
            int bookingId);
    }
}