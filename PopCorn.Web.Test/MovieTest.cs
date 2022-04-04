using Microsoft.EntityFrameworkCore;
using Moq;
using PopCorn.Web.Domain;
using PopCorn.Web.Infra.Context;
using PopCorn.Web.Infra.Repositories;
using PopCorn.Web.Infra.Repositories.Interfaces;
using PopCorn.Web.Services;
using PopCorn.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PopCorn.Web.Test
{
    [Collection("PopCorn.Test")]
    public class MovieTest: TestWithSqlite
    {
        private IMovieService _movieService;
        private ISessionService _sessionService;

        public MovieTest()
        {
            InjectionDependecy();
        }
        [Fact(DisplayName ="Deve verificar se titulo de filme não repete")]
        public void DeveVerificarSeTituloDeFilmeNaoRepete()
        {
            var movie = new Movie
            {
                Id = 1,
                Title = "teste 4",
                Description = "Uma das animações mais dramáticas da história da Walt Disney",
                Duration = "02:30",
                Image = "dwgw4242t4t24t2gwsdgg24g24g24g2gwdg24g24g"
            };
            _movieService.Save(movie);
            Assert.Throws<ArgumentException>(() => _movieService.Save(movie));
            DbContext.Database.EnsureDeleted();

        }
        [Fact(DisplayName = "Deve Verificar Se Filme Esta Vinculado Com Alguma Sessao")]
        public void DeveVerificarSeFilmeEstaVinculadoComAlgumaSessao()
        {           
            var movie = new Movie
            {
                Title = "teste 6",
                Description = "Uma das animações mais dramáticas da história da Walt Disney",
                Duration = "02:30",
                Image = "dwgw4242t4t24t2gwsdgg24g24g24g2gwdg24g24g"
            };
            _movieService.Save(movie);
            var session = new Session
            {
                SessionDate = DateTime.Now,
                StartTime = "11:00",
                EndTime = "13:30",
                TicketValue = 20.00,
                TypeAnimation = Domain.Enums.ETypeAnimation.D3,
                TypeAudio = Domain.Enums.ETypeAudio.dubbed,
                MovieId = movie.Id,
                RoomId = 2,
            };
                       
            _sessionService.Save(session);
            Assert.Throws<ArgumentException>(() => _movieService.Delete(movie.Id));
            DbContext.Database.EnsureDeleted();
        }

        public void InjectionDependecy()
        {
            var movieRepository = new MovieRepository(DbContext);
            _movieService = new MovieService(movieRepository, _sessionService);

            var sessionRepository = new SessionRepository(DbContext);
            _sessionService =  new SessionService(sessionRepository, movieRepository);


        }
    }
}
