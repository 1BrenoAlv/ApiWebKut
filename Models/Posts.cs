using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.Models
{
    public class Posts
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = true;
        public int TypeContentId { get; set; }
    }
}
