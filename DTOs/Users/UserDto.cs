using System.ComponentModel.DataAnnotations;

namespace ApiWebKut.DTOs.Users
{
    public class UserDto
    {
        public Guid Id { get; init; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
    }

    public class LoginUserDto
    {
        [Required(ErrorMessage = "E-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O formato do e-mail é inválido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória!(min. 8 caracteres)")]
        [MinLength(8)]
        public string Password { get; set; }
    }

    public class CreateUserDto
    {
        [Required(ErrorMessage = "Digite o seu nome completo!")]
        [StringLength(150)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória!(min. 8 caracteres)")]
        [MinLength(8)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Digite o seu nome de usuário!")]
        [StringLength(15)]
        public string Username { get; set; }
    }

    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Nome completo é obrigatório!")]
        [StringLength(150)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Nome de usuário é obrigatório!")]
        [StringLength(15)]
        public string Username { get; set; }
        [Required(ErrorMessage = "E-mail é obrigatório!")]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class ChangePasswordUserDto
    {
        [Required(ErrorMessage = "Senha atual é obrigatória!(min. 8 caracteres)")]
        [MinLength(8)]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "Nova senha é obrigatória!(min. 8 caracteres)")]
        [MinLength(8)]
        public string NewPassword { get; set; }
        [Compare("NewPassword", ErrorMessage = "As senhas não coincidem.")]
        public string NewPasswordConfirmed { get; set; }
    }

}
