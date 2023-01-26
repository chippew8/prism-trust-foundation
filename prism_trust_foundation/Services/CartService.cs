using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class CartService
    {
        private static List<Item> cart = new()
        {
            
        };

        public List<Item> GetAll()
        {
            return cart.ToList();
        }

        public void RemoveItem(Item x)
        {
            cart.Remove(x);
        }
        public void AddItem(Item x)
        {
            cart.Add(x);
        }
    }
}
