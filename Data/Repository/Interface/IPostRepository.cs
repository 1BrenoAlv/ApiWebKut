using ApiWebKut.Models;
using Microsoft.Extensions.Hosting;

namespace ApiWebKut.Data.Repository.Interface
{
    public interface IPostRepository
    {
        Task<Posts> AddPostAsync(Posts post);
        Task<Posts?> GetPostByIdAsync(int id);
        Task<IEnumerable<Posts>> GetPostsByUserIdAsync(Guid userId);
        Task<List<Posts>> GetAllPostsAsync();
        Task<Posts?> UpdatePostAsync(int id, Posts post);
        Task<bool> DeletePostAsync(int id);
    }
}
