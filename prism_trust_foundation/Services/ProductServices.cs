
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class ProductServices
    {
   
        private readonly AuthDbContext _context;
        public ProductServices(AuthDbContext context)
        {
            _context = context;
        }
        public List<Product> GetAll()
        {
            return _context.Products.OrderBy(m => m.Name).ToList();
        }
        public Product? GetProductById(string id)
        {
            Product? product = _context.Products.FirstOrDefault(
            x => x.Id.Equals(id));
            return product;
        }
        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public void UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}