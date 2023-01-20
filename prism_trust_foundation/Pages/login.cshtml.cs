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
                    return RedirectToPage("User/UserDetails", new { CurrentID = CurrentUser.Email});
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
