using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin.Timeslots
{
    public class TimeslotListModel : PageModel
    {
        private readonly TimeslotService _timeslotService;
        private readonly EventService _eventService;
        public TimeslotListModel(TimeslotService timeslotService, EventService eventService)
        {
            _timeslotService = timeslotService;
            _eventService = eventService;
        }
        [BindProperty]
        public List<Timeslot> TimeslotsList { get; set; } = new();
        [BindProperty]
        public List<Event> EventList { get; set; } = new();

        [BindProperty]
        public int Target { get; set; }
        public IActionResult OnGet(int id)
        {
            TimeslotsList = _timeslotService.GetTimeslotsByEventId(id);
            
            return Page();
        }
    }
}
