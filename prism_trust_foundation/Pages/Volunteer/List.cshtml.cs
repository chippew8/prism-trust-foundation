using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Pages.Volunteer
{
    public class VolunteerListModel : PageModel
    {
        public List<Event> EventList { get; set; } = new()
        {
            new Event {EventName = "Event1", Description = "Example Event", EventDate = "01-01-2023", EventType = "Donation Drive", EventVenue = "Nanyang Polytechnic"},
            new Event {EventName = "Event2", Description = "Second Example Event", EventDate = "02-01-2023", EventType = "Recycling Drive", EventVenue = "Republic Polytechnic"},
            new Event {EventName = "Event3", Description = "Third Example Event", EventDate = "03-01-2023", EventType = "Recycling Drive", EventVenue = "Temasek Polytechnic"},
            new Event {EventName = "Event4", Description = "Fourth Example Event", EventDate = "04-01-2023", EventType = "Donation Drive", EventVenue = "Ngee Ann Polytechnic"}
        };
        public void OnGet()
        {
        }
    }

}
