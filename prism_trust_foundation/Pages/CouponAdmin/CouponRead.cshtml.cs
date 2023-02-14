using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EDP_Project.Pages.CouponAdmin
{
    public class CouponReadModel : PageModel
    {
        private readonly CouponService _couponService;

        public CouponReadModel(CouponService couponService)
        {
            _couponService = couponService;
        }

        [BindProperty]
        public Coupon MyCoupon { get; set; } = new();
        public List<Coupon> CouponList { get; set; } = new();

        public void OnGet()
        {
            Coupon? coupon = _couponService.GetCouponById(MyCoupon.CouponId);
            CouponList = _couponService.GetAll();
        }

        public IActionResult OnPost()
        {
            Coupon ? coupon = _couponService.GetCouponById(MyCoupon.CouponId);
            if(coupon != null)
            {
                _couponService.DeleteCoupon(coupon);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} has been deleted", MyCoupon.CouponId);
            }
            return Redirect("/CouponAdmin/CouponRead");
        }
    }
}
