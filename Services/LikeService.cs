using ApiWebKut.Data;
using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Likes;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;

namespace ApiWebKut.Services
{
    public class LikeService(ILikeRepository likeRepository, IPostRepository postRepository, AppDbContext appDbContext) : ILikeService
    {
        private readonly ILikeRepository _likeRepository = likeRepository ?? throw new ArgumentNullException(nameof(likeRepository));
        private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        private readonly AppDbContext _appDbContext = appDbContext ?? throw new ArgumentNullException(nameof(appDbContext));

        public async Task<LikesDto> ToggleLikeAsync(Guid userId, int postId)
        {
            var postExists = await _postRepository.GetPostByIdAsync(postId);
            if (postExists == null)
            {
                throw new Exception("Post não encontrado!");
            }

            var like = await _likeRepository.GetLikeAsync(userId, postId);
            bool isLikedAfterToggle;

            if (like != null)
            {
                // Se o like existia, nós o removemos.
                await _likeRepository.RemoveLikeAsync(like);
                isLikedAfterToggle = false;
            }
            else
            {
                var newLike = new Likes { UserId = userId, PostId = postId };
                await _likeRepository.AddLikeAsync(newLike);
                isLikedAfterToggle = true;
            }

            await _appDbContext.SaveChangesAsync();


            var likeCount = await _likeRepository.GetLikeCountAsync(postId);
            return new LikesDto
            {
                IsLiked = isLikedAfterToggle,
                LikeCount = likeCount,
            };
        }
    }
}
