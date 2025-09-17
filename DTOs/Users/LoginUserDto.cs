using System.ComponentModel.DataAnnotations;
namespace ApiWebKut.DTOs.Users
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória!(min. 8 caracteres)")]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
