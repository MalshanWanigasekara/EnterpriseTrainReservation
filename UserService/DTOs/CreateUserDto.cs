namespace UserService.DTOs
{
    public class CreateUserDto
    {
        public string NIC { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Mobile { get; set; } = string.Empty;
    }
}
