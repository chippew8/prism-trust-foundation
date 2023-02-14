using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin.DonationRecipient
{
    public class retrieveItemReqModel : PageModel
    {
        private readonly itemRequestService _itemRequestService;
        private readonly InventoryService _inventoryService;
        private readonly UserService _userService;
        public retrieveItemReqModel(itemRequestService itemRequestService, InventoryService inventoryService, UserService userService)
        {
            _itemRequestService = itemRequestService;
            _inventoryService = inventoryService;
            _userService = userService;
        }
        [BindProperty]
        public List<itemRequest> itemReqList { get; set; } = new();

        public void OnGet()
        {
            itemReqList = _itemRequestService.GetAll();

        }
        public Inventory FindbyId(string? id)
        {
            return _inventoryService.GetInventoryById(id);
        }
        public ApplicationUser FindUserbyId(string? id)
        {
            return _userService.GetUserByEmail(id);
        }
    }
}
