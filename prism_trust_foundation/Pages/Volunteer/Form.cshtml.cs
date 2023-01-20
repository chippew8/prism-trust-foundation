using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using System.Diagnostics.Tracing;

namespace prism_trust_foundation.Pages.Volunteer
{
    public class FormModel : PageModel
    {
        private readonly VolunteerShiftService _shiftService;
        private readonly EventService _eventService;

        public FormModel(VolunteerShiftService shiftService, EventService eventService)
        {
            _shiftService = shiftService;
            _eventService = eventService;
        }

        [BindProperty]
        
        public Event BookingEvent { get; set; } = new();


        public IActionResult OnGet(int id)
        {
            Event? myEvent = _eventService.GetEventById(id);
            if(myEvent != null)
            {
                
                
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("You have signed up to volunteer at"  );
                return Redirect("/Volunteer/List");
            }
            return Page();
        }
    }
}
