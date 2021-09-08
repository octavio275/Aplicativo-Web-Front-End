using System;

using Microsoft.AspNetCore.Mvc;
using PS.template.aplicacion.services;
using Template.Domain.DTOs;


namespace TP2_Logica_de_negocios.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _Service;


        public ClienteController(IClienteService service)
        {
            _Service = service;

        }
        [HttpPost]
        public IActionResult CreateCliente(ClienteDTOs cliente)
        {

            try
            {
                return new JsonResult(_Service.CreateCliente(cliente)) { StatusCode = 201 };
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet("*")]
        public IActionResult GetAll()
        {
            try
            {
                return new JsonResult(_Service.GetAllListaCliente()) { StatusCode = 200 };

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }

        }
        [HttpGet]
        public ActionResult GetClientes([FromQuery] string dni, [FromQuery] string nombre, [FromQuery] string apellido)
        {
            try
            {
                return new JsonResult(_Service.GetClientes(dni, nombre, apellido)) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("ValidarUsuario")]
        public IActionResult ValidarUsuario([FromQuery] string contraseña)
        {
            try
            {
                return new JsonResult(_Service.GetValidarUsuario(contraseña)) { StatusCode = 200 };

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);

            }
        }
    }
}
