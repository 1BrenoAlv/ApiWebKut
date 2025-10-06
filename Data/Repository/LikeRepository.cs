using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Likes;
using ApiWebKut.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiWebKut.Data.Repository
{
    public class LikeRepository(AppDbContext appDbContext) : ILikeRepository
    {
        private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task AddLikeAsync(Likes like)
        {
            await _appDbContext.Likes.AddAsync(like);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Likes?> GetLikeAsync(Guid userId, int postId)
        {
            var like = await _appDbContext.Likes.FirstOrDefaultAsync(l => l.UserId == userId && l.PostId == postId);
            return like;
        }

        public Task<int> GetLikeCountAsync(int postId)
        {
            return _appDbContext.Likes.CountAsync(like => like.PostId == postId);
        }

        public Task RemoveLikeAsync(Likes like)
        {
            _appDbContext.Likes.Remove(like);
            return _appDbContext.SaveChangesAsync();
        }
    }
}
