using EDP_Project.Models;
using Microsoft.AspNetCore.Mvc;

namespace EDP_Project.Services
{
    public class CouponRedemptionService
    {
        private readonly MyDbContext _context;

        public CouponRedemptionService(MyDbContext context)
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

        //public void AddCouponDateOfRedemption(CouponRedemption couponRedemption)
        //{
        //    couponRedemption.Date_of_Redemption = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
        //    _context.CouponRedemptions.Add(couponRedemption);
        //    _context.SaveChanges();
        //}

        public void AddCouponRedemption(CouponRedemption couponRedemption)
        {
            _context.CouponRedemptions.Add(couponRedemption);
            _context.SaveChanges();
        }
    }
}
