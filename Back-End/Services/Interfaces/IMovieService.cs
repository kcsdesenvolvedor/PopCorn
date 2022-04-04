using PopCorn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Services.Interfaces
{
    public interface IMovieService
    {
        public bool MovieLinkedSession(Movie movie);
        public void UpdateSession(Movie movie);
        public bool ExistTitle(string title);
        public Movie GetMovieById(int id);
        public List<Movie> GetMovies();
        public void Save(Movie movie);
        public void Update(Movie movie);
        public void Delete(int id);
    }
}
