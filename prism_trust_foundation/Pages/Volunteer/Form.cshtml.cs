using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;

namespace prism_trust_foundation.Pages.Volunteer
{
    public class FormModel : PageModel
    {
        [BindProperty]
        public VolunteerShift MyVolunteerShift { get; set; } = new();
        public List<VolunteerShift> Timeslots { get; set; } = new()
        {
            new VolunteerShift {Shift_Start = "Event1", Shift_End = "Example Event", Shift_Quantity = 1},
            new VolunteerShift {Shift_Start = "Event1", Shift_End = "Example Event", Shift_Quantity = 2},
            new VolunteerShift {Shift_Start = "Event1", Shift_End = "Example Event", Shift_Quantity = 3},
            new VolunteerShift {Shift_Start = "Event1", Shift_End = "Example Event", Shift_Quantity = 4},
        };
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("You have signed up to volunteer at" + MyVolunteerShift.Shift_Start);
                return Redirect("/Volunteer/List");
            }
            return Page();
        }
    }
}
