using PopCorn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Infra.Repositories.Interfaces
{
    public interface IMovieRepository
    {
        public bool MovieLinkedSession(Movie movie);
        public List<Movie> GetMovies();
        public bool ExistTitle(string title);
        public Movie GetMovieById(int id);
        public bool Save(Movie movie);
        public void Update(Movie movie);
        public bool Delete(int id);
    }
}
