using ApiWebKut.DTOs.Users;
using ApiWebKut.Models;

namespace ApiWebKut.Data.Repository.Interface
{
    public interface IUserRepository
    {
        Task<List<Users>> GetAllAsync();
        Task<Users> GetUsersAsync(Guid id);
        Task<Users> AddUserAsync(Users user);
        Task<bool>DeleteUserAsync(Guid id);
        Task<Users> UpdateUserAsync(Guid id, Users user);

    }
}
