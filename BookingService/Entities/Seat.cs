using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingService.Entities
{
    public class Seat
    {
        [Key]
        public int SeatId { get; set; }

        [Required]
        [MaxLength(10)]
        public string SeatNumber { get; set; } = string.Empty;

        [ForeignKey(nameof(Train))]
        public int TrainId { get; set; }

        public Train? Train { get; set; }

        public ICollection<BookingSeat> BookingSeats { get; set; }
            = new List<BookingSeat>();
    }
}