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
        public Models.User PassUser { get; set; } = new();

        [BindProperty]
        public string? MyMessage { get; set; }

        public IActionResult OnGet(string CurrentID)
        {
            Models.User? user = _svc.GetUserById(CurrentID);
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

        public IActionResult OnPost(int sessionCount)
        {
            var confirmPass = Request.Form["confirmPass"];
            if (ModelState.IsValid)
            {
                if (sessionCount == 1)
                {
                    if (PassUser.Password != confirmPass)
                    {
                        MyMessage = "Password are not matched!";
                        return Page();
                    }
                    else
                    {
                        _svc.UpdateUser(PassUser);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0} is updated", PassUser.Email);
                        return RedirectToPage("UserDetails", new { CurrentID = PassUser.Email });
                    }
                }
                else if (sessionCount == 2)
                {
                    return RedirectToPage("UserDetails", new { CurrentID = PassUser.Email });
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
