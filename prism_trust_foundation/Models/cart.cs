using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class cart
    {
        [Key]
        public string Id { get; set; }


        public string productId { get; set; }
        public int quantity { get; set; }
        public string userId { get; set; }

    }
}