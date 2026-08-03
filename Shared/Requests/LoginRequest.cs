namespace Shared.Requests
{
    public class LoginRequest
    {
        public string Nic { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}