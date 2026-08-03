namespace Shared.DTOs
{
    public class BookingSummaryDto
    {
        public int BookingId { get; set; }

        public DateTime TravelDate { get; set; }

        public string BookingStatus { get; set; } = string.Empty;

        public bool IsRecurring { get; set; }

        public decimal TotalAmount { get; set; }

        public string TrainNumber { get; set; } = string.Empty;
    }
}