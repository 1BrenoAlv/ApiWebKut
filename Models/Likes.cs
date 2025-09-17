using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWebKut.Models
{
    public class Likes
    {
        public int PostId { get; set; } 
        public virtual Posts Post { get; set; } 

        public Guid UserId { get; set; } 
        public virtual Users User { get; set; }

    }
}
