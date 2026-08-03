using UserService.Entities;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        Task<UserEntity?> LoginAsync(
    string nic,
    string password);

        Task<List<UserEntity>> GetAllAsync();

        Task<UserEntity?> GetByIdAsync(string nic);

        Task AddAsync(UserEntity user);

        Task SaveChangesAsync();
    }
}
