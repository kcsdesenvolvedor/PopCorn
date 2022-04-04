using PopCorn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Infra.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public List<User> GetUsers();
        public User GetByUsername(string username, string password);
        public void Save(User user);
        public void Update(User user);
        public void Delete(int id);
    }
}
