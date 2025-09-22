using ApiWebKut.Data;
using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Likes;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;

namespace ApiWebKut.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly AppDbContext _appDbContext;
        public LikeService(ILikeRepository likeRepository, IPostRepository postRepository, AppDbContext appDbContext)
        {
            _likeRepository = likeRepository;
            _postRepository = postRepository;
            _appDbContext = appDbContext;
        }

        public async Task<LikesDto> ToggleLikeAsync(Guid userId, int postId)
        {
            var postExists = await _postRepository.GetPostByIdAsync(postId);
            if (postExists == null)
            {
                throw new Exception("Post não encontrado!");
            }
            var like = await _likeRepository.GetLikeAsync(userId, postId);
            if (like != null)
            {
                await _likeRepository.RemoveLikeAsync(like);
            }
            else
            {
                var newLike = new Likes
                {
                    UserId = userId,
                    PostId = postId
                };
                await _likeRepository.AddLikeAsync(newLike);
            }
           await  _appDbContext.SaveChangesAsync();
            var likeCount = await _likeRepository.GetLikeCountAsync(postId);
            var userLiked = await _likeRepository.GetLikeAsync(userId,postId) != null;
            return new LikesDto
            {
                IsLiked = userLiked,
                LikeCount = likeCount,
            };



        }
    }
}
