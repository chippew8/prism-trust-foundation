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
        private readonly IHttpContextAccessor _contxt;
        public TimeslotBookingModel(EventService eventService, TimeslotService timeslotService, TimeslotBookingService timeslotbookingService, IHttpContextAccessor contxt)
        {
            _eventService = eventService;
            _timeslotService = timeslotService;
            _timeslotbookingService = timeslotbookingService;
            _contxt = contxt;
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
                string Email = _contxt.HttpContext.Session.GetString("Email");
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
                myTimeslotBooking.UserEmail = Email;
                _timeslotbookingService.AddBooking(myTimeslotBooking);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Booking is successful");
                return Redirect("/Index");
            }
            return Page();
        }
    }
}
