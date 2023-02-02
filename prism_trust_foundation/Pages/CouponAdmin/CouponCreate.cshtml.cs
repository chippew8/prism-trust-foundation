using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace EDP_Project.Pages
{
    public class CouponCreateModel : PageModel
    {
        private readonly CouponService _couponService;
        private IWebHostEnvironment _environment;

        public CouponCreateModel(CouponService couponService, IWebHostEnvironment environment)
        {
            _couponService = couponService;
            _environment = environment;
        }

        [BindProperty]
        public Coupon MyCoupon { get; set; } = new();
        [BindProperty]
        public IFormFile? Upload { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Coupon? coupon = _couponService.GetCouponById(MyCoupon.CouponId);
                if (coupon != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} alreay exists", MyCoupon.CouponId);
                    return Page();
                }

                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyCoupon.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                _couponService.AddCoupon(MyCoupon);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon {0} is added", MyCoupon.Coupon_Name);
                return Redirect("/CouponAdmin/CouponRead");

            }
            return Page();
        }
    }
}
