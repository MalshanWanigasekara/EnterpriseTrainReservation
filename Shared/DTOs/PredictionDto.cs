namespace Shared.DTOs
{
    public class PredictionDto
    {
        public string RiskLevel { get; set; } = string.Empty;

        public string Reason { get; set; } = string.Empty;

        public double Score { get; set; }
    }
}