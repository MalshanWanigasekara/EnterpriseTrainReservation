namespace Shared.DTOs
{
    public class TrainDto
    {
        public int TrainId { get; set; }

        public string TrainNumber { get; set; } = string.Empty;

        public string StartStation { get; set; } = string.Empty;

        public string EndStation { get; set; } = string.Empty;

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public decimal BaseTicketPrice { get; set; }

        public int TotalSeatCount { get; set; }
    }
}