namespace BookingService.Entities
{
    public class BookingRequest
    {
        public int BookingId { get; set; }

        public Booking? Booking { get; set; }

        public int RequestId { get; set; }

        public Request? Request { get; set; }
    }
}