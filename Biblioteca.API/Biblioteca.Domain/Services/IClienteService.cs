using Biblioteca.Domain.DTOs.ClientesDTO;
using Biblioteca.Domain.DTOs.ClientesDTO.Response;
using Biblioteca.Domain.Entities;
using System.Collections.Generic;

namespace Biblioteca.Domain.Services
{
    public interface IClienteService
    {
        ClienteResponseDTO CreateCliente(ClienteDTO clienteDTO);
        List<Cliente> GetAll();
        List<ClienteDTO> GetClientes(string dni, string nombre, string apellido);
    }
}
