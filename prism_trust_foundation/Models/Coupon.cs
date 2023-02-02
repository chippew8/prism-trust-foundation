using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EDP_Project.Models
{
	public class Coupon
	{
        [Required, MinLength(2), MaxLength(5)]
        [Display(Name = "Coupon ID")]
        public string CouponId { get; set; } = string.Empty;

        [Required, MinLength(8), MaxLength(20)]
        [Display(Name = "Coupon Name")]
        public string Coupon_Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "Coupon Discount (%)")]
        public int Percentage_Discount { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        [Display(Name = "Coupon Expiration Date")]
        public DateTime Expiry_Date { get; set; } = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

        [Display(Name = "Total Coupon Redeemed")]
        public int Total_Coupon_Redeemed { get; set; }
        
        [Required]
        [Display(Name = "No. of Coupons Created"),]
        public int Total_Coupon_Quantity { get; set; }

        [Display(Name = "Current Coupon Quantity")]
        public int Current_Coupon_Quantity { get; set; }

        [Required]
        [Display(Name = "Points to redeem Coupon")]
        public int Points_to_redeem { get; set; }

        [MaxLength(50)]
        public string? ImageURL { get; set; }
        public ICollection<CouponRedemption>? CouponRedemptions { get; set; }
    }
}
