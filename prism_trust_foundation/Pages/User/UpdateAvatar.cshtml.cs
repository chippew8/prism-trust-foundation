using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.User
{
    public class UpdateAvatarModel : PageModel
    {
        private readonly ILogger<UpdateAvatarModel> _logger;
        private UserService _svc;
        private IWebHostEnvironment _environment;

        public UpdateAvatarModel(ILogger<UpdateAvatarModel> logger, UserService service, IWebHostEnvironment environment)
        {
            _environment = environment;
            _logger = logger;
            _svc = service;
        }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        [BindProperty]
        public Models.User AvatarUser { get; set; } = new();

        [BindProperty]
        public string? MyMessage { get; set; }

        public IActionResult OnGet(string CurrentID)
        {
            Models.User? user = _svc.GetUserById(CurrentID);
            if (user != null)
            {
                AvatarUser = user;
                return Page();
            }
            else
            {
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int sessionCount)
        {
            if (ModelState.IsValid)
            {
                if (sessionCount == 1)
                {
                    if (Upload != null)
                    {
                        if (Upload.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                            return Page();
                        }
                        var uploadsFolder = "uploads";
                        var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                        var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                        using var fileStream = new FileStream(imagePath, FileMode.Create);
                        await Upload.CopyToAsync(fileStream);
                        AvatarUser.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                        _svc.UpdateUser(AvatarUser);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0}'s avatar is been uploaded", AvatarUser.Email);
                        return RedirectToPage("UserDetails", new { CurrentID = AvatarUser.Email });
                    }
                    else
                    {
                        return Page();
                    }
                }
                else if (sessionCount == 2)
                {
                    return RedirectToPage("UserDetails", new { CurrentID = AvatarUser.Email });
                }
                else
                {
                    return Page();
                }
            }
            else
            {
                return Page();
            }
        }
    }
}
