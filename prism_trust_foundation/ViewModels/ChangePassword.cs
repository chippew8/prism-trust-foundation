using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.ViewModels
{
    public class ChangePassword
    {
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
