using Microsoft.EntityFrameworkCore;

using UserService.Data;
using UserService.Entities;
using UserService.Interfaces;

namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<UserEntity?> LoginAsync(
    string nic,
    string password)
        {
            return await context.Users.FirstOrDefaultAsync(
                u => u.NIC == nic &&
                     u.Password == password);
        }

        public async Task<List<UserEntity>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<UserEntity?> GetByIdAsync(string nic)
        {
            return await context.Users.FindAsync(nic);
        }

        public async Task AddAsync(UserEntity user)
        {
            await context.Users.AddAsync(user);
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}