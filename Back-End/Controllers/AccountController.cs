using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using PopCorn.Web.Domain;
using PopCorn.Web.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PopCorn.Web.Controllers
{
    //[EnableCors("CorsApi")]
    [AllowAnonymous]
    [Route("v1/account")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost("login")]
        //[AllowAnonymous]
        public IActionResult Authenticate([FromBody] User model)
        {
            try
            {                
                var user = _userService.Authenticate(model.UserName, model.Password);

                if (user == null)
                    return BadRequest(new { message = "Usuário ou senha inválidos", type = "info" });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
