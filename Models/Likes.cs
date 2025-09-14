using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.Models
{
    public class Likes
    {
        [Key]
        public int PostId { get; set; }
        [Key]
        public int UserId { get; set; }
    }
}
