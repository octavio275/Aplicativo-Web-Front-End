using Biblioteca.Domain.Commands;
using Biblioteca.Domain.Entities;
using System.Collections.Generic;

namespace Biblioteca.Domain.Queries
{
    public interface IClienteRepository : IGenericsRepository
    {
        public List<Cliente> GetClientes(string dni, string nombre, string apellido);
    }
}
