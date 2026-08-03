namespace Shared.DTOs
{
    public class TrainOccupancyDto
    {
        public int TrainId { get; set; }

        public string TrainNumber { get; set; } = string.Empty;

        public DateTime TravelDate { get; set; }

        public int TotalSeats { get; set; }

        public int BookedSeats { get; set; }

        public decimal CurrentTicketPrice { get; set; }

        public double OccupancyPercentage =>
            TotalSeats == 0
                ? 0
                : (BookedSeats * 100.0) / TotalSeats;
    }
}