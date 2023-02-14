using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;


namespace prism_trust_foundation.Pages.DonationRecipient
{
    public class reqItemsModel : PageModel
    {



            private readonly ProductServices _productService;
            private readonly cartService _cartServices;
            private readonly itemRequestService _itemRequestService;
            private readonly ILogger<IndexModel> _logger;
            public List<Product> Products { get; set; }
            public List<cart> MyCart { get; set; }
            public reqItemsModel(ProductServices productService, cartService cartServices, itemRequestService itemRequestService)
            {
                _productService = productService;
                _cartServices = cartServices;
                _itemRequestService = itemRequestService;
            }


            public void OnGet()
            {
                Products = _productService.GetAll();
                try
                {
                    MyCart = _cartServices.GetAll();
                }
                catch
                {
                    MyCart = null;
                }
            }
            public Product FindbyId(string? id)
            {
                return _productService.GetProductById(id);
            }
            public IActionResult OnGetBuyNow(string id)
            {
                var newCart = _cartServices.CheckProductById(id);
                if (newCart == null)
                {
                    _cartServices.AddCart(new cart
                    {
                        Id = (_cartServices.GetAll().Count() + 1).ToString(),
                        productId = id,
                        quantity = 1,
                        userId = "1"
                    }
                        );




                }
                else
                {
                    newCart.quantity += 1;
                    _cartServices.UpdateCart(newCart);

                }
                return RedirectToPage("Index");
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


                Console.WriteLine("Failed");




                return RedirectToPage("Index");
            }

        }

    }
