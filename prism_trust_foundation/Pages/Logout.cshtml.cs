using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages
{
    public class LogoutModel : PageModel
    {

        private readonly SignInManager<ApplicationUser> signInManager;

        private readonly IHttpContextAccessor contxt;

        private UserService _svc;

        public LogoutModel(SignInManager<ApplicationUser> signInManager, UserService service, IHttpContextAccessor httpContextAccessor)
        {
            this.signInManager = signInManager;
            contxt = httpContextAccessor;
            _svc = service;
        }

        public IActionResult OnGet() 
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
            if (user != null)
            {
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("You have not login yet.");
                return RedirectToPage("Index");
            }
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            contxt.HttpContext.Session.Clear();
            await signInManager.SignOutAsync();
            return RedirectToPage("Login");
        }

        public async Task<IActionResult> OnPostDontLogoutAsync()
        {
            return RedirectToPage("Index");
        }
    }
}
