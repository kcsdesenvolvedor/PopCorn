using PopCorn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Services.Interfaces
{
    public interface ISessionService
    {
        public void Save(Session session);
        public void Delete(int id, bool forceDelete);
        public void Update(Session session);
        public Session GetSessionById(int id);
        public List<Session> GetSessions();
    }
}
