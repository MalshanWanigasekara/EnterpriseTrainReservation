using System.ComponentModel.DataAnnotations;

namespace BookingService.Entities
{
    public class Train
    {
        [Key]
        public int TrainId { get; set; }

        [Required]
        [MaxLength(20)]
        public string TrainNumber { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string StartStation { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string EndStation { get; set; } = string.Empty;

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public decimal BaseTicketPrice { get; set; }

        public int TotalSeatCount { get; set; }

        public ICollection<Seat> Seats { get; set; }
            = new List<Seat>();

        public ICollection<Booking> Bookings { get; set; }
            = new List<Booking>();
    }
}