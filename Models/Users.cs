using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWebKut.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        [Required]
        public string FullName { get; set; } 
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; } 
        [Required]
        public string Username { get; set; }


        // Colecao de likes 
        public ICollection<Likes> Likes { get; set; } = new List<Likes>();
        // Posts
        public ICollection<Posts> Posts { get; set; } = new List<Posts>();
    }
}

