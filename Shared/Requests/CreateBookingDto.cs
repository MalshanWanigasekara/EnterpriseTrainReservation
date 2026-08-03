namespace Shared.Requests
{
    public class CreateBookingDto
    {
        public string UserNic { get; set; } = string.Empty;

        public int TrainId { get; set; }

        public DateTime TravelDate { get; set; }

        public bool IsRecurring { get; set; }

        public List<int> SeatIds { get; set; } = new();

        public List<int> RequestIds { get; set; } = new();
    }
}