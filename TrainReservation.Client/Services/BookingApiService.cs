using Shared.DTOs;
using Shared.Requests;
using TrainReservation.Client.Interfaces;

namespace TrainReservation.Client.Services
{
    public class BookingApiService : IBookingApiService
    {
        private readonly IGatewayClient gateway;

        public BookingApiService(
            IGatewayClient gateway)
        {
            this.gateway = gateway;
        }

        public async Task<List<TrainDto>?> GetAllTrainsAsync()
        {
            return await gateway.GetAsync<List<TrainDto>>(
                "/bookings/api/booking/trains");
        }

        public async Task<List<RequestDto>?> GetAllRequestsAsync()
        {
            return await gateway.GetAsync<List<RequestDto>>(
                "/bookings/api/booking/requests");
        }

        public async Task<List<SeatDto>?> GetAvailableSeatsAsync(
            int trainId,
            DateTime travelDate)
        {
            return await gateway.GetAsync<List<SeatDto>>(
                $"/bookings/api/booking/seats?trainId={trainId}&travelDate={travelDate:yyyy-MM-dd}");
        }

        public async Task<List<BookingDto>?> GetMyBookingsAsync(
            string nic)
        {
            return await gateway.GetAsync<List<BookingDto>>(
                $"/bookings/api/booking/user/{nic}");
        }

        public async Task<BookingDto?> GetBookingAsync(
            int bookingId)
        {
            return await gateway.GetAsync<BookingDto>(
                $"/bookings/api/booking/{bookingId}");
        }

        public async Task<int?> CreateBookingAsync(
            CreateBookingDto request)
        {
            return await gateway.PostAsync<CreateBookingDto, int>(
                "/bookings/api/booking",
                request);
        }

        public async Task UpdateBookingAsync(
            int bookingId,
            CreateBookingDto request)
        {
            await gateway.PutAsync(
                $"/bookings/api/booking/{bookingId}",
                request);
        }

        public async Task CancelBookingAsync(
            int bookingId)
        {
            await gateway.DeleteAsync(
                $"/bookings/api/booking/{bookingId}");
        }
    }
}