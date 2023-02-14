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
            try
            {
                return _context.itemRequests.ToList();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            }
            public void AddRequest(itemRequest x)
            {
                _context.itemRequests.Add(x);
            }
        public itemRequest CheckReqById(string id)
        {
            try
            {
                itemRequest product = _context.itemRequests.FirstOrDefault(x => x.productId.Equals(id));

                return product;


            }
            catch
            {
                return null;
            }


        }
        public void UpdateReq(itemRequest itemRequest)
        {
            _context.itemRequests.Update(itemRequest);
            _context.SaveChanges();
        }
    }
    }
