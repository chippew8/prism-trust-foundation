using Microsoft.AspNetCore.Identity;
using static Azure.Core.HttpHeader;

namespace prism_trust_foundation.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string NRIC { get; set; }

        public bool Admin_Role { get; set; }

        public bool Ban_Status { get; set; }

    }
}
