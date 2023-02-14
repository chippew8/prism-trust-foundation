using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.CouponAdmin
{
    public class CouponStatsModel : PageModel
    {

        private readonly CouponService _couponService;
        private readonly CouponRedemptionService _couponRedemptionService;

        public CouponStatsModel(CouponService couponService, CouponRedemptionService couponRedemptionService)
        {
            _couponService = couponService;
            _couponRedemptionService = couponRedemptionService;
        }

        [BindProperty]
        public Coupon MyCoupon { get; set; } = new();
        [BindProperty]
        public CouponRedemption MyCouponRedemption { get; set; } = new();
        public List<Coupon> CouponList { get; set; } = new();
        public List<CouponRedemption> CouponRedemptionList { get; set; } = new();

        public void OnGet()
        {



            Coupon? coupon = _couponService.GetCouponById(MyCoupon.CouponId);
            CouponList = _couponService.GetAll();

            CouponRedemptionList = _couponRedemptionService.GetAll();

        }
    }
}
