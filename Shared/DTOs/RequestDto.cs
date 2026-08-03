namespace Shared.DTOs
{
    public class RequestDto
    {
        public int RequestId { get; set; }

        public string Description { get; set; } = string.Empty;

        public decimal AdditionalPrice { get; set; }
    }
}