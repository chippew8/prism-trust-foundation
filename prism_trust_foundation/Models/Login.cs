using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace prism_trust_foundation.Models
{
    public class Login
    {

        [Required]
        public string NRIC { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
