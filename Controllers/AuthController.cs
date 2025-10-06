using ApiWebKut.DTOs.Users;
using ApiWebKut.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiWebKut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
       
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginUserDto loginUserDto)
        {
            var token = await _userService.LoginAsync(loginUserDto);
            if (token == null)
            {
                return Unauthorized("E-mail ou senha inválida!");
            }
            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordUserDto changePasswordUserDto)
        {

            var userIdExisting = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdExisting))
            {
                return Unauthorized("Id do usuário não foi encontrado!");
            }
            var userId = Guid.Parse(userIdExisting);
            var result = await _userService.ChangePasswordAsync(userId, changePasswordUserDto);
            if (!result)
            {
                return BadRequest("Não foi possível alterar a senha. Verifique os dados fornecidos.");
            }
            return Ok("Senha alterada com sucesso.");
        }
    }
}
