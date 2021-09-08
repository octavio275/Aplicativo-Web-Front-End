using System.Collections.Generic;
using Template.Domain.DTOs;
using Template.Domain.Entities;

namespace Template.Domain.Queries
{
   public interface IClienteQuery
    {
        List<ClienteDTOs> GetAllListaCliente();
        LoginDTOs ValidarUsuario(string contraseña);
        List<Cliente> GetClientes(string dni, string nombre, string apellido);
    }
    
}
