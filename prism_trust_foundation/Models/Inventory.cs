using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class Inventory
    {
        [Key]
        public int InventoryId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }
    }
}
