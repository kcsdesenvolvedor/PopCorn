using PopCorn.Web.Domain;
using PopCorn.Web.Infra.Context;
using PopCorn.Web.Infra.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Infra.Repositories
{
    public class RoomRepository : IRoomRepository
    {
        private readonly PopCornContext _context;

        public RoomRepository(PopCornContext context)
        {
            _context = context;
        }
        public Room GetRoomByName(string name)
        {
            var room = _context.Rooms.First(c => c.Name == name);
            return room;
        }

        public List<Room> GetRooms(DateTime dateSession, string startTime, int movieId)
        {
            List<Room> rooms = new List<Room>();
            var dateSessionStartHoursMinutes = dateSession.AddHours(double.Parse(startTime.Split(':')[0])).AddMinutes(double.Parse(startTime.Split(':')[1]));
            var movies = _context.Movies.FirstOrDefault(c=> c.Id == movieId);
            var dateSessionEndlHoursMinutes = dateSessionStartHoursMinutes.AddHours(double.Parse(movies.Duration.Split(':')[0])).AddMinutes(double.Parse(movies.Duration.Split(':')[1]));
            List<Session> sessions = new List<Session>();
            _context.Sessions.Where(c => c.SessionDate.Date == dateSession.Date
                                                     ).ToList().ForEach(x => {

                                                         if (x.SalaOcupada(dateSessionStartHoursMinutes, dateSessionEndlHoursMinutes))
                                                             sessions.Add(x);
                                                     });

            //recebe todas as salas
            var roomsAlls = _context.Rooms.ToList();

            foreach (var session in sessions)
            {
                //adiciona todas as salas que já estão com sessão marcada para aquela mesma data e horário
                rooms.Add(_context.Rooms.FirstOrDefault(c=> c.Id == session.RoomId));

            }
            //retorna todas as salas, excerto as salas com sessão marcada para aquela mesma data e horário
            return roomsAlls.Except(rooms).ToList();
        }

        public List<Room> GetRoomsAlls()
        {
            var rooms = _context.Rooms.ToList();
            return rooms;
        }
    }
}
