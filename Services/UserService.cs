using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Users;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiWebKut.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

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

        public Task<string> LoginAsync(LoginUserDto loginDto)
        {
            var user = _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.Result.Password))
            {
                return null;
            }
            var token = GenerateJwtToken(user.Result);
            return Task.FromResult(token);
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
        private string GenerateJwtToken(Users user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username)
        }),
                Expires = DateTime.UtcNow.AddHours(8),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
