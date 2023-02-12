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
        public AdminUserDetailsModel(ILogger<AdminUserDetailsModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public ApplicationUser AdminHomeUser { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            ApplicationUser? user = _svc.GetUserByEmail(id);
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
            ApplicationUser? user = _svc.GetUserByEmail(AdminHomeUser.Email);
            if (ModelState.IsValid)
            {
                if (user != null)
                {
                    if (user.Email == AdminHomeUser.Email)
                    {
                        if (sessionCount == 1)
                        {
                            if (AdminHomeUser.Ban_Status == false)
                            {
                                AdminHomeUser.Ban_Status = true;
                                _svc.UpdateUser(AdminHomeUser);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0}'s status is updated", AdminHomeUser.Email);
                                return RedirectToPage("Index");
                            }
                            else
                            {
                                AdminHomeUser.Ban_Status = false;
                                _svc.UpdateUser(AdminHomeUser);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0}'s status is updated", AdminHomeUser.Email);
                                return RedirectToPage("Index");
                            }
                        }
                        else if (sessionCount == 2)
                        {
                            if(AdminHomeUser.Admin_Role == false)
                            {
                                AdminHomeUser.Admin_Role = true;
                                _svc.UpdateUser(AdminHomeUser);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0} is updated", AdminHomeUser.Email);
                                return RedirectToPage("Index");
                            }
                            else
                            {
                                AdminHomeUser.Admin_Role = false;
                                _svc.UpdateUser(AdminHomeUser);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0} is updated", AdminHomeUser.Email);
                                return RedirectToPage("Index");
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
