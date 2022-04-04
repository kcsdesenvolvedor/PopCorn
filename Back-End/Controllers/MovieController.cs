using Microsoft.AspNetCore.Authentication.JwtBearer;
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
    [Authorize]
    [Route("v1/[controller]")]
    public class MovieController : Controller
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetMovies()
        {
            try
            {

                return Ok(_movieService.GetMovies());
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public IActionResult GetMovieById(int id)
        {
            try
            {
                return Ok(_movieService.GetMovieById(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult Save([FromBody] Movie movie)
        {
            try
            {
                _movieService.Save(movie);
                return Ok(new { message = "Filme cadastrado com sucesso!", type = "sucess" });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message, type = "error" });
            }
        }

        [HttpPut]
        [Authorize]
        public IActionResult Update([FromBody] Movie movie)
        {
            try
            {
                _movieService.Update(movie);
                return Ok(new { message = "Alteração realizada com sucesso!", type = "sucess" });
            }
            catch (ArgumentException ex)
            {

                return Ok(new { message = ex.Message, type = "error"});
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                _movieService.Delete(id);
                return Ok(new { message = "Filme deletado com sucesso!", type = "sucess" });


            }
            catch (Exception ex)
            {

                return Ok(new { message = ex.Message, type = "error" }); 
            }
        }
    }
}
