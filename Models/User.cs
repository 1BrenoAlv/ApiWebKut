using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-zA-Z])(?=.*\d).{8,}$",
            ErrorMessage = "A senha deve ter no mínimo 8 caracteres, com letras e números.")]
        public string Password { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(15)]
        public string Username { get; set; }


        public User() { }
        public User(string fullName, string email, string password, string username)
        {
            Id = Guid.NewGuid(); // GERA O GUID AUTOMATICAMENTE PARA CADA USUARIO
            FullName = fullName;
            Email = email;
            Password = password;
            Username = username;
        }
    }
}
