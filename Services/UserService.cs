using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Users;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;

namespace ApiWebKut.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<bool> ChangePasswordAsync(Guid id, ChangePasswordUserDto changePasswordUserDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var userEntity = new Users
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                Username = createUserDto.Usernane,
                Password = createUserDto.Password 
            };

            var newUser = await _userRepository.AddUserAsync(userEntity);

            return new UserDto
            {
                Id = newUser.Id,
                FullName = newUser.FullName,
                Email = newUser.Email,
                Username = newUser.Username
            };
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Username = user.Username
            });
        }

        public  async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUsersAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Username = user.Username
            };
        }

        public Task<string> LoginAsync(LoginUserDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<UserDto?> UpdateUserAsync(Guid id, UserDto userDto)
        {
            var existingUser = await _userRepository.GetUsersAsync(id);
            if (existingUser == null)
            {
                return null; 
            }

            existingUser.FullName = userDto.FullName;
            existingUser.Email = userDto.Email;
            existingUser.Username = userDto.Username;

            var updatedUser = await _userRepository.UpdateUserAsync(id, existingUser);

            return new UserDto
            {
                Id = updatedUser.Id,
                FullName = updatedUser.FullName,
                Email = updatedUser.Email,
                Username = updatedUser.Username
            };
        }
        }
}
