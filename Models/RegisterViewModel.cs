using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }=null!;

        [Required]
        public string UserSurname { get; set; } = null!;

        [Required]
        public string Nickname { get; set; } = null!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(10, ErrorMessage = "Parola 10 karakterden fazla olamaz!"), MinLength(6, ErrorMessage = "Parola 6 karakterden az olamaz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Required]
        [StringLength(10, ErrorMessage = "Parola 10 karakterden fazla olamaz!"), MinLength(6, ErrorMessage = "Parola 6 karakterden az olamaz!")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Parola yukarıdakiyle aynı olmalıdır!")]
        public string PasswordConf { get; set; } = null!;
    }
}
