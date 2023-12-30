using System.ComponentModel.DataAnnotations;

namespace Proiect.Views
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        public string? Password { get; set; }
    }
}
