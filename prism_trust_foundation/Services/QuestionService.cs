using prism_trust_foundation.Models;

namespace prism_trust_foundation.Services
{
    public class QuestionService
    {
        private readonly AuthDbContext _context;
        public QuestionService(AuthDbContext context)
        {
            _context = context;
        }

        public List<Question> GetAll()
        {
            return _context.Question.OrderBy(d => d.QueryId).ToList();
        }
        public Question? GetQueryByEmail(string Email)
        {
            Question? QueryEmail = _context.Question.FirstOrDefault(i => i.Email.Equals(Email));
            return QueryEmail;
        }
        public Question? GetQueryById(int Id)
        {
            Question? QueryEmail = _context.Question.FirstOrDefault(i => i.QueryId.Equals(Id));
            return QueryEmail;
        }
        public void AddQuery(Question myQuery)
        {
            _context.Question.Add(myQuery);
            _context.SaveChanges();
        }
        public void DeleteQuery(Question myQuery)
        {
            _context.Question.Remove(myQuery);
            _context.SaveChanges();
        }
    }
}
