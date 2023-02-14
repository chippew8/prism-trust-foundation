using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;
using prism_trust_foundation.Models;

namespace EDP_Project.Services
{
    public class CouponRedemptionService
    {
        private readonly AuthDbContext _context;

        public CouponRedemptionService(AuthDbContext context)
        {
            _context = context;
        }

        public List<CouponRedemption> GetAll()
        {
            return _context.CouponRedemptions.OrderBy(m => m.Date_of_Redemption).ToList();
        }

        [BindProperty]
        public CouponRedemption MyCouponRedemption { get; set; } = new();

        public CouponRedemption? GetCouponRedemptionById(int id)
        {
            CouponRedemption? couponRedemption = _context.CouponRedemptions.FirstOrDefault(x => x.CouponRedemptionId.Equals(id));
            return couponRedemption;
        }

        public void AddCouponRedemption(CouponRedemption couponRedemption)
        {
            _context.CouponRedemptions.Add(couponRedemption);
            _context.SaveChanges();
        }
    }
}
