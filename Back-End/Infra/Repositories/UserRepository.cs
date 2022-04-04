using PopCorn.Web.Domain;
using PopCorn.Web.Infra.Context;
using PopCorn.Web.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PopCornContext _context;

        public UserRepository(PopCornContext popCornContext)
        {
            _context = popCornContext;
        }

        public void Delete(int id)
        {
            var user = GetUsers().Where(u => u.Id == id).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public User GetByUsername(string username, string password)
        {
            return _context.Users.FirstOrDefault(c=> c.UserName == username && c.Password == password);
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public void Save(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }
}
