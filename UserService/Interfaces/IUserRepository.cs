using UserService.Entities;

namespace UserService.Interfaces
{
    public interface IUserRepository
    {
        //login function
        Task<UserEntity?> LoginAsync(string nic,string password);

        // get all User da
        Task<List<UserEntity>> GetAllAsync();
        Task<UserEntity?> GetByIdAsync(string nic);
        Task AddAsync(UserEntity user);
        Task SaveChangesAsync();
    }
}
