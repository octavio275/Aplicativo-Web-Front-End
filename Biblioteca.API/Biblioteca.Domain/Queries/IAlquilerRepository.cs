using Biblioteca.Domain.Commands;
using Biblioteca.Domain.DTOs.LibrosDTO;
using System.Collections.Generic;

namespace Biblioteca.Domain.Queries
{
    public interface IAlquilerRepository : IGenericsRepository
    {
        public List<object> ObtenerPorEstado(int estado);

        public List<LibroDeClienteDTO> GetLibrosPorCliente(int idCliente);
    }
}
