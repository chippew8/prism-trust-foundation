using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;


namespace prism_trust_foundation.Pages.DonationRecipient
{
    public class reqItemsModel : PageModel
    {



            private readonly InventoryService _inventoryService;
            private readonly cartService _cartServices;
            private readonly itemRequestService _itemRequestService;
            private readonly UserService _userService;
            private readonly IHttpContextAccessor contxt;
        public List<Inventory> Products { get; set; }
            public List<cart> MyCart { get; set; }
            public string userId { get; set; }
            public reqItemsModel(InventoryService inventoryService, cartService cartServices, itemRequestService itemRequestService, IHttpContextAccessor contextAccessor, UserService userService)
            {
                _inventoryService =inventoryService;
                _cartServices = cartServices;
                _itemRequestService = itemRequestService;
                _userService = userService;
                contxt = contextAccessor;
            }

             
            public void OnGet()
            {
            
            
            Products = _inventoryService.GetAll();
            try
            {

                MyCart = _cartServices.GetAll();
            }

            catch
            {
                MyCart = null;
            }
}
            public Inventory FindbyId(string id)
            {
                return _inventoryService.GetInventoryById(id);
            }
            public IActionResult OnGetBuyNow(string id)
            {
            
            string Email = contxt.HttpContext.Session.GetString("Email");
           

            var newCart = _cartServices.CheckProductById(id);
                
            if (newCart == null)
                {
                    _cartServices.AddCart(new cart
                    {
                        Id = (_cartServices.GetAll().Count() + 1).ToString(),
                        productId = id,
                        quantity = 1,
                        userId = Email
                    }
                        );

                }
                else
                {
                    newCart.quantity += 1;
                    _cartServices.UpdateCart(newCart);

                }
                return RedirectToPage("reqItems");
            }

            public IActionResult OnGetSubmitReq(string id)
            {

                MyCart = _cartServices.GetAll();
            var x = 1;
            foreach (var i in MyCart)
            {
                var newReq = _itemRequestService.CheckReqById(i.productId);

                if (newReq == null)
                {
                    _itemRequestService.AddRequest(new itemRequest
                    {
                        Id = (_itemRequestService.GetAll().Count() + x).ToString(),
                        productId = i.productId,
                        quantity = i.quantity,
                        userId = id
                    }
                          );
                    x += 1;



                }
                else
                {
                    newReq.quantity += i.quantity;
                    _itemRequestService.UpdateReq(newReq);

                }
            }
            _cartServices.removeAll();
                return RedirectToPage("reqItems");

            }



        public IActionResult OnGetDelete(string id)
        {
            _cartServices.remProductById(id);
            return RedirectToPage("reqItems");
        }


    }

    }

