using PopCorn.Web.Domain;
using PopCorn.Web.Infra.Context;
using PopCorn.Web.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Infra.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly PopCornContext _context;

        public MovieRepository(PopCornContext context)
        {
            _context = context;
        }
        public bool Delete(int id)
        {
            var movie = _context.Movies.First(m => m.Id == id);
            
             _context.Movies.Remove(movie);
             _context.SaveChanges();
             return true;
        }

        public bool ExistTitle(string title)
        {
            return _context.Movies.Where(c => c.Title.ToLower() == title.ToLower()).FirstOrDefault() != null;
        }

        public Movie GetMovieById(int id)
        {
            var movie = _context.Movies.First(c => c.Id == id);
            return movie;
        }

        public List<Movie> GetMovies()
        {
            var movies = _context.Movies.ToList();
            return movies;
        }

        public bool MovieLinkedSession(Movie movie)
        {
            if(_context.Sessions.Count() > 0)
            {
                var sessions = _context.Sessions.ToList();
                var sessionLinked = sessions.FirstOrDefault(m => m.MovieId == movie.Id);
                if (sessionLinked != null)
                    return true;

                return false;
            }

            return false;
        }

        public bool Save(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
            return true;
        }

        public void Update(Movie movie)
        {
            _context.Movies.Update(movie);
            _context.SaveChanges();
        }
    }
}
