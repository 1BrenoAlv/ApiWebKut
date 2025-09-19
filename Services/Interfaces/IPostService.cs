using ApiWebKut.DTOs.Posts;

namespace ApiWebKut.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllPostsAsync();
        Task<PostDto?> GetPostByIdAsync(int id);
        Task<PostDto> CreatePostAsync(CreatePostDto createPostDto, Guid userId);
        Task<PostDto?> UpdatePostAsync(int id, UpdatePostDto updatePostDto, Guid userId);
        Task<bool> DeletePostAsync(int id, Guid userId);
    }
}