using System.ComponentModel.DataAnnotations;

namespace UserService.Entities
{
    public class UserEntity
    {
        [Key]
        public string NIC { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Mobile { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}