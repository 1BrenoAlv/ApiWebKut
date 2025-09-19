using ApiWebKut.DTOs.Users;

namespace ApiWebKut.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task<UserDto> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserDto?> UpdateUserAsync(Guid id, UserDto userDto);
        Task<bool> DeleteUserAsync(Guid id);
        //Task<bool> ChangePasswordAsync(Guid id, ChangePasswordUserDto changePasswordUserDto);
        Task<string> LoginAsync(LoginUserDto loginDto);

    }
}
