﻿using System;
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
        public string EventId { get; set; } = string.Empty;
        [Required]
        public string EventName { get; set; } = string.Empty;

        [Required]
        public string EventType { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string EventVenue { get; set; } = string.Empty;
        [Required]
        public string EventDate { get; set; } = string.Empty;
       
    }
}
