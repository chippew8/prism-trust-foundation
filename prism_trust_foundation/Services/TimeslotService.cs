using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class TimeslotService
    {
        private readonly AuthDbContext _context;
        public TimeslotService(AuthDbContext context)
        {
            _context = context;
        }

        public List<Timeslot> GetAll()
        {
            return _context.Timeslot.OrderBy(d => d.Shift_Start).ToList();
        }

        public List<Timeslot> GetTimeslotsByEventid(int EventId)
        {
            return _context.Timeslot.Where(d => d.Equals(EventId)).ToList();
        }

        public Timeslot? GetTimeslotById(int Id)
        {
            Timeslot? EventTimeslots = _context.Timeslot.FirstOrDefault(x => x.TimeslotId.Equals(Id));
            return EventTimeslots;
        }
        
        public void AddTimeslot(Timeslot myTimeslot)
        {
            _context.Timeslot.Add(myTimeslot);
            _context.SaveChanges();
        }

        public void UpdateTimeslot(Timeslot myTimeslot)
        {
            _context.Timeslot.Update(myTimeslot);
            _context.SaveChanges();
        }
    }
}
