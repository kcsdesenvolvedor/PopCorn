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
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _repository;
        private readonly ISessionService _sessionService;

        public MovieService(IMovieRepository repository, ISessionService sessionService)
        {
            _repository = repository;
            _sessionService = sessionService;
        }
        public void Delete(int id)
        {
            var movie = _repository.GetMovieById(id);
            if(MovieLinkedSession(movie))
                throw new ArgumentException("Filme está vinculado a uma sessão");

            _repository.Delete(id);
            
        }

        public bool ExistTitle(string title)
        {
            return _repository.ExistTitle(title);
        }

        public Movie GetMovieById(int id)
        {
            var movie = _repository.GetMovieById(id);
            return movie;
        }

        public List<Movie> GetMovies()
        {
            var movies = _repository.GetMovies();
            return movies;
        }

        public bool MovieLinkedSession(Movie movie)
        {
            return _repository.MovieLinkedSession(movie);
        }

        public void Save(Movie movie)
        {

                if (ExistTitle(movie.Title))
                    throw new ArgumentException("Já existe um filme cadastrado com esse título.");
                _repository.Save(movie);
                            
        }

        public void Update(Movie movie)
        {
            UpdateSession(movie);
            _repository.Update(movie);
            
        }

        public void UpdateSession(Movie movie)
        {
            var sessions = _sessionService.GetSessions().Where(s => s.MovieId == movie.Id);
            
            foreach (var session in sessions)
            {
                session.CaculatorEndTime(movie.Duration);
                var sessionOfDay = _sessionService.GetSessions().Where(s => s.SessionDate == session.SessionDate && s.Id != session.Id && s.RoomId == session.RoomId);
                foreach (var sessionDay in sessionOfDay)
                {
                    if(int.Parse(session.StartTime.Split(":")[0]) <= int.Parse(sessionDay.StartTime.Split(":")[0]) && int.Parse(session.EndTime.Split(":")[0]) >= int.Parse(sessionDay.StartTime.Split(":")[0]))
                    {
                        throw new ArgumentException("Alterar a duração desse filme afetarar o início da sessão Id: " + sessionDay.Id);
                    }
                }
                
                _sessionService.Update(session);
            }

        }
    }
}
