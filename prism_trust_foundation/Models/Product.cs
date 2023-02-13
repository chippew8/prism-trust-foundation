using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class Product
    {
        [Required]
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; } = string.Empty;

        

        [Required]
        public int Quantity { get; set; }

      
    }
}

