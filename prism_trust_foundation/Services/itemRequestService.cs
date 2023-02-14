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
        }
    }
