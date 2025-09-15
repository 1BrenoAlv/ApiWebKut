using ApiWebKut.Data;
using ApiWebKut.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public UsersController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            _appDbContext.User.Add(user);

            await _appDbContext.SaveChangesAsync();

            return Ok(user);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _appDbContext.User.ToListAsync();


            return Ok(users) ;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<User>>> GetIdUser(Guid id)
        {
            var user = await _appDbContext.User.FindAsync(id);

            if(user == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(user) ;
        }
//        [HttpPatch("{id}")]
//        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] User userUpdated)
//        {
//            var user = await _appDbContext.User.FindAsync(id);
//t _appDbContext.SaveChangesAsync();

//        }

    }
}
