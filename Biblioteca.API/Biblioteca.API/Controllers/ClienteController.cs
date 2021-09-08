using Biblioteca.Domain.DTOs.ClientesDTO;
using Biblioteca.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Biblioteca.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService service;
        public ClienteController(IClienteService service)
        {
            this.service = service;
        }

        [HttpPost]
        public IActionResult Add(ClienteDTO clientedto)
        {
            try
            {
                return new JsonResult(service.CreateCliente(clientedto)) { StatusCode = 201 };
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetClientes(string dni, string nombre, string apellido)
        {
            try
            {
                return new JsonResult(service.GetClientes(dni,nombre,apellido)) { StatusCode = 200 };

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
