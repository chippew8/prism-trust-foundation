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
            var today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            var yesterday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1);
            var twodaysback = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 2);
            var threedaysback = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 3);
            var fourdaysback = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 4);


            Coupon? coupon = _couponService.GetCouponById(MyCoupon.CouponId);
            CouponList = _couponService.GetAll();

            var count = 0;
            var count2 = 0;
            var count3 = 0;
            var count4 = 0;
            var count5 = 0;
            foreach (var i in CouponRedemptionList)
            {
                if (i.Date_of_Redemption == today)
                {
                    count += 1;
                }
                else if (i.Date_of_Redemption == yesterday)
                {
                    count2 += 1;
                }
                else if (i.Date_of_Redemption == twodaysback)
                {
                    count3 += 1;
                }
                else if (i.Date_of_Redemption == threedaysback)
                {
                    count4 += 1;
                }
                else if (i.Date_of_Redemption == fourdaysback)
                {
                    count5 += 1;
                }
            }
            CouponRedemptionList = _couponRedemptionService.GetAll();
        }
    }
}
