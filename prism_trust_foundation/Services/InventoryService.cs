using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class InventoryService
    {
        private readonly AuthDbContext _context;
        public InventoryService(AuthDbContext context)
        {
            _context = context;
        }

        public List<Inventory> GetAll()
        {
            return _context.Inventory.OrderBy(d => d.InventoryId).ToList();
        }
        public Inventory? GetInventoryByName(string Name)
        {
            Inventory? itemName = _context.Inventory.FirstOrDefault(i => i.Name.Equals(Name));
            return itemName;
        }
        public void AddInventory(Inventory myInventory)
        {
            _context.Inventory.Add(myInventory);
            _context.SaveChanges();
        }

        public void UpdateInventory(Inventory myInventory)
        {
            _context.Inventory.Update(myInventory);
            _context.SaveChanges();
        }
    }
}
