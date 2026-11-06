using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FecebookClass;
using FecebookClass.entities;

namespace FecebookApp.services
{
    public class UserService
    {
        private readonly FacebookDbContext _context;
        public UserService (FacebookDbContext context)
        {
            context=_context;
        }
        public User Register(string username, string email, string password, string bio, DateTime dateofbirth)
        {
            User user = new User { name = username, Email = email, Password = password };
            _context.Users.Add(user);
            _context.SaveChanges();
            return user;
        }
        public User Login(
            string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
            if (user == null) 
            { throw new Exception("invalid email or password"); }
                return user;
        }

    }
}
