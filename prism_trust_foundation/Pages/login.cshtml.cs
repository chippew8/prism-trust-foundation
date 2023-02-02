using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace prism_trust_foundation.Pages
{
    public class loginModel : PageModel
    {
        private readonly ILogger<loginModel> _logger;
        private UserService _svc;
        public loginModel(ILogger<loginModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public Models.User CurrentUser { get; set; } = new();

        [BindProperty]
        public string? MyMessage { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Models.User? user = _svc.GetUserById(CurrentUser.Email);
            if (user != null)
            {
                if(user.Password == CurrentUser.Password)
                {
                    if (user.Status == "Activated")
                    {
                        if (user.Role == "User")
                        {
                            return RedirectToPage("User/UserDetails", new { CurrentID = CurrentUser.Email });
                        }
                        else if (user.Role == "Admin")
                        {
                            return RedirectToPage("Admin/Index", new { CurrentID = CurrentUser.Email });
                        }
                        else
                        {
                            return Page();
                        }
                    }
                    else
                    {
                        MyMessage = "Your account has been deactivated!";
                        return Page();
                    }
                }
                else
                {
                    MyMessage = "Wrong Password!";
                    return Page();
                }
                
            }
            else
            {
                MyMessage = "Your account does not exist, please register and account if you don't have!";
                return Page();
            }
        }
    }
}
