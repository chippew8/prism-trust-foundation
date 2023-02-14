using Microsoft.EntityFrameworkCore.Metadata.Internal;
using prism_trust_foundation.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EDP_Project.Models
{
    public class CouponRedemption
    {
        //[Display(Name = "Coupon Redemption ID")]
        //public string CouponRedemptionId { get; set; }
        [Display(Name = "Coupon Redemption IDs")]
        public int CouponRedemptionId { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date_of_Redemption { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        public string CouponId { get; set; }
        public Coupon Coupon { get; set; }
        public string UserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}
