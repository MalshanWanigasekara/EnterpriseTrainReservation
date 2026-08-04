using Shared.DTOs;
using Shared.Requests;
using Shared.Responses;
using UserService.Entities;
using UserService.Interfaces;

namespace UserService.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserServiceImpl(IUserRepository repository)
        {
            this.userRepository = repository;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            var user = await userRepository.LoginAsync(
                request.Nic,
                request.Password);

            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid NIC or Password"
                };
            }

            return new LoginResponse
            {
                Success = true,
                Message = "Login Successful",
                User = new UserDto
                {
                    NIC = user.NIC,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Mobile = user.Mobile
                }
            };
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            var users = await userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                NIC = u.NIC,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Mobile = u.Mobile
            }).ToList();
        }

        public async Task<UserDto> CreateUserAsync(RegisterRequest request)
        {
            var entity = new UserEntity
            {
                NIC = request.Nic,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Mobile = request.PhoneNumber,
                Password = request.Password
            };

            await userRepository.AddAsync(entity);
            await userRepository.SaveChangesAsync();

            return new UserDto
            {
                NIC = entity.NIC,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Mobile = entity.Mobile
            };
        }
    }
}