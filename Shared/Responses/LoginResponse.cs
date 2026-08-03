using Shared.DTOs;

namespace Shared.Responses
{
    public class LoginResponse
    {
        public bool Success { get; set; }

        public string Message { get; set; } = string.Empty;

        public UserDto? User { get; set; }
    }
}