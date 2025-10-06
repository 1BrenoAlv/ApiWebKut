using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Users;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;

namespace ApiWebKut.Services
{
    public class UserService(IUserRepository userRepository, IConfiguration configuration, ITokenService tokenService) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        private readonly IConfiguration _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        private readonly ITokenService _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));

        public async Task<bool> ChangePasswordAsync(Guid id, ChangePasswordUserDto changePasswordUserDto)
        {
            var user = await _userRepository.GetUsersAsync(id);
            if (user == null)
            {
                return false;
            }
            if (!BCrypt.Net.BCrypt.Verify(changePasswordUserDto.OldPassword, user.Password))
            {
                return false;
            }
            var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordUserDto.NewPassword);
            user.Password = newHashedPassword;
            await _userRepository.UpdateUserAsync(id, user);
            return true;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto createUserDto)
        {
            var userEntity = new Users
            {
                FullName = createUserDto.FullName,
                Email = createUserDto.Email,
                Username = createUserDto.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password)
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

        public async Task<UserDto> GetUserByIdAsync(Guid id)
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

        public async Task<UserDto?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUsersAsync(id);
            if (user == null)
            {
                return null;
            }
            var existingUserEmail = await _userRepository.GetUserByEmailAsync(updateUserDto.Email);
            if(existingUserEmail != null && existingUserEmail.Id != id)
            {
                throw new Exception("E-mail já está em uso por outro usuário.");
            }
            user.FullName = updateUserDto.FullName;
            user.Username = updateUserDto.Username;
            user.Email = updateUserDto.Email;

            var updatedUser = await _userRepository.UpdateUserAsync(id, user);

            return new UserDto
            {
                Id = updatedUser.Id, 
                FullName = updatedUser.FullName,
                Username = updatedUser.Username,
                Email = updatedUser.Email
            };

        }

    }
}
