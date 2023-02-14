using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Storage
{
    public class AddModel : PageModel
    {
        private readonly InventoryService _inventoryService;
        public AddModel(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;

        }
        [BindProperty]
        public Inventory MyInventory { get; set; } = new();
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Inventory? itemName = _inventoryService.GetInventoryByName(MyInventory.Name);
                if (itemName != null)
                {
                    ModelState.AddModelError("", "Item already exists in Inventory");
                    return Page();
                }
                else if (MyInventory.Quantity <= 0)
                {
                    ModelState.AddModelError("", "Quantity must be above 0");
                    return Page();
                }
                else
                {
                    _inventoryService.AddInventory(MyInventory);
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("{0} successfully added into inventory", MyInventory.Name);
                    ModelState.Clear();
                }
            }
            return Redirect("/Admin/Storage/Add");
        }
    }
}
