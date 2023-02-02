using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.User
{
    public class UserDetailsModel : PageModel
    {
        private readonly ILogger<UserDetailsModel> _logger;
        private UserService _svc;
        public UserDetailsModel(ILogger<UserDetailsModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public Models.User HomeUser { get; set; } = new();

        public IActionResult OnGet(string CurrentID)
        {
            Models.User? user = _svc.GetUserById(CurrentID);
            if (user != null)
            {
                HomeUser = user;
                return Page();
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPost(int sessionCount)
        {
            Models.User? user = _svc.GetUserById(HomeUser.Email);
            if (user != null)
            {
                if (user.Password == HomeUser.Password)
                {
                    if (sessionCount == 1)
                    {
                        return RedirectToPage("ChangePass", new { CurrentID = HomeUser.Email });
                    }
                    else if (sessionCount == 2)
                    {
                        return RedirectToPage("UpdateDetails", new { CurrentID = HomeUser.Email });
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
            else
            {
                return Page();
            }
        }
    }
}
