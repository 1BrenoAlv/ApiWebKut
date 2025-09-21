using ApiWebKut.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ApiWebKut.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound(new { message = "Post não encontrado" });
            }
            return Ok(post);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePost([FromBody] DTOs.Posts.CreatePostDto createPostDto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized(new { message = "Usuário não autenticado ou token inválido." });
            }
            var userId = Guid.Parse(userIdString);
            var newPost = await _postService.CreatePostAsync(createPostDto, userId);
            return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id }, newPost);
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] DTOs.Posts.UpdatePostDto updatePostDto)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized(new { message = "Usuário não autenticado ou token inválido." });
            }
            var userId = Guid.Parse(userIdString);
            var updatedPost = await _postService.UpdatePostAsync(id, updatePostDto, userId);

            if (updatedPost == null)
            {
                return NotFound(new { message = "Post não encontrado ou você não tem permissão para editá-lo." });
            }
            return Ok(updatedPost);
        }
        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdString))
            {
                return Unauthorized(new { message = "Usuário não autenticado ou token inválido." });
            }

            var userId = Guid.Parse(userIdString);
            var result = await _postService.DeletePostAsync(id, userId);

            if (!result)
            {
                return NotFound(new { message = "Post não encontrado ou você não tem permissão para excluí-lo." });
            }
            return NoContent();
        }
    }
}