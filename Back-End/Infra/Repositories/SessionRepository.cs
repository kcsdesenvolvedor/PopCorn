using Microsoft.EntityFrameworkCore;
using PopCorn.Web.Domain;
using PopCorn.Web.Infra.Context;
using PopCorn.Web.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Infra.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly PopCornContext _context;

        public SessionRepository(PopCornContext context)
        {
            _context = context;
          
        }
        public void Delete(int Id, bool forceDelete)
        {
            var session = GetSessionById(Id);

            //verifica se falta 10 dias ou mais para a data da sessão, para poder excluir ou não a sessão
            if ((session.SessionDate.Day - DateTime.Now.Day) >= 10 || DateTime.Now.Day > session.SessionDate.Day || forceDelete == true)
            {
                _context.Remove(session);
                _context.SaveChanges();
                return;
            }
            throw new ArgumentException();
        }

        public Session GetSessionById(int id)
        {
            var session = _context.Sessions.First(c => c.Id == id);
            return session;
        }

        public List<Session> GetSessions()
        {
            var session = _context.Sessions.ToList();
            return session;
        }

        public void Save(Session session)
        {
            _context.Sessions.Add(session);
            _context.SaveChanges();          
        }

        public void Update(Session session)
        {
            _context.Sessions.Update(session);
            _context.SaveChanges();
        }
    }
}
