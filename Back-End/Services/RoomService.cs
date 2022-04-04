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
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _repository;

        public RoomService(IRoomRepository repository)
        {
            _repository = repository;
        }
        public Room GetRoomByName(string name)
        {
            var room = _repository.GetRoomByName(name);
            return room;
        }

        public List<Room> GetRooms(DateTime dateSession, string startTime, int movieId)
        {
            var rooms = _repository.GetRooms(dateSession, startTime, movieId);
            return rooms;
        }

        public List<Room> GetRoomsAlls()
        {
            var rooms = _repository.GetRoomsAlls();
            return rooms;
        }
    }
}
