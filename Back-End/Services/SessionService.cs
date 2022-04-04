using PopCorn.Web.Domain;
using PopCorn.Web.Infra.Context;
using PopCorn.Web.Infra.Repositories.Interfaces;
using PopCorn.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Services
{
    public class SessionService : ISessionService
    {
        private readonly ISessionRepository _repository;
        private readonly IMovieRepository _movieRepository;

        public SessionService(ISessionRepository repository, IMovieRepository movieRepository)
        {
            _repository = repository;
            _movieRepository = movieRepository;
        }
        public void Delete(int id, bool forceDelete)
        {
            _repository.Delete(id, forceDelete);
            
        }

        public Session GetSessionById(int id)
        {
            var session = _repository.GetSessionById(id);
            return session;
        }

        public List<Session> GetSessions()
        {
            var session = _repository.GetSessions();
            return session;
        }

        public void Save(Session session)
        {
            var movie = _movieRepository.GetMovieById(session.MovieId);
            session.CaculatorEndTime(movie.Duration);
            session.SessionDate = session.SessionDate.Date;
            _repository.Save(session);           
        }

        public void Update(Session session)
        {
            _repository.Update(session);
        }
    }
}
