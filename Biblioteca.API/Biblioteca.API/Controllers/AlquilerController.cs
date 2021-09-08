using Biblioteca.Domain.DTOs;
using Biblioteca.Domain.DTOs.AlquileresDTO;
using Biblioteca.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilerController : ControllerBase
    {
        private readonly IAlquilerService service;
        public AlquilerController(IAlquilerService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult AddAlquiler(AlquileryReservaRequestDTO alquilerRequestDto)
        {
            try
            {
                return new JsonResult(service.AddAlquiler(alquilerRequestDto)) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet]
        public ActionResult GetPorEstado(int estado)
        {
            try
            {
                return new JsonResult(service.GetPorEstado(estado)) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch]
        public IActionResult Edit(RequestAlquilarReserva paraAlquilarDTO)
        {
            try
            {
                service.AlquilarReserva(paraAlquilarDTO);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("cliente/{id}")]
        public ActionResult GetLibrosPorCliente(int id)
        {
            try
            {
                return new JsonResult(service.GetLibrosPorCliente(id)) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
