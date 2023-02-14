using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin.Timeslots
{
    public class AddTimeslotModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly TimeslotService _TimeslotService;
        private IWebHostEnvironment _environment;
        public AddTimeslotModel(EventService eventService, IWebHostEnvironment environment, TimeslotService timeslotService)
        {
            _eventService = eventService;
            _environment = environment;
            _TimeslotService = timeslotService;
        }
        [BindProperty]
        public Event MyEvent { get; set; } = new();
        [BindProperty]
        public Timeslot myTimeslot { get; set; } = new();
        public Event? myEvent { get; set; }
        public IActionResult OnGet(int eventId)
        {
            myEvent = _eventService.GetEventById(eventId);
            if(myEvent != null)
            {
                MyEvent = myEvent;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Event does not exist");
                return Redirect("/Admin/Events/EventList");
            }
            
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Timeslot? timeslots = _TimeslotService.GetTimeslotById(myTimeslot.TimeslotId);
                if (timeslots != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Event already exists");
                    return Page();
                }
                myTimeslot.EventId = myEvent.EventId;
                _TimeslotService.AddTimeslot(myTimeslot);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Timeslot for event {0} is added", myTimeslot.TimeslotId);
                return Redirect("/Admin/Events/EventList");
            }
            return Page();
        }
    }
}
