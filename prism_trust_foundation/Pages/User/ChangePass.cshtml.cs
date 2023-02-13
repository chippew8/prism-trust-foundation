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

        private readonly IHttpContextAccessor contxt;

        private UserService _svc;

        public ChangePassModel(SignInManager<ApplicationUser> signInManager, UserService service, IHttpContextAccessor httpContextAccessor)
        {
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
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

        public IActionResult OnPost(int sessionCount)
        {
            if (ModelState.IsValid)
            {
                if (sessionCount == 1)
                {
                    return RedirectToPage("UserDetails");
                }
                else if (sessionCount == 2)
                {
                    /*IdentityResult result = UserManager.ChangePassword(model.UserId, model.CurrentPassword, model.NewPassword);*/
                    _svc.UpdateUser(PassUser);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("User {0} is updated", PassUser.Email);
                }
            }
            return RedirectToPage("UserDetails");
        }
    }
}
