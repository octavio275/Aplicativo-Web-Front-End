using System;
using Microsoft.AspNetCore.Mvc;
using Template.Aplication.Services;
using Template.Domain.Commands;
using Template.Domain.DTOs;

namespace TP2_Logica_de_negocios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilerController : ControllerBase
{

        private readonly IAlquilerService _Service;

        public AlquilerController(IAlquilerService service)
        {
            _Service = service;

        }
        [HttpPost]
        public IActionResult Post(AlquileresDTOs alquilerDTO)
        {

            try
            {
                return new JsonResult(this._Service.RegistrarAlquileres(alquilerDTO)){ StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]

        public IActionResult Actualizar(ActualizarReservaDTO actualizarReservaDTO)
        {
            try
            {
                return new JsonResult(this._Service.ActualizarReserva(actualizarReservaDTO)) { StatusCode = 200 }; 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpGet("clientes/{id}")]
        public ActionResult GetLibros(int id)
        {
            try
            {
                return new JsonResult(_Service.GetLibros(id)) { StatusCode = 200 };

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        public IActionResult GetByEstado(int estado)
        {

            try
            {
                return new JsonResult(_Service.GetByEstado(estado)) { StatusCode = 200 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
