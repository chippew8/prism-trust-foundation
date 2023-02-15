using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin
{
    public class UpdateModel : PageModel
    {
        private readonly EventService _eventService;
        private readonly IHttpContextAccessor contxt;

        public UpdateModel(EventService eventService)
        {
            _eventService = eventService;
        }
        [BindProperty]
        public Event MyEvent { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            string Email = contxt.HttpContext.Session.GetString("Email");

            if (Email != null)
            {
                if (contxt.HttpContext.Session.GetString("Admin") == "Yes")
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
                        return Redirect("/Admin/EventList");
                    }
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


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _eventService.UpdateEvent(MyEvent);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is updated", MyEvent.EventName);
                return Redirect("/Admin/EventList");
            }
            return Page();
        }



    }
}
