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
        private readonly IHttpContextAccessor contxt;
        public EventListModel(EventService eventService)
        {
            _eventService = eventService;
        }
        [BindProperty]
        public List<Event> EventList { get; set; } = new();


        public IActionResult OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");

            if (Email != null)
            {
                if (contxt.HttpContext.Session.GetString("Admin") == "Yes")
                {
                    EventList = _eventService.GetAll();
                    return Page();
                }
                else
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Unauthorized Access");
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("You have not login yet.");
                return RedirectToPage("/Index");
            }
        }

    }
}