using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin
{
    public class AdminUserDetailsModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signinManager;

        private readonly IHttpContextAccessor contxt;

        private UserService _svc;

        public AdminUserDetailsModel(UserService service, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor)
        {
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
        }

        [BindProperty]
        public ApplicationUser AdminHomeUser { get; set; } = new();

        public IActionResult OnGet(string id)
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
            if (user != null)
            {
                if (contxt.HttpContext.Session.GetString("Admin") == "Yes")
                {
                    ApplicationUser? selected = _svc.GetUserByEmail(id);
                    AdminHomeUser = selected;
                    return Page();
                }
                else
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("You have not an Admin.");
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
                            if (user.Ban_Status == false)
                            {
                                user.Ban_Status = true;
                                _svc.UpdateUser(user);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0}'s status is updated", user.Email);
                                return RedirectToPage("Index");
                            }
                            else
                            {
                                user.Ban_Status = false;
                                _svc.UpdateUser(user);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0}'s status is updated", user.Email);
                                return RedirectToPage("Index");
                            }
                        }
                        else if (sessionCount == 2)
                        {
                            if(user.Admin_Role == false)
                            {
                                user.Admin_Role = true;
                                _svc.UpdateUser(user);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0} is updated", user.Email);
                                return RedirectToPage("Index");
                            }
                            else
                            {
                                user.Admin_Role = false;
                                _svc.UpdateUser(user);
                                TempData["FlashMessage.Type"] = "success";
                                TempData["FlashMessage.Text"] = string.Format("User {0} is updated", user.Email);
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
