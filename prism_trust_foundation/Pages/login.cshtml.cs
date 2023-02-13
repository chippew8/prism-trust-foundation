using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using prism_trust_foundation.ViewModels;

namespace prism_trust_foundation.Pages
{
    public class LoginModel : PageModel
    {
        private readonly UserService _registerService;
        [BindProperty]
        public Login LModel { get; set; }
        public ApplicationUser MyUser { get; set; }

        private readonly SignInManager<ApplicationUser> signinManager;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserService registerService)
        {
            _registerService = registerService;
            this.signinManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //ApplicationUser? applicationUser = _registerService.GetUserByNRIC(MyUser.NRIC);
                var identityResult = await signinManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, false);
                /*if (identityResult.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
                else */
                if (identityResult.Succeeded)
                {
                    return RedirectToPage("/Index");
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page();
        }
    }
}
