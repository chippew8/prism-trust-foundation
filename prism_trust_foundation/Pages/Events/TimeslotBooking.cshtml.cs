using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Events
{
    public class TimeslotBookingModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly TimeslotService _timeslotService;
        private readonly TimeslotBookingService _timeslotbookingService;
        public TimeslotBookingModel(EventService eventService, TimeslotService timeslotService, TimeslotBookingService timeslotbookingService)
        {
            _eventService = eventService;
            _timeslotService = timeslotService;
            _timeslotbookingService = timeslotbookingService;
        }

        [BindProperty]
        Event? MyEvent { get; set; } = new();
        [BindProperty]
        Timeslot myTimeslot { get; set; } = new();
        [BindProperty]
        public List<Timeslot> TimeslotsList { get; set; } = new();
        [BindProperty]
        public TimeslotBooking? myTimeslotBooking { get; set; } = new();
        [BindProperty]
        public string BookType { get; set; }
        public int BookTime { get; set; }
        public IActionResult OnGet(int id)
        {
            Event? myEvent = _eventService.GetEventById(id);
            
            if (myEvent != null)
            {
                MyEvent = myEvent;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Event does not exist");
                return Redirect("/Admin/EventList");
            }

        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Event? events = _eventService.GetEventByName(MyEvent.EventName);
                myTimeslot = _timeslotService.GetTimeslotByTimeandType(BookTime, BookType);
                if (events != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Event already exists");
                    return Page();
                }
                
                myTimeslotBooking.EventId = MyEvent.EventId;
                myTimeslotBooking.ShiftId = myTimeslot.TimeslotId;
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is added", MyEvent.EventName);
                return Redirect("/Admin/Events/EventList");
            }
            return Page();
        }
    }
}
