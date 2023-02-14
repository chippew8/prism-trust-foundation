using Microsoft.EntityFrameworkCore;
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class cartService
    {
        private readonly AuthDbContext _context;
        public cartService(AuthDbContext context)
        {
            _context = context;
        }
        public List<cart> GetAll()
        {
            try
            {
                return _context.cart.OrderBy(d => d.Id).ToList();
            }
            catch(InvalidOperationException)
            {
                return null;
            }
        }
        public void AddCart(cart cart)
        {
            _context.cart.Add(cart);
            _context.SaveChanges();
        }
        public void removeAll()
        {
            _context.cart.ExecuteDelete();
            _context.SaveChanges();
        }

        public cart CheckProductById(string id)
        {
            try
            {
                cart product = _context.cart.FirstOrDefault(x => x.productId.Equals(id));
                return product;
            }
            catch
            {
                return null;
            }


        }


        public void UpdateCart(cart cart)
        {
            _context.cart.Update(cart);
            _context.SaveChanges();
        }

    }
}
