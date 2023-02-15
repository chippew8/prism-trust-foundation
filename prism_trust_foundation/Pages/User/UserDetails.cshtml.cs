using EDP_Project.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using System;

namespace prism_trust_foundation.Pages.User
{
    public class UserDetailsModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signinManager;

        private readonly IHttpContextAccessor contxt;

        private UserService _svc;
        private readonly AuthDbContext _context;

        public UserDetailsModel(SignInManager<ApplicationUser> signInManager, UserService service, IHttpContextAccessor httpContextAccessor, AuthDbContext context)
        {
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
            _context = context;
        }

        [BindProperty]
        public ApplicationUser HomeUser { get; set; } = new();
        public List<Coupon> MyCoupons { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
            /*MyCoupons = user.Include(x => x.Coupon).FirstOrDefault(x => x.UserName == User.Identity.Name).Coupon.ToList();*/
            if (user != null)
            {
                HomeUser = user;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("You have not login yet.");
                return RedirectToPage("/Index");
            }
        }
        public IActionResult OnPost(int sessionCount)
        {
            ApplicationUser? user = _svc.GetUserByEmail(HomeUser.Email);
            if (user != null)
            {
                if (user.Email == HomeUser.Email)
                {
                    if (sessionCount == 1)
                    {
                        return RedirectToPage("ChangePass");
                    }
                    else if (sessionCount == 2)
                    {
                        return RedirectToPage("UpdateDetails");
                    }
                    else if (sessionCount == 3)
                    {
                        return RedirectToPage("UpdateAvatar");
                    }
                    else if (sessionCount == 4)
                    {
                        return RedirectToPage("/CouponRedeem");
                    }
                    else
                    {
                        return Page();
                    }
                }
                else
                {
                    return Page();
                }

            }
            else
            {
                return Page();
            }
        }
    }
}
