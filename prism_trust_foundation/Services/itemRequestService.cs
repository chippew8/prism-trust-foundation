using Microsoft.EntityFrameworkCore;
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class itemRequestService
    {
        
            private readonly AuthDbContext _context;
            public itemRequestService(AuthDbContext context)
            {
                _context = context;
            }
            public List<itemRequest> GetAll()
            {
                return _context.itemRequests.ToList();
            }
            public void AddRequest(itemRequest x)
            {
                _context.itemRequests.Add(x);
            }
        public itemRequest? GetItemReqById(int id)
        {
            itemRequest? itemId = _context.itemRequests.FirstOrDefault(i => i.productId.Equals(id));
            return itemId;
        }
    }

    }
