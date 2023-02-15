using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Events
{
    public class EventListModel : PageModel
    {
        private readonly EventService _eventService;
        public EventListModel(EventService eventService)
        {
            _eventService = eventService;
        }
        [BindProperty]
        public List<Event> EventList { get; set; } = new();

        public void OnGet()
        {
            EventList = _eventService.GetAll();
        }
        


    }
}