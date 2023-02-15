using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Events
{

    public class AddModel : PageModel
    {
        private readonly TimeslotService _timeslotService;
        private readonly EventService _eventService;
        private IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor contxt;
        public AddModel(EventService eventService, IWebHostEnvironment environment, TimeslotService timeslotService, IHttpContextAccessor context)
        {
            _eventService = eventService;
            _environment = environment;
            _timeslotService = timeslotService;
            contxt = context;
        }
        [BindProperty]
        public Event MyEvent { get; set; } = new();
        [BindProperty]
        public IFormFile? Upload { get; set; }
        public IActionResult OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");

            if (Email != null)
            {
                if (contxt.HttpContext.Session.GetString("Admin") == "Yes")
                {
                    
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Event? events = _eventService.GetEventByName(MyEvent.EventName);
                if (events != null)
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Event already exists");
                    return Page();
                }
                if (Upload != null)
                {
                    if(Upload.Length >2 * 1024 * 1024)
                    {
                        ModelState.AddModelError("Upload", "File size cannot exceed 2MB");
                        return Page();
                    }
                    var uploadsFolder = "uploads";
                    var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                    var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await Upload.CopyToAsync(fileStream);
                    MyEvent.ImageURL = String.Format("/{0}/{1}", uploadsFolder, imageFile);
                }

                _eventService.AddEvent(MyEvent);
                Timeslot timeslot1 = new Timeslot(){Shift_Start = 1200,Shift_End = 1300,Shift_Type = "Donate",Shift_Quantity= 0,EventId= MyEvent.EventId};
                _timeslotService.AddTimeslot(timeslot1);
                Timeslot timeslot2 = new Timeslot() { Shift_Start = 1300, Shift_End = 1400, Shift_Type = "Donate", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot2);
                Timeslot timeslot3 = new Timeslot() { Shift_Start = 1400, Shift_End = 1500, Shift_Type = "Donate", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot3);
                Timeslot timeslot4 = new Timeslot() { Shift_Start = 1500, Shift_End = 1600, Shift_Type = "Donate", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot4);

                Timeslot timeslot5 = new Timeslot() { Shift_Start = 1200, Shift_End = 1300, Shift_Type = "Recycle", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot5);
                Timeslot timeslot6 = new Timeslot() { Shift_Start = 1300, Shift_End = 1400, Shift_Type = "Recycle", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot6);
                Timeslot timeslot7 = new Timeslot() { Shift_Start = 1400, Shift_End = 1500, Shift_Type = "Recycle", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot7);
                Timeslot timeslot8 = new Timeslot() { Shift_Start = 1500, Shift_End = 1600, Shift_Type = "Recycle", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot8);

                Timeslot timeslot9 = new Timeslot() { Shift_Start = 1200, Shift_End = 1300, Shift_Type = "Volunteer", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot9);
                Timeslot timeslot10 = new Timeslot() { Shift_Start = 1300, Shift_End = 1400, Shift_Type = "Volunteer", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot10);
                Timeslot timeslot11 = new Timeslot() { Shift_Start = 1400, Shift_End = 1500, Shift_Type = "Volunteer", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot11);
                Timeslot timeslot12 = new Timeslot() { Shift_Start = 1500, Shift_End = 1600, Shift_Type = "Volunteer", Shift_Quantity = 0, EventId = MyEvent.EventId };
                _timeslotService.AddTimeslot(timeslot12);


                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is added", MyEvent.EventName);
                return Redirect("/Admin/Events/EventList");
            }
            return Page();
        }
    }
}
