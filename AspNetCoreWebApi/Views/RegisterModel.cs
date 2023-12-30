using System.ComponentModel.DataAnnotations;

namespace Proiect.Views
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string? Username { get; set; }

        [EmailAddress(ErrorMessage = "Adresa de email invalida")]
        [Required(ErrorMessage = "Emailul este obligatoriu")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Parolele nu sunt identice")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

    }
}
