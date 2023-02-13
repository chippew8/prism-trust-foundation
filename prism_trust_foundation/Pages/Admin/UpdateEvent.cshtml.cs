using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin
{
    public class UpdateModel : PageModel
    {
        private readonly EventService _eventService;

        public UpdateModel(EventService eventService)
        {
            _eventService = eventService;
        }
        [BindProperty]
        public Event MyEvent { get; set; } = new();

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
                return Redirect("/Events/EventList");
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _eventService.UpdateEvent(MyEvent);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is updated", MyEvent.EventName);
                return Redirect("/Events/EventList");
            }
            return Page();
        }



    }
}
