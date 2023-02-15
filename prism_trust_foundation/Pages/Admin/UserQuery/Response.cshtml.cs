using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin.UserQuery
{
    public class ResponseModel : PageModel
    {
        [BindProperty]
        public string response { get; set; }
        [BindProperty]
        public Question question { get; set; }

        private readonly QuestionService _questionService;
        private readonly IHttpContextAccessor contxt;
        private EmailSender _emailSender;
        private readonly AuthDbContext _authDbContext;

        public ResponseModel(QuestionService questionService, IHttpContextAccessor contxt, EmailSender emailSender, AuthDbContext authDbContext)
        {
            _questionService = questionService;
            this.contxt = contxt;
            _emailSender = emailSender;
            _authDbContext = authDbContext; 
        }
        public IActionResult OnGet(int id)
        {
            Question? myQuestion = _questionService.GetQueryById(id);
            if (myQuestion == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Query does not exist");
                return Redirect("/Admin/UserQuery/Index");
            }
            else
            {
                question = myQuestion;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                Question? myQuestion = _questionService.GetQueryById(question.QueryId);
                var confirmation = response;

                await _emailSender.Execute("Response", confirmation!, myQuestion.Email);
                _questionService.DeleteQuery(myQuestion);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Successfully replied to query");
                return Redirect("/Admin/UserQuery/Index");
            }
            return Page();
        }
    }
}
