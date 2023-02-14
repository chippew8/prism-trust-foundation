using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Services;
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Pages.Storage
{
    public class IndexModel : PageModel
    {
        private readonly InventoryService _inventoryService;
        public IndexModel(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        public List<Inventory> InventoryList { get; set; } = new();
        [BindProperty]
        public Inventory MyInventory { get; set; } = new();
        public Inventory removeitem { get; set; } = new();
        public void OnGet()
        {
            InventoryList = _inventoryService.GetAll();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Inventory? myInventory = _inventoryService.GetInventoryById(MyInventory.InventoryId);
                if (myInventory == null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Item does not exist in Inventory");
                    return Redirect("/Admin/Storage/Index");
                }
                else
                {
/*                    removeitem.Name = myInventory.Name;
                    removeitem.Category = myInventory.Category;
                    removeitem.Quantity = myInventory.Quantity;*/
                    TempData["FlashMessage.Type"] = "success";
                    TempData["FlashMessage.Text"] = string.Format("Removed {0} successfully", myInventory.Name);
                    _inventoryService.DeleteInventory(myInventory);
                }
            }
            return Page();
        }
    }
}
