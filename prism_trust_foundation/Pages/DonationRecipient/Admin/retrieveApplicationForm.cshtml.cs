using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.DonationRecipient.Admin
{




    public class retrieveApplicationFormModel : PageModel

    {
        private readonly AuthDbContext _context;
        private readonly UserService _userService;
        public retrieveApplicationFormModel(AuthDbContext context, UserService userService)
        {
            _context = context;
            _userService = userService;

        }

        [BindProperty]
        public List<donationRecipient> dRecipient { get; set; }

        public void OnGet()
        {
            try
            {
                dRecipient = _context.donationRecipients.ToList();
            }
            catch (InvalidOperationException)
            {
                dRecipient = null;
            }

        }
        public IActionResult OnGetAccept(string id)
        {
            Console.WriteLine("dscxdfd");
            var x = _userService.GetUserByEmail(id);
            x.dRecip_Role = true;
            _context.donationRecipients.ExecuteDelete();
            _context.SaveChanges();
        
        



            return RedirectToPage("/DonationRecipient/Admin/retrieveApplicationForm");
        }
    }
}
