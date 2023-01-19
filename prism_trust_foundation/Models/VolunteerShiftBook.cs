using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class VolunteerShiftBook
    {

        [Required]
        [Key]
        public string ShiftBookingId { get; set; } = string.Empty;
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public string EventId { get; set; } = string.Empty;
        [Required, MaxLength(25)]
        public string EventName { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string EventVenue { get; set; } = string.Empty;
        [Required]
        public string EventDate { get; set; } = string.Empty;
        [Required]
        public string ShiftId { get; set; } = string.Empty;
    }
}
