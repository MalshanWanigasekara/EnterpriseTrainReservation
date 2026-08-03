using PredictionService.Interfaces;
using Shared.DTOs;

namespace PredictionService.Services
{
    public class PredictionServiceImpl : IPredictionService
    {
        private readonly IBookingClient bookingClient;

        public PredictionServiceImpl(IBookingClient bookingClient)
        {
            this.bookingClient = bookingClient;
        }

        public async Task<PredictionNotificationDto> PredictAsync(
            int trainId,
            DateTime travelDate)
        {
            TrainOccupancyDto occupancy =
                await bookingClient.GetTrainOccupancyAsync(
                    trainId,
                    travelDate);

            PredictionNotificationDto prediction = new();

            if (occupancy.OccupancyPercentage <= 40)
            {
                prediction.AvailabilityPrediction =
                    "HIGH Availability";

                prediction.PricingTrend =
                    $"Current fare (RM {occupancy.CurrentTicketPrice:F2}) is expected to remain stable.";

                prediction.Recommendation =
                    "Seats are widely available. You can book at your convenience.";
            }
            else if (occupancy.OccupancyPercentage <= 70)
            {
                prediction.AvailabilityPrediction =
                    "MODERATE Availability";

                prediction.PricingTrend =
                    $"Demand is increasing. The current fare (RM {occupancy.CurrentTicketPrice:F2}) may increase soon.";

                prediction.Recommendation =
                    "Consider booking within the next few days.";
            }
            else if (occupancy.OccupancyPercentage <= 90)
            {
                prediction.AvailabilityPrediction =
                    "LOW Availability";

                prediction.PricingTrend =
                    $"High demand detected. The current fare (RM {occupancy.CurrentTicketPrice:F2}) is likely to increase.";

                prediction.Recommendation =
                    "Book today to secure your seat and current fare.";
            }
            else
            {
                prediction.AvailabilityPrediction =
                    "VERY LOW Availability";

                prediction.PricingTrend =
                    $"Train is almost full. The current fare (RM {occupancy.CurrentTicketPrice:F2}) is expected to increase significantly.";

                prediction.Recommendation =
                    "Book immediately. Seats are almost sold out.";
            }

            return prediction;
        }
    }
}