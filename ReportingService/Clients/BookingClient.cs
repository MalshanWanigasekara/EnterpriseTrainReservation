using Newtonsoft.Json;
using ReportingService.Interfaces;
using Shared.DTOs;

namespace ReportingService.Clients
{
    public class BookingClient : IBookingClient
    {
        private readonly HttpClient httpClient;

        public BookingClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<List<BookingDto>> GetBookingsByUserAsync(
            string nic)
        {
            var response = await httpClient.GetAsync( $"https://train-booking-service-cmgpdkaze6bgd7cu.southeastasia-01.azurewebsites.net/api/booking/user/{nic}");

            response.EnsureSuccessStatusCode();

            var json =  await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<List<BookingDto>>(json)
                   ?? new List<BookingDto>();
        }
    }
}