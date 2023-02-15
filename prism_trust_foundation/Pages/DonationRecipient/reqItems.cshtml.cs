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
        private readonly IHttpContextAccessor _contxt;
            private readonly ILogger<IndexModel> _logger;
            public List<Inventory> Products { get; set; }
            public List<cart> MyCart { get; set; }
            public reqItemsModel( cartService cartServices, itemRequestService itemRequestService, InventoryService inventoryService, IHttpContextAccessor httpContextAccessor)
            {
            _inventoryService =  inventoryService;
                _cartServices = cartServices;
                _itemRequestService = itemRequestService;
            _contxt = httpContextAccessor;
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
            public Inventory FindbyId(string? id)
            {

            int x = (int)Convert.ToInt64(id);
                return _inventoryService.GetInventoryById(x);
            }
            public IActionResult OnGetBuyNow(string id)
            {

            var email = _contxt.HttpContext.Session.GetString("Email");


                var newCart = _cartServices.CheckProductById(id);
                if (newCart == null)
                {
                    _cartServices.AddCart(new cart
                    {
                        Id = (_cartServices.GetAll().Count() + 1).ToString(),
                        productId = id,
                        quantity = 1,
                        userId = email
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
                    _itemRequestService.AddRequest(new itemRequest
                    {
                        Id = (_itemRequestService.GetAll().Count() + x).ToString(),
                        productId = i.productId,
                        quantity = i.quantity,
                        userId = i.userId,
                    }
                      );
                    x += 1;
                }
                _cartServices.removeAll();


               




                return RedirectToPage("reqItems");
            }

        }

    }

