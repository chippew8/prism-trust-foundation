using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace prism_trust_foundation.Models
{
    public class User
    {

        [Required]
        public string NRIC { get; set; }

        [Required, MaxLength(25)]
        public string Fname { get; set; }

        [Required, MaxLength(25)]
        public string Lname { get; set; }

        [Required, MaxLength(50)]
        [Key]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Gender { get; set; }

        public string? Role { get; set; }

        public string? Status { get; set; }

        public string? PhoneNum { get; set; }

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}
