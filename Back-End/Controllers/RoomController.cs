using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PopCorn.Web.Services;
using PopCorn.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Controllers
{
    [Authorize]
    [Route("v1/[controller]")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet("{name}")]
        [Authorize]
        public IActionResult GetRoomByName(string name)
        {
            try
            {

                return Ok(_roomService.GetRoomByName(name));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetRooms(DateTime dateSession, string startTime, int movieId)
        {
            try
            {

                return Ok(_roomService.GetRooms(dateSession, startTime, movieId));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("/v1/room/alls")]
        [Authorize]
        public IActionResult GetRoomsAlls()
        {
            try
            {
                return Ok(_roomService.GetRoomsAlls());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
