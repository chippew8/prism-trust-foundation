using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Events
{
    public class AddModel : PageModel
    {
        private readonly EventService _eventService;

        public AddModel(EventService eventService)
        {
            _eventService = eventService;
        }
        [BindProperty]
        public Event MyEvent { get; set; } = new();

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Event? events = _eventService.GetEventById(MyEvent.EventId);
                return Redirect("/Events/EventList");
                if (events != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Event already exists");
                    return Page();
                }
                _eventService.AddEvent(MyEvent);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is added", MyEvent.EventName);
                return Redirect("/Events/EventList");
            }
            return Page();
        }
    }
}
