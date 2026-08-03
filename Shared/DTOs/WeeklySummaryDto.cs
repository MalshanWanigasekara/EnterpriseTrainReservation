namespace Shared.DTOs
{
    public class WeeklySummaryDto
    {
        public DateTime WeekStart { get; set; }

        public DateTime WeekEnd { get; set; }

        public int TotalBookings { get; set; }

        public int ConfirmedBookings { get; set; }

        public int CancelledBookings { get; set; }

        public decimal TotalSpent { get; set; }
    }
}