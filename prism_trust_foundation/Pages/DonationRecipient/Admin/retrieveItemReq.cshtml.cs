using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.DonationRecipient.Admin
{

    
    public class retrieveItemReqModel : PageModel
    {
        private readonly AuthDbContext _context;
        public readonly InventoryService _inventoryService;
        public retrieveItemReqModel(AuthDbContext context , InventoryService inventoryService)
        {
            _context = context;
            _inventoryService = inventoryService;
        }
        public List<itemRequest> itemReqs { get; set; }

        public void OnGet()
        {
            itemReqs = _context.itemRequests.OrderBy(m => m.Id).ToList();
        }
    }
}
