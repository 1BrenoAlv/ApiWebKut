using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Likes;
using ApiWebKut.DTOs.Posts;
using ApiWebKut.DTOs.Users;
using ApiWebKut.Models;
using ApiWebKut.Services.Interfaces;
using Microsoft.Extensions.Hosting;
using System.Security.Authentication;
using System.Security.Claims;

namespace ApiWebKut.Services
{
    public class PostService(IPostRepository postRepository, IFileService fileService, IHttpContextAccessor httpContextAccessor) : IPostService
    {
        private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        private readonly IFileService _fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));


        public async Task<PostDto> CreatePostAsync(CreatePostDto createPostDto, Guid userId)
        {
            string? imageUrl = null;
            if (createPostDto.ImageFile != null)
            {
                imageUrl = await _fileService.SaveImageAsync(createPostDto.ImageFile);
            }
            else
            {
                createPostDto.ImageFile = null;
            }
            var post = new Posts
            {
                Title = createPostDto.Title,
                Content = createPostDto.Content,
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                TypeContentId = createPostDto.TypeContentId,
                ImageUrl = imageUrl
            };
            var newPost = await _postRepository.AddPostAsync(post);
            var postToReturn = await _postRepository.GetPostByIdAsync(newPost.Id);
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                ImageUrl = post.ImageUrl,
                User = new UserDto
                {
                    Id = post.User.Id,
                    Username = post.User.Username,
                    Email = post.User.Email,
                    FullName = post.User.FullName
                },
                TypeContent = new DTOs.TypeContent.TypeContentDto
                {
                    Id = post.TypeContent.Id,
                    Content = post.TypeContent.Content
                }
            };
        }

        public async Task<bool> DeletePostAsync(int id, Guid userId)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            if (post == null)
            {
                return false;
            }
            if (post.UserId != userId)
            {
                throw new AuthenticationException("Você não tem permissão para deletar este post!");
            }

            var imageUrlToDelete = post.ImageUrl;

            var success = await _postRepository.DeletePostAsync(id);

            if (success && !string.IsNullOrEmpty(imageUrlToDelete))
            {
                var imagePath = Path.Combine("wwwroot", imageUrlToDelete.TrimStart('/'));

                if (File.Exists(imagePath))
                {
                    File.Delete(imagePath);
                }
            }

            return success;
        }

        public async Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Guid.TryParse(userIdString, out Guid userId);
            var posts = await _postRepository.GetAllPostsAsync();
            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt ?? p.CreatedAt,
                ImageUrl = p.ImageUrl,
                User = new UserDto
                {
                    Id = p.User.Id,
                    Username = p.User.Username,
                    Email = p.User.Email,
                },
                TypeContent = new DTOs.TypeContent.TypeContentDto
                {
                    Id = p.TypeContent.Id,
                    Content = p.TypeContent.Content,
                },
                LikesCount = p.Likes?.Count ?? 0,

                UserHasLiked = userId != Guid.Empty && (p.Likes?.Any(like => like.UserId == userId) ?? false)
            });
            
            return postDtos;
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var userIdString = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out Guid currentUserId))
            {
                return null;
            }
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null || post.UserId != currentUserId)
            {
                return null;
            }

            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                UpdatedAt = post.UpdatedAt ?? post.CreatedAt,
                ImageUrl = post.ImageUrl,
                User = new UserDto
                {
                    Id = post.User.Id,
                    Username = post.User.Username,
                    Email = post.User.Email,
                    FullName = post.User.FullName
                },
                TypeContent = new DTOs.TypeContent.TypeContentDto
                {
                    Id = post.TypeContent.Id,
                    Content = post.TypeContent.Content
                },
                LikesCount = post.Likes?.Count() ?? 0,
                UserHasLiked = post.Likes?.Any(like => like.UserId == currentUserId) ?? false
            };
        }
        public async Task<IEnumerable<PostDto>> GetPostsByUserIdAsync(Guid userId)
        {
            var posts = await _postRepository.GetPostsByUserIdAsync(userId);
            return posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt ?? p.CreatedAt,
                ImageUrl = p.ImageUrl,
                User = new UserDto
                {
                    Id = p.User.Id,
                    Username = p.User.Username,
                    Email = p.User.Email,
                },
                TypeContent = new DTOs.TypeContent.TypeContentDto
                {
                    Id = p.TypeContent.Id,
                    Content = p.TypeContent.Content,
                },
                LikesCount = p.Likes?.Count ?? 0
            });
        }

        public async Task<PostDto> UpdatePostAsync(int id, UpdatePostDto updatePostDto, Guid userId) 
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return null;
            }
            if (post.UserId != userId)
            {
                throw new AuthenticationException("Você não tem permissão para editar esse post!");
            }
            
            post.Title = updatePostDto.Title;
            post.Content = updatePostDto.Content;
            post.TypeContentId = updatePostDto.TypeContentId;

            var updatedPost = await _postRepository.UpdatePostAsync(id, post);
            return new PostDto
            {
                Id = updatedPost.Id,
                Title = updatedPost.Title,
                Content = updatedPost.Content,
                CreatedAt = updatedPost.CreatedAt,
                ImageUrl = updatedPost.ImageUrl,
                User = new UserDto
                {
                    Id = updatedPost.User.Id,
                    Username = updatedPost.User.Username,
                    Email = updatedPost.User.Email,
                    FullName = updatedPost.User.FullName
                },
                TypeContent = new DTOs.TypeContent.TypeContentDto
                {
                    Id = updatedPost.TypeContent.Id,
                    Content = updatedPost.TypeContent.Content
                }
            };
        }
    }
}
