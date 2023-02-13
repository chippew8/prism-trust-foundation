using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.User
{
    public class ChangePassModel : PageModel
    {
        private readonly ILogger<ChangePassModel> _logger;
        private UserService _svc;
        public ChangePassModel(ILogger<ChangePassModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public ApplicationUser PassUser { get; set; } = new();

        public IActionResult OnGet(string CurrentID)
        {
            ApplicationUser? user = _svc.GetUserByEmail(CurrentID);
            if (user != null)
            {
                PassUser = user;
                return Page();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _svc.UpdateUser(PassUser);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("User {0} is updated", PassUser.Email);
            }
            return RedirectToPage("UserDetails", new { CurrentID = PassUser.Email });
        }
    }
}
