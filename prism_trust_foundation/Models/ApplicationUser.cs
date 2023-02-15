using EDP_Project.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Azure.Core.HttpHeader;

namespace prism_trust_foundation.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? NRIC { get; set; }

        public bool? Admin_Role { get; set; }

        public bool? Ban_Status { get; set; }

        [MaxLength(30)]
        public string? Fname { get; set; }

        public string? PhoneNum { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }

        [MaxLength(50)]
        public string? ImageURL { get; set; }

        public string? Gender { get; set; }

        public int points { get; set; }
        public bool? dRecip_Role { get; set; }

        /*        public ICollection<Timeslot>? Timeslot { get; set; }
        */
        public ICollection<Coupon>? Coupon { get; set; }

        public ICollection<CouponRedemption>? CouponRedemption { get; set; }

    }
}
