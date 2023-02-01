using prism_trust_foundation.Pages.Events;
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class VolunteerShiftBookingService
    {
        private static List<VolunteerShiftBook>? volunteerShiftBooks;

        public List<VolunteerShiftBook> GetAll()
        {
            return volunteerShiftBooks.OrderBy(d => d.ShiftBookingId).ToList();
        }

        public VolunteerShiftBook? GetVolunteerBookingById(string id)
        {
            VolunteerShiftBook? volunteerbooking = volunteerShiftBooks.FirstOrDefault(x => x.ShiftBookingId.Equals(id));
            return volunteerbooking;
        }
    }
}
