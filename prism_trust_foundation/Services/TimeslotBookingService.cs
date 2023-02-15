using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class TimeslotBookingService
    {
        private readonly AuthDbContext _context;
        public TimeslotBookingService(AuthDbContext context)
        {
            _context = context;
        }

        public List<TimeslotBooking> GetAll()
        {
            return _context.TimeslotBooking.OrderBy(d => d.TimeslotBookingId).ToList();
        }

        public List<TimeslotBooking> GetTimeslotBookingByEventId(int EventId)
        {
            return _context.TimeslotBooking.Where(d => d.EventId.Equals(EventId)).ToList();
        }

        public void AddBooking(TimeslotBooking myTimeslotBooking)
        {
            _context.TimeslotBooking.Add(myTimeslotBooking);
            _context.SaveChanges();
        }

        public void UpdateBooking(TimeslotBooking myTimeslotBooking)
        {
            _context.TimeslotBooking.Update(myTimeslotBooking);
            _context.SaveChanges();
        }
    }
}
