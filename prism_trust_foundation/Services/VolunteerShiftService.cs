using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class VolunteerShiftService
    {

        private static List<VolunteerShift> AllShifts = new()
        {
        new VolunteerShift{ Shift_Start = "1", Shift_End = "2", Shift_Quantity = 10},
        new VolunteerShift{ Shift_Start = "2", Shift_End = "3", Shift_Quantity = 2},
        new VolunteerShift{ Shift_Start = "3", Shift_End = "4", Shift_Quantity = 13},
        new VolunteerShift{ Shift_Start = "4", Shift_End = "5", Shift_Quantity = 0}
        };

        public List<VolunteerShift> GetAll()
        {
            return AllShifts.OrderBy(d => d.Shift_Start).ToList();
        }

        public VolunteerShift? GetvolunteerShiftByStart(string start_time)
        {
            VolunteerShift volunteerShift = AllShifts.FirstOrDefault(d => d.Shift_Start == start_time);
            return volunteerShift;
        }
    }
}
