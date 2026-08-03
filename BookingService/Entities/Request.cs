using System.ComponentModel.DataAnnotations;

namespace BookingService.Entities
{
    public class Request
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; } = string.Empty;

        public decimal AdditionalPrice { get; set; }

        public ICollection<BookingRequest> BookingRequests { get; set; }
            = new List<BookingRequest>();
    }
}