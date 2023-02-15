using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Models
{
    public class Question
    {
        [Key]
        public int QueryId { get; set; }
        public string Email { get; set; } = string.Empty;
        [Required]
        public string UserQuestion { get; set; } = string.Empty;
    }
}
