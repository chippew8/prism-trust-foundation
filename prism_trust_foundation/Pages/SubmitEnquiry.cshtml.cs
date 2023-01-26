using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace prism_trust_foundation.Pages
{
    public class SubmitEnquiryModel : PageModel
    {
       
            [BindProperty]
            public string Email { get; set; }
            [BindProperty]
            public string EnquiryDescription { get; set; }

            [BindProperty]
            public string EnquiryType { get; set; } = string.Empty;
            


            public List<string> TypeList { get; set; } = new() { };



            public void OnGet()
            {
            }

        }
    }

