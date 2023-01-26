
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages
{
    public class RequestitemsModel : PageModel
    {
        private readonly CartService _cartService;
        private readonly requestitemsService _requestService;
        public RequestitemsModel(CartService cartService, requestitemsService requestService)
        {
            _cartService = cartService;
            _requestService = requestService;
        }


        [BindProperty]
        public Item MyCart { get; set; } = new();
        

        public List<Item> CartList { get; set; } = new();
        
            
        


        public void OnGet()
        {
            CartList = _cartService.GetAll();
        }
/*On get handler*/
        public void OnGetAddToCart(string productID)
        {

        }
        public IActionResult OnPost()
        {

            
            if (ModelState.IsValid)
            {
                _cartService.AddItem(MyCart);
                return Redirect("/requestitems");
            }
            return Page();
        }
    }
}