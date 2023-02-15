using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using prism_trust_foundation.Models;
using prism_trust_foundation.Services;

namespace prism_trust_foundation.Pages.Admin.UserQuery
{
    public class IndexModel : PageModel
    {
        private readonly QuestionService _questionService;
        public IndexModel(QuestionService questionService)
        {
            _questionService = questionService;
        }
        public List<Question> QuestionList { get; set; } = new();
        [BindProperty]
        public Question MyQuestion { get; set; } = new();
        public void OnGet()
        {
            QuestionList = _questionService.GetAll();
        }

        public IActionResult OnPost()
        {
            Question? question = _questionService.GetQueryById(MyQuestion.QueryId);
            if (question == null)
            {
                TempData["FlashMessage.Type"] = "danger";
                TempData["FlashMessage.Text"] = string.Format("Query does not exist");
                return Redirect("/Admin/UserQuery/Index");
            }
            else
            {
                _questionService.DeleteQuery(question);
                TempData["FlashMessage.Type"] = "success";
                TempData["FlashMessage.Text"] = string.Format("Query removed successfully");
            }
            return Redirect("/Admin/UserQuery/Index");
        }
    }
}
