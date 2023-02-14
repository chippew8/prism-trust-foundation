using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin.Timeslots
{
    public class UpdateTimeslotModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly TimeslotService _timeslotService;

        public UpdateTimeslotModel(EventService eventService, TimeslotService timeslotService)
        {
            _eventService = eventService;
            _timeslotService = timeslotService;
        }
        [BindProperty]
        public Timeslot MyTimeslot { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            Timeslot? myTimeslot = _timeslotService.GetTimeslotById(id);
            if (myTimeslot != null)
            {
                MyTimeslot = myTimeslot;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Event does not exist");
                return Redirect("/Admin/EventList");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _timeslotService.UpdateTimeslot(MyTimeslot);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is updated", MyTimeslot.TimeslotId);
                return Redirect("/Admin/Events/EventList");
            }
            return Page();
        }



    }
}

