using PopCorn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Services.Interfaces
{
    public interface IUserService
    {
        public User Authenticate(string username, string password);
        public List<User> GetUsers();
        public User GetUserByName(string username, string password);
        public void Save(User user);
        public void Update(User user);
        public void Delete(int id);
    }
}
