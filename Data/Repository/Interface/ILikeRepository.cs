using ApiWebKut.DTOs.Likes;
using ApiWebKut.Models;

namespace ApiWebKut.Data.Repository.Interface
{
    public interface ILikeRepository
    {
       Task<Likes?> GetLikeAsync(Guid userId, int postId);
        Task AddLikeAsync(Likes like);
        Task RemoveLikeAsync(Likes like);
        Task<int> GetLikeCountAsync(int postId);
    }
}
