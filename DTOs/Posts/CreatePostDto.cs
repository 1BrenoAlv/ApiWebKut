using ApiWebKut.Models;
using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.DTOs.Posts
{
    public class CreatePostDto
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public int TypeContentId{ get; set; }
    }
}
