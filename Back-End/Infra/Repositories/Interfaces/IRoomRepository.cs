using PopCorn.Web.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Infra.Repositories.Interfaces
{
    public interface IRoomRepository
    {
        public List<Room> GetRoomsAlls();
        public Room GetRoomByName(string name);
        public List<Room> GetRooms(DateTime dateSession, string startTime, int movieId);
    }
}
