using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class TimeslotBooking
    {
        [Required]
        [Key]
        public int TimeslotBookingId { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        public int EventId { get; set; }
        [Required]
        public int ShiftId { get; set; }

    }
}
