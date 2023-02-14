using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public UserDetailsModel(SignInManager<ApplicationUser> signInManager, UserService service, IHttpContextAccessor httpContextAccessor)
        {
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
        }

        [BindProperty]
        public ApplicationUser HomeUser { get; set; } = new();

        public IActionResult OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
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
