namespace Shared.DTOs
{
    public class PredictionNotificationDto
    {
        public string AvailabilityPrediction { get; set; } = string.Empty;

        public string PricingTrend { get; set; } = string.Empty;

        public string Recommendation { get; set; } = string.Empty;
    }
}