using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace prism_trust_foundation.Models
{
    public class Event
    {

        [Required]
        [Key]
        public string EventName { get; set; } = string.Empty;

        [Required, MaxLength(25)]
        public string EventType { get; set; } = string.Empty;
        [Required, MaxLength(25)]
        public string Description { get; set; } = string.Empty;
        [Required, MaxLength(50)]
        public string EventVenue { get; set; } = string.Empty;
        [Required]
        public string EventDate { get; set; } = string.Empty;
       
    }
}
