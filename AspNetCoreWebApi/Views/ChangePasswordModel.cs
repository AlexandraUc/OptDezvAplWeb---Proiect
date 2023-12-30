using System.ComponentModel.DataAnnotations;

namespace Proiect.Views
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "Numele este obligatoriu")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Parola este obligatorie")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "Parolele nu sunt identice")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
