using ApiWebKut.DTOs.Likes;
using System.Security.Claims;

namespace ApiWebKut.Services.Interfaces
{
    public interface ILikeService
    {
        Task<LikesDto>ToggleLikeAsync(Guid userId, int postId);
    }
}
