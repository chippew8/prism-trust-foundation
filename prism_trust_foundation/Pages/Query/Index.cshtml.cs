using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Query
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Question MyQuestion { get; set; } = new();
        private readonly SignInManager<ApplicationUser> signinManager;
        private readonly IHttpContextAccessor contxt;
        private QuestionService _svc;
        public IndexModel(SignInManager<ApplicationUser> signinManager, IHttpContextAccessor contxt, QuestionService svc)
        {
            this.signinManager = signinManager;
            this.contxt = contxt;
            _svc = svc;
        }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                /*if (MyQuestion.UserQuestion.IsNullOrEmpty())
                {
                    TempData["FlashMessage.Type"] = "danger";
                    TempData["FlashMessage.Text"] = string.Format("Query cannnot be empty.");
                    ModelState.AddModelError("", "Query cannot be empty.");
                    return Redirect("/Query/Index");
                }
                else
                { 
                }*/
                MyQuestion.Email = contxt.HttpContext.Session.GetString("Email");
                _svc.AddQuery(MyQuestion);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Successfully submitted query");
                ModelState.Clear();
            }
            return Redirect("/Index");
        }
    }
}
