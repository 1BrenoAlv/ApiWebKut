using ApiWebKut.Data.Repository.Interface;
using ApiWebKut.DTOs.Posts;
using ApiWebKut.Services.Interfaces;
using ApiWebKut.Models;
using System.Security.Authentication;

namespace ApiWebKut.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IFileService _fileService;
        public PostService(IPostRepository postRepository, IFileService fileService)
        {
            _postRepository = postRepository;
            _fileService = fileService;
        }

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
            return new PostDto(postToReturn);
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
                throw new AuthenticationException("Você não tem permissão para deletar esse post!");
            }
            return await _postRepository.DeletePostAsync(id);
        }

        public Task<IEnumerable<PostDto>> GetAllPostsAsync()
        {
            var posts = _postRepository.GetAllPostsAsync();
            return Task.FromResult(posts.Result.Select(p => new PostDto(p)));
        }

        public async Task<PostDto> GetPostByIdAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return null;
            }
            return new PostDto(post);
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
            return new PostDto(updatedPost);
        }
    }
}
