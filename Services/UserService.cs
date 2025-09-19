using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Users;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiWebKut.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public UserService(IUserRepository userRepository, IConfiguration configuration, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        //public async Task<bool> ChangePasswordAsync(Guid id, ChangePasswordUserDto changePasswordUserDto)
        //{
        //    var user = await _userRepository.GetUsersAsync(id);
        //    if (user == null)
        //    {
        //        return false;
        //    }
        //    if (!BCrypt.Net.BCrypt.Verify(changePasswordUserDto.OldPassword, user.Password))
        //    {
        //        return false;
        //    }
        //    var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordUserDto.NewPassword);
        //    user.Password = newHashedPassword;
        //    await _userRepository.UpdateUserAsync(id, user);
        //    return true;
        //}

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var userEntity = new Users
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                Username = createUserDto.Username,
                Password = createUserDto.Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
            };

            var newUser = await _userRepository.AddUserAsync(userEntity);

            return new UserDto(newUser);
            
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            return await _userRepository.DeleteUserAsync(id);
        }

        
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(user => new UserDto(user));
        }

        public async Task<UserDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetUsersAsync(id);
            if (user == null)
            {
                return null;
            }

            return new UserDto(user);
        }

        public async Task<string> LoginAsync(LoginUserDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Password))
            {
                return null;
            }
            var token = _tokenService.GenerateJwtToken(user);
            return token;
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

            return new UserDto(updatedUser);
        }
        
    }
}
