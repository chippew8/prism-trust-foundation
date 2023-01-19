using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class EventService
    {
        private readonly MyDbContext _context;
        public EventService(MyDbContext context)
        {
            _context = context;
        }

        public List<Event> GetAll()
        {
            return _context.Event.OrderBy(d => d.EventName).ToList();
        }
        public Event? GetEventById(string Id)
        {
            Event? myEvent = _context.Event.FirstOrDefault(x => x.EventId.Equals(Id));
            return myEvent;
        }

        public Event? GetEventByName(string Name)
        {
            Event? myEvent = _context.Event.FirstOrDefault(x => x.EventName.Equals(Name));
            return myEvent;
        }

        public void AddEvent(Event myEvent)
        {
            _context.Event.Add(myEvent);
            _context.SaveChanges();
        }

        public void UpdateEvent(Event myEvent)
        {
            _context.Event.Update(myEvent);
            _context.SaveChanges();
        }
        
    }
}
