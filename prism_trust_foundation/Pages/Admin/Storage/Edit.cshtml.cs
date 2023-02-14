using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Storage
{
    public class EditModel : PageModel
    {
        [BindProperty]
        public Inventory edititem { get; set; } = new();
        private readonly InventoryService _inventoryService;
        public EditModel(InventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }
        public IActionResult OnGet(int id)
        {
            Inventory? myInventory = _inventoryService.GetInventoryById(id);
            if (myInventory == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Item does not exist in Inventory");
                return Redirect("/Admin/Storage/Index");
            }
            else
            {
                edititem = myInventory;
                return Page();
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _inventoryService.UpdateInventory(edititem);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("{0} successfully editted", edititem.Name);
                return Redirect("/Admin/Storage/Index");
            }
            return Page();
        }
    }
}