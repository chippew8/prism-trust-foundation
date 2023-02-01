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
            
        private MyDbContext _context;
        public UserService(MyDbContext context)
        {
            _context = context;
        }


        public bool AddUser(User newuser)
        {
            if (UserExists(newuser.Email))
            {
                return false;
            }
            _context.Add(newuser);
            _context.SaveChanges();
            return true;
        }
        public bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Email == id);
        }

        public User? GetUserById(string id)
        {
            User? user = _context.Users.FirstOrDefault(
            x => x.Email.Equals(id));
            return user;
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public List<User> GetAll()
        {
            return _context.Users.OrderBy(m => m.Email).ToList();
        }
    }
}
