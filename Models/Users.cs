using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.Models
{
    public class Users
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Username { get; set; }
    }
}
