using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class VolunteerShiftBook
    {

        [Required]
        [Key]
        public string EventId { get; set; } = string.Empty;

        [Required, MaxLength(25)]
        public string Event { get; set; } = string.Empty;
        [Required, MaxLength(25)]
        public string Description { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string EventVenue { get; set; } = string.Empty;
        [Required]
        public string EventDate { get; set; } = string.Empty;
    }
}
