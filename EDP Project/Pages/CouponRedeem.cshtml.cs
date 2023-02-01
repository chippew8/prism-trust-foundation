using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages
{
    public class CouponRedeemModel : PageModel
    {
        private readonly CouponService _couponService;
        private readonly CouponRedemptionService _couponRedemptionService;

        public CouponRedeemModel(CouponService couponService, CouponRedemptionService couponRedemptionService)
        {
            _couponService = couponService;
            _couponRedemptionService = couponRedemptionService;
        }

        [BindProperty]
        public Coupon MyCoupon { get; set; } = new();
        [BindProperty]
        public CouponRedemption MyCouponRedemption { get; set; } = new();

        public List<Coupon> CouponList { get; set; } = new();

        public void OnGet()
        {
            CouponList = _couponService.GetAll();
        }

        public IActionResult OnPost()
        {
            Coupon? coupon = _couponService.GetCouponById(MyCoupon.CouponId);
            CouponRedemption? couponRedemption = _couponRedemptionService.GetCouponRedemptionById(MyCouponRedemption.CouponRedemptionId);
            if (coupon != null)
            {
                _couponService.RedeemCoupon(coupon);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} has been redeemed", MyCoupon.CouponId);
            }
            
            if (couponRedemption != null)
            {
                MyCouponRedemption.Coupon = coupon;
                MyCouponRedemption.Date_of_Redemption = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                _couponRedemptionService.AddCouponRedemption(MyCouponRedemption);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} has been redeemed", MyCoupon.CouponId);
            }
            else if (couponRedemption == null)
            {
                MyCouponRedemption.Date_of_Redemption = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                MyCouponRedemption.Coupon = coupon;
                _couponRedemptionService.AddCouponRedemption(MyCouponRedemption);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} has been redeemed", MyCoupon.CouponId);
            }



            return Redirect("/Index");
        }
    }
}
