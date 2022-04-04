using Microsoft.AspNetCore.Mvc;
using PopCorn.Web.Domain;
using PopCorn.Web.Services.Interfaces;
using System.Threading.Tasks;

namespace PopCorn.Web.Controllers
{
    [Route("v1/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult GetUsers()
        {
            try
            {
                return Ok(_userService.GetUsers());
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Save([FromBody] User user)
        {
            try
            {
                _userService.Save(user);
                return Ok(new { message = "Usuário cadastrado com sucesso!", type = "success"});
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Update([FromBody] User user)
        {
            try
            {
                _userService.Update(user);
                return Ok(new { message = "Usuário atualizado com sucesso!", type = "success" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _userService.Delete(id);
                return Ok(new { message = "Usuário removido com sucesso!", type = "success" });
            }
            catch (System.Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
