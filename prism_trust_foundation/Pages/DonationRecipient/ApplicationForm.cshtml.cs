using Microsoft.AspNetCore.Identity;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using Microsoft.AspNetCore.DataProtection;
using System.Web;
using prism_trust_foundation.Services;
using System.Net.Mail;
using System.ComponentModel.DataAnnotations;

namespace prism_trust_foundation.Pages
{
    public class ApplicationFormModel : PageModel
    {
        private IWebHostEnvironment _environment;
        private readonly AuthDbContext _context;
        private readonly UserService _userService;
        private readonly IHttpContextAccessor contxt;
        [BindProperty]
        public donationRecipient donationRecipients { get; set; }


        /*[BindProperty]
        public IFormFile? Upload { get; set; }*/

        public ApplicationUser? _user { get; set; }

        public ApplicationFormModel(AuthDbContext authDbContext, IWebHostEnvironment environment, UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _context = authDbContext;
            _environment = environment;
            _userService = userService;
            contxt = httpContextAccessor;
        }
        public void OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            _user = _userService.GetUserByEmail(Email);
        }


        public async Task<IActionResult> OnPostAsync()
        {





            if (ModelState.IsValid)
            {
                /* if (Upload != null)
                 {*/


                /*var uploadsFolder = "uploads";
                var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                using var fileStream = new FileStream(imagePath, FileMode.Create);
                await Upload.CopyToAsync(fileStream);
                var ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);*/


                string Email = contxt.HttpContext.Session.GetString("Email");
                _user = _userService.GetUserByEmail(Email);

                donationRecipient dRecipient = new donationRecipient()
                {
                    Fname = _user.Fname,
                    email = _user.Email,
                    NRIC = _user.NRIC,
                    address = donationRecipients.address,
                    BirthDate = _user.BirthDate,
                    nationality = donationRecipients.nationality,
                    monthlyIncome = donationRecipients.monthlyIncome,
                    /*documentProof = donationRecipients.documentProof,*/

                };



                _context.donationRecipients.Add(dRecipient);
                _context.SaveChanges();

            }
            /* }
             else
             {
                 Console.Write(Upload);
             }*/
            return RedirectToPage("/Index");


        }

    }
}