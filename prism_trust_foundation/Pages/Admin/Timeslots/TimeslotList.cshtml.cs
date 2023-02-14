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
        public List<Timeslot> TimeslotList { get; set; } = new();
        [BindProperty]
        public int TargetId { get; set; }
        public IActionResult OnGet(int eventId)
        {
            TargetId = eventId;
            TimeslotList = _timeslotService.GetTimeslotsByEventId(eventId);
            if (TimeslotList != null)
            {
                
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Event does not exist");
                return Redirect("/Admin/EventList");
            }
        }
    }
}
