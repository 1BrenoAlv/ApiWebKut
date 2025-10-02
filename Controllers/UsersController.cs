using ApiWebKut.Data;
using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Users;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ApiWebKut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users); 
        }
        [HttpGet("{id:guid}")]

        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound(); 
            }

            return Ok(user); 
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var newUser = await _userService.CreateUserAsync(createUserDto);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }
        [HttpDelete("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpPut("{id:guid}")]
        [Authorize]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody]UpdateUserDto updateUserDto)
        {
            var userIdToken = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            if(id !=  userIdToken)
            {
                return Forbid("Você não tem permissão para editar este perfil.");
            }
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(id, updateUserDto);
                if (updatedUser == null)
                {
                    return NotFound("Usuário não encontrado.");
                }
                return Ok(updatedUser);
            }catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
