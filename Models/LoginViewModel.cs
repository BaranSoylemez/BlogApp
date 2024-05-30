using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(10, ErrorMessage ="Parola 10 karakterden fazla olamaz!"), MinLength(6, ErrorMessage ="Parola 6 karakterden az olamaz!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;
    }
}
