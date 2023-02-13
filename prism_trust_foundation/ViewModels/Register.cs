using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.ViewModels
{
    public class Register
    {
        [IsNRIC]
        [Required]
        public string NRIC { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(30)]
        public string Fname { get; set; }

        public string? PhoneNum { get; set; }

        public string? Status { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [MaxLength(50)]
        public string? ImageURL { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password does not match")]
        public string ConfirmPassword { get; set; }
    }
}
