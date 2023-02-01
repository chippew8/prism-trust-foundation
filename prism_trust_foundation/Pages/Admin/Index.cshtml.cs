using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Pages.Admin;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<AdminUserDetailsModel> _logger;
        private readonly UserService _svc;
        public IndexModel(UserService employeeService)
        {
            _svc = employeeService;
        }
        public List<Models.User> UserList { get; set; } = new();
        public void OnGet()
        {
            UserList = _svc.GetAll();
        }
    }
}
