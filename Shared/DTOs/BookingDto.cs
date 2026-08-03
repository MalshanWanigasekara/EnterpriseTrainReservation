namespace Shared.DTOs
{
    public class BookingDto
    {
        public int BookingId { get; set; }

        public DateTime TravelDate { get; set; }

        public bool IsRecurring { get; set; }

        public string BookingStatus { get; set; } = string.Empty;

        public decimal TotalAmount { get; set; }

        public TrainDto? Train { get; set; }

        public List<SeatDto> Seats { get; set; } = new();

        public List<RequestDto> Requests { get; set; } = new();
    }
}