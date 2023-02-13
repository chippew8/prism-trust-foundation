using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity;
using prism_trust_foundation.ViewModels;

namespace prism_trust_foundation.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public IFormFile? Upload { get; set; }

        private readonly UserService _registerService;

        private IWebHostEnvironment _environment;

        private UserManager<ApplicationUser> userManager { get; }

        private SignInManager<ApplicationUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment environment, UserService registerService)
        {
            _registerService = registerService;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _environment = environment;
        }

        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
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
                    var ImageURL = string.Format("/{0}/{1}", uploadsFolder, imageFile);


                    ApplicationUser? employee = _registerService.GetUserByNRIC(RModel.NRIC);
                    ApplicationUser? user = _registerService.GetUserByEmail(RModel.Email);
                    if (employee != null)
                    {
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Type"] = string.Format(
                            "NRIC is already in use");
                        return Page();
                    }
                    else if (user != null)
                    {
                        TempData["FlashMessage.Type"] = "danger";
                        TempData["FlashMessage.Type"] = string.Format(
                            "Email is already in use");
                        return Page();
                    }

                    ApplicationUser newregister = new ApplicationUser
                    {
                        UserName = RModel.Email,
                        NRIC = RModel.NRIC,
                        Email = RModel.Email,
                        Fname = RModel.Fname,
                        PhoneNum = RModel.PhoneNum,
                        Ban_Status = false,
                        Admin_Role = false,
                        BirthDate = RModel.BirthDate,
                        ImageURL = ImageURL,
                        Gender = RModel.Gender
                    };

                    var result = await userManager.CreateAsync(newregister, RModel.Password);

                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(newregister, true);
                        return RedirectToPage("../Pages/Index");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Type"] = string.Format(result.ToString());
                    return Page();
                }
            }
            return Page();
        }
    }
}
