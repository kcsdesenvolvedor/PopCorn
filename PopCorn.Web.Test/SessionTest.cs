using PopCorn.Web.Domain;
using PopCorn.Web.Infra.Repositories;
using PopCorn.Web.Services;
using PopCorn.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace PopCorn.Web.Test
{
    [Collection("PopCorn.Test")]
    public class SessionTest: TestWithSqlite
    {
        private IMovieService _movieService;
        private ISessionService _sessionService;
        private IRoomService _roomService;
        public SessionTest()
        {
            
            InjectionDependecy();
        }
        
        [Fact(DisplayName = "Deve Calcular Horario Final Automaticamente")]
        public void DeveCalcularHorarioFinalAutomaticamente()
        {
            var movie = new Movie
            {
                Title = "A volta dos que não foram",
                Description = "O melhor filme do mundo",
                Duration = "02:00",
                Image = "asldkf90a9ija9wijdef9aij9w"
            };

            _movieService.Save(movie);
            var session = new Session
            {
                SessionDate = DateTime.Now,
                StartTime = "11:00",
                TicketValue = 20.00,
                TypeAnimation = Domain.Enums.ETypeAnimation.D3,
                TypeAudio = Domain.Enums.ETypeAudio.dubbed,
                MovieId = movie.Id,
                RoomId = 2,
            };
            session.CaculatorEndTime(movie.Duration);
            _sessionService.Save(session);

            Assert.Equal("13:00", session.EndTime);
            DbContext.Database.EnsureDeleted();
        }
        [Fact(DisplayName = "Deve Retornar Salas Que Nao Estao Vinculadas Com Nenhuma Sessao")]
        public void DeveRetornarSalasQueNaoEstaoVinculadasComNenhumaSessao()
        {
       
            var movie = new Movie
            {
                Title = "A volta dos que não foram",
                Description = "O melhor filme do mundo",
                Duration = "02:00",
                Image = "asldkf90a9ija9wijdef9aij9w"
            };
            _movieService.Save(movie);

            var session = new Session
            {
                SessionDate = DateTime.Now.Date,
                StartTime = "11:00",
                EndTime = "13:00",
                TicketValue = 20.00,
                TypeAnimation = Domain.Enums.ETypeAnimation.D3,
                TypeAudio = Domain.Enums.ETypeAudio.dubbed,
                MovieId = 1,
                RoomId = 1,
            };
            _sessionService.Save(session);

            var session2 = new Session
            {
                SessionDate = DateTime.Now,
                StartTime = "15:00",
                EndTime = "17:00",
                TicketValue = 20.00,
                TypeAnimation = Domain.Enums.ETypeAnimation.D3,
                TypeAudio = Domain.Enums.ETypeAudio.dubbed,
                MovieId = 1,
                RoomId = 2,
            };
            _sessionService.Save(session2);

            var rooms = _roomService.GetRooms(session.SessionDate.Date, "12:00", 1);
            Assert.All(rooms, item => Assert.DoesNotContain(item.Name, "Sala 01"));
            DbContext.Database.EnsureDeleted();
        }

        [Fact(DisplayName = "Deve Verificar Se Sessao Pode Ser Removida")]
        public void DeveVerificarSeSessaoPodeSerRemovida()
        {
            var movie = new Movie
            {
                Title = "A volta dos que não foram",
                Description = "O melhor filme do mundo",
                Duration = "02:00",
                Image = "asldkf90a9ija9wijdef9aij9w"
            };
            _movieService.Save(movie);

            var session = new Session
            {
                SessionDate = DateTime.Now.Date,
                StartTime = "11:00",
                EndTime = "13:00",
                TicketValue = 20.00,
                TypeAnimation = Domain.Enums.ETypeAnimation.D3,
                TypeAudio = Domain.Enums.ETypeAudio.dubbed,
                MovieId = 1,
                RoomId = 1,
            };
            _sessionService.Save(session);

            var session2 = new Session
            {
                SessionDate = (DateTime.Now.Date).AddDays(11),
                StartTime = "16:00",
                EndTime = "18:00",
                TicketValue = 20.00,
                TypeAnimation = Domain.Enums.ETypeAnimation.D3,
                TypeAudio = Domain.Enums.ETypeAudio.dubbed,
                MovieId = 1,
                RoomId = 1,
            };
            _sessionService.Save(session2);

            
            Assert.Throws<ArgumentException>(() => _sessionService.Delete(session.Id, false));
        }

        public void InjectionDependecy()
        {
            var movieRepository = new MovieRepository(DbContext);
            _movieService = new MovieService(movieRepository, _sessionService);

            var sessionRepository = new SessionRepository(DbContext);
            _sessionService = new SessionService(sessionRepository, movieRepository);

            var roomRepository = new RoomRepository(DbContext);
            _roomService = new RoomService(roomRepository);

            for (int i = 1; i <= 10; i++)
            {
                DbContext.Rooms.Add(
                        new Room
                        {
                            Id = i,
                            Name = i < 10 ? $"Sala 0{i}" : $"Sala {i}",
                            NumberOfSeats = 200
                        }
                    );
            }

        }
    }
}
