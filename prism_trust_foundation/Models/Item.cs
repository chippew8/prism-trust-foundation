using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class Item
    {
        
        public string UserEmail { get; set; } = string.Empty;
        [Required]
        public Product Product { get; set; }

        [Required,Range(1,3)]
        public int itemquantity { get; set; }

        
    }
}