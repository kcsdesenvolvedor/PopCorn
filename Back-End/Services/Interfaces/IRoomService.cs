using PopCorn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Services.Interfaces
{
    public interface IRoomService
    {
        public Room GetRoomByName(string name);
        public List<Room> GetRooms(DateTime dateSession, string startTime, int movieId);
        public List<Room> GetRoomsAlls();
    }
}
