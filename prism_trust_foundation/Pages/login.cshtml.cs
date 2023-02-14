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

        private readonly IHttpContextAccessor contxt;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserService registerService, IHttpContextAccessor httpContextAccessor)
        {
            _registerService = registerService;
            this.signinManager = signInManager;
            contxt = httpContextAccessor;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //ApplicationUser? applicationUser = _registerService.GetUserByNRIC(MyUser.NRIC);
                var identityResult = await signinManager.PasswordSignInAsync(LModel.Email, LModel.Password, LModel.RememberMe, true);
                /*if (identityResult.Succeeded)
                {
                    return RedirectToPage("/Admin/Index");
                }
                else */
                if (identityResult.Succeeded)
                {
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("You are a nigger");
                    contxt.HttpContext.Session.SetString("Email", LModel.Email);
                    ApplicationUser? user = _registerService.GetUserByEmail( LModel.Email);
                    if (user.Admin_Role == true)
                    {
                        return RedirectToPage("/Admin/Index");
                    }
                    else
                    {
                        return RedirectToPage("/User/UserDetails");
                    }
                }
                else
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("You have not created an account yet.");
                    return Page();
                }
                ModelState.AddModelError("", "Username or Password incorrect");
            }
            return Page();
        }
    }
}
