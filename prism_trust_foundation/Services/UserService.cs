using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prism_trust_foundation.Models;
using Microsoft.EntityFrameworkCore;

namespace prism_trust_foundation.Services
{
    public class UserService
    {
        private readonly AuthDbContext _context;

        public UserService(AuthDbContext context)
        {
            _context = context;
        }
        public List<ApplicationUser> GetAll()
        {
            return _context.AspNetUser.OrderBy(m => m.Id).ToList();
        }
        public ApplicationUser? GetUserByNRIC(string Nric)
        {
            ApplicationUser? applicationUser = _context.AspNetUser.FirstOrDefault(
                x => x.NRIC.Equals(Nric));
            return applicationUser;
        }

        public ApplicationUser? GetUserByEmail(string email)
        {
            ApplicationUser? applicationUser = _context.AspNetUser.Include(u => u.Coupon).FirstOrDefault(
                x => x.Email.Equals(email));
            return applicationUser;
        }

        public void AddUser(ApplicationUser applicationUser)
        {
            _context.AspNetUser.Add(applicationUser);
            _context.SaveChanges();
        }

        public void UpdateUser(ApplicationUser applicationUser)
        {
            _context.AspNetUser.Update(applicationUser);
            _context.SaveChanges();
        }
    }
}