using System;
using Microsoft.AspNetCore.Mvc;
using Template.Aplication.Services;

namespace TP2_Logica_de_negocios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private readonly ILibroService _Service;


        public LibroController(ILibroService service)
        {
            _Service = service;

        }
   
        [HttpGet("ListaLibros")]
        public IActionResult GetALL()
        {
            try
            {
                return new JsonResult(_Service.GetALL()) { StatusCode = 200 };

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }
        [HttpGet]

        public IActionResult Libros([FromQuery] bool stock, [FromQuery] string autor, [FromQuery] string titulo)
        {
            try
            {
                return new JsonResult(this._Service.BuscarLibro(stock, autor, titulo)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
       

  

    }
}
