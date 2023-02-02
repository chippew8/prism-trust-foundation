using EDP_Project.Models;
using EDP_Project.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace EDP_Project.Pages.CouponAdmin
{
    public class CouponEditModel : PageModel
    {
        private readonly CouponService _couponService;
        private IWebHostEnvironment _environment;
        public CouponEditModel(CouponService couponService, IWebHostEnvironment environment)
        {
            _couponService = couponService;
            _environment = environment;
        }

        [BindProperty]
        public Coupon MyCoupon { get; set; } = new();
        [BindProperty]
        public IFormFile? Upload { get; set; }
        public IActionResult OnGet(string id)
        {
            Coupon? coupon = _couponService.GetCouponById(id);
            if (coupon != null)
            {
                MyCoupon = coupon;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} not found", id);
                return Redirect("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Upload != null)
                {
                    if (Upload.Length > 2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                        return Page();
                    }

                    var uploadsFolder = "uploads";
                    if (MyCoupon.ImageURL != null)
                    {
                        var oldImageFile = Path.GetFileName(MyCoupon.ImageURL);
                        var oldImagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, oldImageFile);
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyCoupon.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                _couponService.UpdateCoupon(MyCoupon);             
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Coupon ID {0} is updated", MyCoupon.CouponId);
                return Redirect("/CouponAdmin/CouponRead");
                
            }
            return Page();
        }
    }
}
