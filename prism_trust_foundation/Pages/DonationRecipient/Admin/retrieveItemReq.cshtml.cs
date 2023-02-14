using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.DonationRecipient.Admin
{
    public class retrieveItemReqModel : PageModel
    {
        private readonly itemRequestService _itemRequestService;
        private readonly ProductServices _productServices;
        private readonly UserService _userService;
        public retrieveItemReqModel(itemRequestService itemRequestService, ProductServices productServices, UserService userService)
        {
            _itemRequestService = itemRequestService;
            _productServices = productServices;
            _userService = userService;
        }
        [BindProperty]
        public List<itemRequest> itemReqList { get; set; } = new();

        public void OnGet()
        {
            itemReqList = _itemRequestService.GetAll();

        }
        public Product FindbyId(string? id)
        {
            return _productServices.GetProductById(id);
        }
        public ApplicationUser FindUserbyId(string? id)
        {
            return _userService.GetUserByEmail(id);
        }
    }
}
