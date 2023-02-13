using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using prism_trust_foundation.ViewModels;

namespace prism_trust_foundation.Pages.User
{
    public class UpdateAvatarModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signinManager;

        private readonly IHttpContextAccessor contxt;

        private UserService _svc;
        
        private IWebHostEnvironment _environment;

        public UpdateAvatarModel(SignInManager<ApplicationUser> signInManager, UserService service, IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
        {
            _environment = environment;
            this.signinManager = signInManager;
            _svc = service;
            contxt = httpContextAccessor;
        }

        [BindProperty]
        public IFormFile? Upload { get; set; }

        [BindProperty]
        public UpdateAvatar UaModel { get; set; } = new();

        public ApplicationUser AvatarUser { get; set; } = new();

        [BindProperty]
        public string? MyMessage { get; set; }

        public IActionResult OnGet()
        {
            string Email = contxt.HttpContext.Session.GetString("Email");
            ApplicationUser? user = _svc.GetUserByEmail(Email);
            if (user != null)
            {
                AvatarUser = user;
                UaModel.ImageURL = user.ImageURL;
                return Page();
            }
            else
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("You have not login yet.");
                return RedirectToPage("/Index");
            }
        }

        public async Task<IActionResult> OnPostAsync(int sessionCount)
        {
            if (ModelState.IsValid)
            {
                if (sessionCount == 2)
                {
                    if (Upload != null)
                    {
                        if (Upload.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("Upload", "File size cannot exceed 2MB.");
                            return Page();
                        }
                        string Email = contxt.HttpContext.Session.GetString("Email");
                        ApplicationUser? user = _svc.GetUserByEmail(Email);
                        var uploadsFolder = "uploads";
                        var imageFile = Guid.NewGuid() + Path.GetExtension(Upload.FileName);
                        var imagePath = Path.Combine(_environment.ContentRootPath, "wwwroot", uploadsFolder, imageFile);
                        using var fileStream = new FileStream(imagePath, FileMode.Create);
                        await Upload.CopyToAsync(fileStream);
                        user.ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);
                        _svc.UpdateUser(user);
                        TempData["FlashMessage.Type"] = "success";
                        TempData["FlashMessage.Text"] = string.Format("User {0}'s avatar is been uploaded", user.Email);
                        return RedirectToPage("UserDetails");
                    }
                    else
                    {
                        return Page();
                    }
                }
                else if (sessionCount == 1)
                {
                    return RedirectToPage("UserDetails");
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
