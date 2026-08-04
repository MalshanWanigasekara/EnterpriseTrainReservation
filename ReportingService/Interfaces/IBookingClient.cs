using Shared.DTOs;

namespace ReportingService.Interfaces
{
    public interface IBookingClient
    {
        // get all bookingsf for the user
        Task<List<BookingDto>> GetBookingsByUserAsync(string nic);
    }
}