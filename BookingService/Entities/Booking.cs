using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Entities
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }

        public DateTime TravelDate { get; set; }

        public bool IsRecurring { get; set; }

        [Required]
        [MaxLength(20)]
        public string BookingStatus { get; set; } = "CONFIRMED";

        public decimal TotalAmount { get; set; }

        [Required]
        public string UserNic { get; set; } = string.Empty;

        [ForeignKey(nameof(Train))]
        public int TrainId { get; set; }

        public Train? Train { get; set; }

        public ICollection<BookingSeat> BookingSeats { get; set; }
            = new List<BookingSeat>();

        public ICollection<BookingRequest> BookingRequests { get; set; }
            = new List<BookingRequest>();

        // Used for optimistic concurrency
        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}