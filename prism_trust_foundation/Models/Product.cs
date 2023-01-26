using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class Product
    {
        [Required]
        public string Id { get; set; } = string.Empty;
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public decimal Quantity { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;
    }
}
}
