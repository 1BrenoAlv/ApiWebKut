using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWebKut.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Content { get; set; } = string.Empty ;
        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        public bool IsDeleted { get; set; } = false;
        public string? ImageUrl { get; set; }
        public int TypeContentId { get; set; } 
        [ForeignKey("TypeContentId")]
        public virtual TypeContent TypeContent { get; set; } 
        public Guid UserId { get; set; } 
        [ForeignKey("UserId")]
        public virtual Users User { get; set; }


        // colecao de likes
        public ICollection<Likes> Likes { get; set; } = new List<Likes>();
    }
}
