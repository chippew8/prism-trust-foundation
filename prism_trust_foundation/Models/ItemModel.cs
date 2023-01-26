using prism_trust_foundation.Models;
using LearnASPNETCoreRazorPagesWithRealApps.Entities;
using System.Collections.Generic;
using System.Linq;


namespace prism_trust_foundation.Models
{
    public class ItemModel
    {
        private List<Product> Products;

        public ItemModel()
        {
            Products = new List<Product>() {
                new Product
                {
                    Id = "1",
                    Name = "Shirt",
                    Category = "clothing",
                    Quantity = 1,
                    Description = "t-shirt"
                },
                new Product
                {
                   Id = "2",
                    Name = "Coffee",
                    Category = "non-perishable food",
                    Quantity = 1,
                    Description = ""
                },
                new Product
                {
                    Id = "3",
                    Name = "Canned Food",
                    Category = "Non-perishable food",
                    Quantity = 1,
                    Description = ""
                }
            };
        }

        public List<Product> findAll()
        {
            return Products;
        }

        public Product find(string id)
        {
            return Products.Where(p => p.Id == id).FirstOrDefault();
        }

    }
}