using Shared.DTOs;

namespace ReportingService.Interfaces
{
    public interface IBookingClient
    {
        Task<List<BookingDto>> GetBookingsByUserAsync(string nic);
    }
}