using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Events
{

    public class AddModel : PageModel
    {
        private readonly EventService _eventService;
        private IWebHostEnvironment _environment;
        public AddModel(EventService eventService, IWebHostEnvironment environment)
        {
            _eventService = eventService;
            _environment = environment;
            
        }
        [BindProperty]
        public Event MyEvent { get; set; } = new();
        [BindProperty]
        public IFormFile? Upload { get; set; }
        public void OnGet()
        {
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
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Event {0} is added", MyEvent.EventName);
                return Redirect("/Admin/Events/EventList");
            }
            return Page();
        }
    }
}
