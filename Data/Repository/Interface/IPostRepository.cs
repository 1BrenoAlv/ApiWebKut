using ApiWebKut.Models;

namespace ApiWebKut.Data.Repository.Interface
{
    public interface IPostRepository
    {
        Task<Posts> AddPostAsync(Posts post);
        Task<Posts?> GetPostByIdAsync(int id);
        Task<List<Posts>> GetAllPostsAsync();
        Task<Posts?> UpdatePostAsync(int id, Posts post);
        Task<bool> DeletePostAsync(int id);
    }
}
