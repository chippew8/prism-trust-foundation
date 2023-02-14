using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class Timeslot
    {
        [Required, Key]
        public int TimeslotId { get; set; }

        [Required]
        public int Shift_Start { get; set; }

        [Required]
        public int Shift_End { get; set; }

        [Required]
        public int Shift_Quantity { get; set; }

        [Required]
        public string Shift_Type { get; set; }

        [Required]
        public int EventId { get; set; } 

    }
}
