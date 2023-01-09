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
            if (UserExists(newuser.NRIC))
            {
                return false;
            }
            _context.Add(newuser);
            _context.SaveChanges();
            return true;
        }
        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.NRIC == id);
        }

    }
}
