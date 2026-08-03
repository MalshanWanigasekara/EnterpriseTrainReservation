using Newtonsoft.Json;
using PredictionService.Interfaces;
using Shared.DTOs;

namespace PredictionService.Clients
{
    public class BookingClient : IBookingClient
    {
        private readonly HttpClient httpClient;

        public BookingClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;

            // Change this port
            httpClient.BaseAddress =
                new Uri("http://localhost:5004");
        }

        public async Task<TrainOccupancyDto> GetTrainOccupancyAsync(
            int trainId,
            DateTime travelDate)
        {
            var response = await httpClient.GetAsync(
                $"api/booking/occupancy?trainId={trainId}&travelDate={travelDate:yyyy-MM-dd}");

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TrainOccupancyDto>(json)!;
        }
    }
}