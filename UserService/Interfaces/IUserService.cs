using Shared.DTOs;
using Shared.Requests;
using Shared.Responses;

namespace UserService.Interfaces
{
    public interface IUserService
    {
        Task<LoginResponse> LoginAsync(LoginRequest request);

        Task<List<UserDto>> GetAllUsersAsync();

        Task<UserDto> CreateUserAsync(RegisterRequest request);
    }
}