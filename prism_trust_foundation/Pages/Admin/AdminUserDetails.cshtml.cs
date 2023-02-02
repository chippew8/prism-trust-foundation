using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin
{
    public class AdminUserDetailsModel : PageModel
    {
        private readonly ILogger<AdminUserDetailsModel> _logger;
        private UserService _svc;
        private UserService _user;
        public AdminUserDetailsModel(ILogger<AdminUserDetailsModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
            _user = service;
        }

        [BindProperty]
        public Models.User AdminHomeUser { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            Models.User? user = _svc.GetUserById(id);
            if (user != null)
            {
                AdminHomeUser = user;
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
                    if (AdminHomeUser.Status == "Activated")
                    {
                        AdminHomeUser.Status = "Deactivated";
                        _user.UpdateUser(AdminHomeUser);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0}'s status is updated", AdminHomeUser.Email);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        AdminHomeUser.Status = "Activated";
                        _user.UpdateUser(AdminHomeUser);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0}'s status is updated", AdminHomeUser.Email);
                        return RedirectToPage("Index");
                    }
                }
                else if (sessionCount == 2)
                {
                    if (AdminHomeUser.Role == "User")
                    {
                        AdminHomeUser.Role = "Admin";
                        _user.UpdateUser(AdminHomeUser);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0} is updated", AdminHomeUser.Email);
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        AdminHomeUser.Role = "User";
                        _user.UpdateUser(AdminHomeUser);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0} is updated", AdminHomeUser.Email);
                        return RedirectToPage("Index");
                    }

                }
                else
                {
                    _svc.UpdateUser(AdminHomeUser);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("User {0} is updated", AdminHomeUser.Email);
                    return RedirectToPage("Index");
                }
            }
            return Page();
        }
    }
}
