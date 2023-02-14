using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace EDP_Project.Pages
{
    public class CouponRedeemModel : PageModel
    {
        private readonly CouponService _couponService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly CouponRedemptionService _couponRedemptionService;
        private readonly AuthDbContext _db;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor contxt;



        public CouponRedeemModel(CouponService couponService, CouponRedemptionService couponRedemptionService,
            AuthDbContext db, UserService userService, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _couponService = couponService;
            _couponRedemptionService = couponRedemptionService;
            _db = db;
            _userService = userService;
            this.userManager = userManager;
            contxt = httpContextAccessor;
        }

        [BindProperty]
        public Coupon MyCoupon { get; set; } = new();
        [BindProperty]
        public CouponRedemption MyCouponRedemption { get; set; } = new();
        [BindProperty]
        public ApplicationUser? couponuser { get; set; } = new();

        public List<Coupon> CouponList { get; set; } = new();
        /*        [BindProperty]
                public ApplicationUser MyUser { get; set; } = new();*/


        public void OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            couponuser = _userService.GetUserByEmail(Email);
            CouponList = _couponService.GetAll();

            string? username = User.Identity?.Name;

        }

        public async Task<IActionResult> OnPost()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            var user = await userManager.GetUserAsync(User);
            Coupon? coupon = _couponService.GetCouponById(MyCoupon.CouponId);
            ApplicationUser? applicationUser = _userService.GetUserByEmail(Email);
            CouponRedemption? couponRedemption = _couponRedemptionService.GetCouponRedemptionById(MyCouponRedemption.CouponRedemptionId);
            if (coupon != null)
            {
                _couponService.RedeemCoupon(coupon);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} has been redeemed", MyCoupon.CouponId);
            }

            if (couponRedemption != null)
            {
                MyCouponRedemption.Date_of_Redemption = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                MyCouponRedemption.Coupon = coupon;
                MyCouponRedemption.ApplicationUser = applicationUser;

                MyCouponRedemption.CouponId = coupon.CouponId;
                MyCouponRedemption.UserId = applicationUser.Email;

                _couponRedemptionService.AddCouponRedemption(MyCouponRedemption);

                applicationUser.Coupon.Add(coupon);
                _db.SaveChanges();
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} has been redeemed", MyCoupon.CouponId);
            }
            else if (couponRedemption == null)
            {
                MyCouponRedemption.Date_of_Redemption = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

                MyCouponRedemption.Coupon = coupon;
                MyCouponRedemption.ApplicationUser = applicationUser;

                MyCouponRedemption.CouponId = coupon.CouponId;
                MyCouponRedemption.UserId = applicationUser.Id;

                _couponRedemptionService.AddCouponRedemption(MyCouponRedemption);

                applicationUser.Coupon.Add(coupon);
                applicationUser.points -= coupon.Points_to_redeem;
                _db.SaveChanges();
                /*coupon.User.Add(applicationUser);*/
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} has been redeemed", MyCoupon.CouponId);
            }



            return Redirect("/Index");
        }


    }
}
