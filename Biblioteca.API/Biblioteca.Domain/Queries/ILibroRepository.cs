using Biblioteca.Domain.Commands;
using Biblioteca.Domain.Entities;
using System.Collections.Generic;

namespace Biblioteca.Domain.Queries
{
    public interface ILibroRepository : IGenericsRepository
    {
        public List<Libro> ObtenerLibros(bool stock, string autor, string titulo);
    }
}
