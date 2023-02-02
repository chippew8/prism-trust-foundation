using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.User
{
    public class UpdateDetailsModel : PageModel
    {
        private readonly ILogger<UpdateDetailsModel> _logger;
        private UserService _svc;
        public UpdateDetailsModel(ILogger<UpdateDetailsModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        [BindProperty]
        public Models.User UpdateUser { get; set; } = new();

        [BindProperty]
        public string? MyMessage { get; set; }

        public IActionResult OnGet(string CurrentID)
        {
            Models.User? user = _svc.GetUserById(CurrentID);
            if (user != null)
            {
                UpdateUser = user;
                return Page();
            }
            else
            {
                return Page();
            }
        }

        public IActionResult OnPost(int sessionCount)
        {
            if (ModelState.IsValid)
            {
                if (sessionCount == 1)
                {
                    _svc.UpdateUser(UpdateUser);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("User {0} is updated", UpdateUser.Email);
                    return RedirectToPage("UserDetails", new { CurrentID = UpdateUser.Email });
                }
                else if (sessionCount == 2)
                {
                    return RedirectToPage("UserDetails", new { CurrentID = UpdateUser.Email });
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
