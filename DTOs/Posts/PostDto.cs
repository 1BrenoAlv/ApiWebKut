using ApiWebKut.DTOs.Likes;
using ApiWebKut.DTOs.TypeContent;
using ApiWebKut.DTOs.Users;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ApiWebKut.DTOs.Posts
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public UserDto User { get; set; }
        public TypeContentDto TypeContent { get; set; }
        public int LikesCount { get; set; }
        public bool UserHasLiked { get; set; }
        public string? ImageUrl { get; set; }
    }

    public class CreatePostDto
    {
        [Required(ErrorMessage = "Digite o Titulo!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Digite o conteúdo do post!")]
        public string Content { get; set; }
        public int TypeContentId { get; set; }
        public IFormFile ImageFile { get; set; }
    }

    public class UpdatePostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public int TypeContentId { get; set; }
        public IFormFile? ImageFile { get; set; }
    }

    public class DeletedPostDto
    {
        public bool IsDeleted { get; set; } = false;
    }
}