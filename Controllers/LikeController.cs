using ApiWebKut.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiWebKut.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController(ILikeService likeService) : ControllerBase
    {
        private readonly ILikeService _likeService = likeService ?? throw new ArgumentNullException(nameof(likeService));
        
        [Authorize]
        [HttpPost("{postId:int}")]
        public async Task<IActionResult> ToggleLike(int postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            if(userId == null) return Unauthorized(new { message = "Usuário não autenticado." });
            var userIdGuid = Guid.Parse(userId.Value);
            try
            {
                var result = await _likeService.ToggleLikeAsync(userIdGuid, postId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Ocorreu um erro na sua solicitação!" });
            }
        }
    }
}
