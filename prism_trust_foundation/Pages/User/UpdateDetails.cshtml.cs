using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using prism_trust_foundation.ViewModels;

namespace prism_trust_foundation.Pages.User
{
    public class UpdateDetailsModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signinManager;

        private readonly IHttpContextAccessor contxt;

        private UserService _svc;

        public UpdateDetailsModel(SignInManager<ApplicationUser> signInManager, UserService service, IHttpContextAccessor httpContextAccessor)
        {
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
        }

        [BindProperty]
        public UpdateDetails UdModel { get; set; }

        [BindProperty]
        public ApplicationUser UpdateUser { get; set; } = new();

        public IActionResult OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
            if (user != null)
            {
                UpdateUser = user;
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
                if (sessionCount == 2)
                {
                    string Email = contxt.HttpContext.Session.GetString("Email");
                    ApplicationUser? user = _svc.GetUserByEmail(Email);
                    user.Fname = UdModel.Fname;
                    user.PhoneNum = UdModel.PhoneNum;
                    user.BirthDate = UdModel.BirthDate;
                    user.Gender = UdModel.Gender;
                    _svc.UpdateUser(user);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("User {0} is updated", UpdateUser.Email);
                }
                else if(sessionCount == 1)
                {
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("You have canecel updating of details");
                    return RedirectToPage("UserDetails");
                }
                return RedirectToPage("UserDetails");
            }
            return RedirectToPage("UserDetails");
        }
    }
}
