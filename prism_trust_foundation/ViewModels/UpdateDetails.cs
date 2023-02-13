using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.ViewModels
{
    public class UpdateDetails
    {
        [Required, MaxLength(30)]
        public string Fname { get; set; }

        public string? PhoneNum { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }
    }
}
