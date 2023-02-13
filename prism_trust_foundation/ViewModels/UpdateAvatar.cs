using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.ViewModels
{
    public class UpdateAvatar
    {
        [MaxLength(50)]
        public string? ImageURL { get; set; }
    }
}
