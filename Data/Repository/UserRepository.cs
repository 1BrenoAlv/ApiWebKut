using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Data.Repository
{
    public class UserRepository(AppDbContext appDbContext) : IUserRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<Users> AddUserAsync(Users user)
        {
            await _appDbContext.Users.AddAsync(user);
            await _appDbContext.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var userDelete = await _appDbContext.Users.FindAsync(id);

            if (userDelete == null)
            {
                return false;
            }
            _appDbContext.Users.Remove(userDelete);
            return await _appDbContext.SaveChangesAsync() > 0;
        }

        public async Task<List<Users>> GetAllAsync()
        {
            return await _appDbContext.Users.ToListAsync();
        }

        public async Task<Users> GetUserByEmailAsync(string email)
        {
            var user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

        public async Task<Users?> GetUsersAsync(Guid id)
        {
            return await _appDbContext.Users.FindAsync(id);
        }

        public async Task<Users?> UpdateUserAsync(Guid id, Users user)
        {
            var existingUser = await _appDbContext.Users.FindAsync(id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.FullName = user.FullName;
            existingUser.Email = user.Email;
            existingUser.Username = user.Username;

            _appDbContext.Users.Update(existingUser);
            await _appDbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}