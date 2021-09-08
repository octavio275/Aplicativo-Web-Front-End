using Biblioteca.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService service;
        public LibroController(ILibroService service)
        {
            this.service = service;
        }

        [HttpGet]
        public ActionResult GetLibros(bool stock, string autor, string titulo)
        {
            try
            {
                return new JsonResult(service.BuscarLibros(stock, autor, titulo)) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
