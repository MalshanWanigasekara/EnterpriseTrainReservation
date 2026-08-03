using Shared.DTOs;
using Shared.Requests;
using Shared.Responses;

namespace TrainReservation.Client.Interfaces
{
    public interface IAuthenticationService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);

        Task<UserDto?> RegisterAsync(RegisterRequest request);

        void Logout();

        bool IsLoggedIn();

        string? GetLoggedInNic();
    }
}