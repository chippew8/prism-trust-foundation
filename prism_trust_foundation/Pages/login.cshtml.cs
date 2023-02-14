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
        private readonly UserManager<ApplicationUser> userManager;

        private readonly IHttpContextAccessor contxt;

        public LoginModel(SignInManager<ApplicationUser> signInManager, UserService registerService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _registerService = registerService;
            this.signinManager = signInManager;
            contxt = httpContextAccessor;
            this.userManager = userManager;
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
                    ApplicationUser? user = _registerService.GetUserByEmail( LModel.Email);
                    var testuser = await userManager.GetUserAsync(User);
                    Console.WriteLine(testuser);
                    contxt.HttpContext.Session.SetString("Email", user.Email);
                    /*contxt.HttpContext.Session.SetString("Email", "{0}, {1}, {2}", user.Email, user.);*/
                    contxt.HttpContext.Session.SetString("Password", LModel.Password);

                    if (user.ImageURL == null)
                    {
                        contxt.HttpContext.Session.SetString("Url", "/uploads/user.png");
                    }
                    else
                    {
                        contxt.HttpContext.Session.SetString("Url", user.ImageURL);
                    }

                    if (user.Admin_Role == true)
                    {
                        contxt.HttpContext.Session.SetString("Admin", "Yes");
                        return RedirectToPage("/Admin/Index");
                    }
                    else
                    {
                        contxt.HttpContext.Session.SetString("Admin", "No");
                        return RedirectToPage("/Index");
                    }
                }
                else
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Username or Password incorrect");
                    return Page();
                }
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Username or Password is required");
            }
            return Page();
        }
    }
}
