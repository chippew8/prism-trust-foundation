using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.DonationRecipient.Admin
{
    public class retrieveApplicationFormModel : PageModel
    {
        private readonly UserService _userService;
        private readonly AuthDbContext _context;
        
        public retrieveApplicationFormModel(UserService userService, AuthDbContext context)
        {
            _userService = userService;
            _context = context;
        }
        public List<donationRecipient> dRlist { get; set; }

        public void OnGet()
        {
            dRlist = _context.donationRecipients.OrderBy(m => m.email).ToList();
        }

        public IActionResult OnGetAccept(string id)
        {
           var x =  _userService.GetUserByEmail(id);
            x.dRecip_Role = true;
           
            _userService.UpdateUser(x);
            _context.donationRecipients.Remove(_context.donationRecipients.FirstOrDefault(x => x.email.Equals(id)));
            _context.SaveChanges();

            return RedirectToPage("retrieveApplicationForm");
        }
        public IActionResult OnGetDelete(string id)
        {
            var x =  _context.donationRecipients.Remove(_context.donationRecipients.FirstOrDefault(x => x.email.Equals(id)));
            _context.SaveChanges();


            return RedirectToPage("retrieveApplicationForm");
        }

    }
}
