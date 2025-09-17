using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.DTOs.Users
{
    public class CreateUserDto
    {
        [Required(ErrorMessage ="Digite o seu nome completo!")]
        [StringLength(150)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória!(min. 8 caracteres)")]
        [MinLength(8)] 
        public string Password { get; set; }
        [Required(ErrorMessage = "Digite o seu nome de usuário!")]
        [StringLength (15)]
        public string Usernane { get; set; }
    }
}
