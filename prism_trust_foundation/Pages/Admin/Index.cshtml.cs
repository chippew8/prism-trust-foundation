using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Pages.Admin;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signinManager;

        private readonly IHttpContextAccessor contxt;

        private readonly UserService _svc;

        public IndexModel(UserService service, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
        }

        public List<ApplicationUser> UserList { get; set; } = new();

        public IActionResult OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
            if (user != null)
            {
                if(contxt.HttpContext.Session.GetString("Admin") == "Yes")
                {
                    UserList = _svc.GetAll();
                    return Page();
                }
                else
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Unauthorized Access");
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("You have not login yet.");
                return RedirectToPage("/Index");
            }
        }
    }
}
