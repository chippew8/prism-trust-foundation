using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using SendGrid.Helpers.Mail;

namespace prism_trust_foundation.Pages.Admin.Timeslots
{
    public class TimeslotListModel : PageModel
    {
        private readonly TimeslotService _timeslotService;
        private readonly EventService _eventService;
        private readonly IHttpContextAccessor contxt;
        public TimeslotListModel(TimeslotService timeslotService, EventService eventService, IHttpContextAccessor context)
        {
            _timeslotService = timeslotService;
            _eventService = eventService;
            contxt = context;
            this.contxt = contxt;
        }
        [BindProperty]
        public List<Timeslot> TimeslotsList { get; set; } = new();
        [BindProperty]
        public List<Event> EventList { get; set; } = new();

        [BindProperty]
        public int Target { get; set; }
        public IActionResult OnGet(int id)
        {
            string Email = contxt.HttpContext.Session.GetString("Email");

            if (Email != null)
            {
                if (contxt.HttpContext.Session.GetString("Admin") == "Yes")
                {
                    TimeslotsList = _timeslotService.GetTimeslotsByEventId(id);
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
