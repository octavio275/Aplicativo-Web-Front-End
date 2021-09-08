using System.Collections.Generic;
using Template.Domain.DTOs;
using Template.Domain.Entities;

namespace Template.Domain.Commands
{
    public interface IAlquileresRepository : IRepository<Alquileres>
    {
        List<LibrosPorClienteDTOs> GetLibros(int idCliente);
        List<object> ObtenerPorEstado(int estado);
    }
}
