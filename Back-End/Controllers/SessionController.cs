using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PopCorn.Web.Domain;
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
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }

        [HttpDelete("{Id}/{forceDelete}")]
        [Authorize]
        public IActionResult Delete(int Id, bool forceDelete)
        {
            try
            {
                _sessionService.Delete(Id, forceDelete);
                return Ok(new { message = "Sessão deletada com sucessso!", type = "sucess" });
            
            }
            catch (Exception)
            {
                return Ok(new { message = "Sessão não pode ser removida, falta menos de 10 dias para a estreia!", type = "error" });
            }
        }

        [HttpGet("{Id}")]
        [Authorize]
        public IActionResult GetSessionById(int Id)
        {
            try
            {
                
                return Ok(_sessionService.GetSessionById(Id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetSessions()
        {
            try
            {

                return Ok(_sessionService.GetSessions());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save([FromBody] Session session)
        {
            try
            {
                _sessionService.Save(session);
                return Ok(new { message = "Sessão cadastrada com sucesso!", type = "sucess" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
