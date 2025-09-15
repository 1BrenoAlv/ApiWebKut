using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; } = string.Empty;
        [Required]
        public string Username { get; set; } = string.Empty;
    }
}

