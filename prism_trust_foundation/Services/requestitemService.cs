using prism_trust_foundation.Models;
namespace prism_trust_foundation.Services

{
    public class requestitemsService
    {
        private static List<Item> RequestDb = new()
        {
            
        };
        public List<Item> GetAll()
        {
            return RequestDb.ToList();
        }
        public void AddRequest(Item x)
        {
            RequestDb.Add(x);
        }
    }
}
