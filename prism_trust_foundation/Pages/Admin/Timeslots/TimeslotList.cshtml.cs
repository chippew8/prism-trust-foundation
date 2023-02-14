using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin.Timeslots
{
    public class TimeslotListModel : PageModel
    {
        private readonly TimeslotService _timeslotService;
        public TimeslotListModel(TimeslotService timeslotService)
        {
            _timeslotService = timeslotService;
        }
        [BindProperty]
        public List<Timeslot> TimeslotList { get; set; } = new(); 

        public IActionResult OnGet(int eventId)
        {
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
