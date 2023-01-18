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

        public List<Event> EventList { get; set; } = new()
        {
            
            new Event {EventId = "1", EventName = "Event1", Description = "Example Event", EventDate = "01-01-2023", EventType = "Donation Drive", EventVenue = "Nanyang Polytechnic"},
            new Event {EventId = "2", EventName = "Event2", Description = "Second Example Event", EventDate = "02-01-2023", EventType = "Recycling Drive", EventVenue = "Republic Polytechnic"},
            new Event {EventId = "3", EventName = "Event3", Description = "Third Example Event", EventDate = "03-01-2023", EventType = "Recycling Drive", EventVenue = "Temasek Polytechnic"},
            new Event {EventId = "4", EventName = "Event4", Description = "Fourth Example Event", EventDate = "04-01-2023", EventType = "Donation Drive", EventVenue = "Ngee Ann Polytechnic"}
        };
        public void OnGet()
        {
            EventList = _eventService.GetAll();
        }
    }
}
