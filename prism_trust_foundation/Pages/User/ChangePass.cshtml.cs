using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using prism_trust_foundation.ViewModels;

namespace prism_trust_foundation.Pages.User
{
    public class ChangePassModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signinManager;

        private UserManager<ApplicationUser> userManager;

        private readonly IHttpContextAccessor contxt;

        private UserService _svc;

        private readonly AuthDbContext _authDbContext;

        public ChangePassModel(UserManager<ApplicationUser> userManager, AuthDbContext authDbContext, SignInManager<ApplicationUser> signInManager, UserService service, IHttpContextAccessor httpContextAccessor)
        {
            this.userManager = userManager;
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
            _authDbContext = authDbContext;
        }

        [BindProperty]
        public ChangePassword CpModel { get; set; }

        public ApplicationUser PassUser { get; set; } = new();

        public IActionResult OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
            if (user != null)
            {
                PassUser = user;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("You have not login yet.");
                return RedirectToPage("/Index");
            }
        }

        public async Task<ActionResult> OnPost(int sessionCount)
        {

            if (ModelState.IsValid)
            {
                if (sessionCount == 1)
                {
                    return RedirectToPage("UserDetails");
                }
                else if (sessionCount == 2)
                {
                    string Email = contxt.HttpContext.Session.GetString("Email");
                    string CurrentPassword = contxt.HttpContext.Session.GetString("Password");
                    ApplicationUser? user = _svc.GetUserByEmail(Email);
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    if (user != null)
                    {
                        IdentityResult result = await userManager.ResetPasswordAsync(user, token, CpModel.NewPassword);
                        _svc.UpdateUser(user);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0} is updated", PassUser.Email);
                        contxt.HttpContext.Session.Clear();
                        await signinManager.SignOutAsync();
                        return RedirectToPage("/Login");
                    }
                }
            }
            return RedirectToPage("UserDetails");
        }
    }
}
