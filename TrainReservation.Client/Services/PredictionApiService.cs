using Shared.DTOs;
using TrainReservation.Client.Interfaces;

namespace TrainReservation.Client.Services
{
    public class PredictionApiService : IPredictionApiService
    {
        private readonly IGatewayClient gateway;

        public PredictionApiService(
            IGatewayClient gateway)
        {
            this.gateway = gateway;
        }

        public async Task<PredictionNotificationDto?> GetPredictionAsync(
            int trainId,
            DateTime travelDate)
        {
            return await gateway.GetAsync<PredictionNotificationDto>(
                $"/predictions/api/predictions?trainId={trainId}&travelDate={travelDate:yyyy-MM-dd}");
        }
    }
}