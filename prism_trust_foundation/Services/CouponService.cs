using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prism_trust_foundation.Models;

namespace EDP_Project.Services
{
	public class CouponService
	{
        private readonly AuthDbContext _context;

        public CouponService(AuthDbContext context)
        {
            _context = context;
        }

        public List<Coupon> GetAll()
        {
            return _context.Coupons.OrderBy(m => m.Coupon_Name).ToList();
        }

        [BindProperty]
        public Coupon MyCoupon { get; set; } = new();

        public Coupon? GetCouponById(string id)
        {
            Coupon? coupon = _context.Coupons.Include(u => u.User).FirstOrDefault(x => x.CouponId.Equals(id));
            return coupon;
        }

        public void AddCoupon(Coupon coupon)
        {
            coupon.Current_Coupon_Quantity = coupon.Total_Coupon_Quantity;
            _context.Coupons.Add(coupon);
            _context.SaveChanges();
        }

        public void UpdateCoupon(Coupon coupon)
        {
            coupon.Current_Coupon_Quantity = coupon.Total_Coupon_Quantity - coupon.Total_Coupon_Redeemed;
            _context.Coupons.Update(coupon);
            _context.SaveChanges();
        }

        public void DeleteCoupon(Coupon coupon)
        {
            _context.Coupons.Remove(coupon);
            _context.SaveChanges();
        }

        public void RedeemCoupon(Coupon coupon)
        {
            coupon.Total_Coupon_Redeemed += 1;
            coupon.Current_Coupon_Quantity -= 1;
            _context.SaveChanges();
        }
    }
}
