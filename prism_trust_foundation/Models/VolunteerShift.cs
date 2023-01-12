using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace prism_trust_foundation.Models
{
    public class VolunteerShift
    {
        [Required, RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid timing.")]
        [Key]
        public string Shift_Start { get; set; } = string.Empty;
        [Required, RegularExpression(@"^[0-9]{4}$", ErrorMessage = "Invalid timing.")]
        public string Shift_End { get; set; } = string.Empty ;
        [Required, MaxLength(2)]
        public double Shift_Quantity { get; set; }
    }
}
