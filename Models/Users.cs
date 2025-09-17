using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiWebKut.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; private set; } = Guid.NewGuid();
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;


        // Colecao de likes 
        public ICollection<Likes> Likes { get; set; } = new List<Likes>();
        // Posts
        public ICollection<Posts> Posts { get; set; } = new List<Posts>();
    }
}

