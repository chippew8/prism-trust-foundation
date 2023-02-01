using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace prism_trust_foundation.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly ILogger<RegisterModel> _logger;
        private UserService _svc;
        public RegisterModel(ILogger<RegisterModel> logger, UserService service)
        {
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public Models.User MyUser { get; set; }

        [BindProperty]
        public string? MyMessage { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            MyUser.Role = "User";
            MyUser.Status = "Activated";
            var confirmPass = Request.Form["confirmPass"];
            if (ModelState.IsValid)
            {
                if (MyUser.Password != confirmPass)
                {
                    MyMessage = "Password are not matched!";
                }
                else if (_svc.AddUser(MyUser))
                {
                    return RedirectToPage("login");
                }
                else
                {
                    MyMessage = "User already exist!";
                    return Page();
                }
            }
            return Page();
        }

    }
}
