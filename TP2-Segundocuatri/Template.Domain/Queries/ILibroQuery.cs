using System.Collections.Generic;
using Template.Domain.DTOs;
using Template.Domain.Entities;

namespace Template.Domain.Queries
{
    public interface ILibroQuery
    {
    
        List<LibrosDTOs> GetALL();
        List<Libros> ObtenerLibros(bool stock, string autor, string titulo);
    }

}
