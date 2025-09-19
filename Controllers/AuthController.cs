using ApiWebKut.DTOs.Users;
using ApiWebKut.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiWebKut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
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
    }
}
