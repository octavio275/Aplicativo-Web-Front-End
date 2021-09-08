using Biblioteca.Domain.DTOs.LibrosDTO;
using System.Collections.Generic;

namespace Biblioteca.Domain.Services
{
    public interface ILibroService
    {
        List<LibroDTO> BuscarLibros(bool stock, string autor, string titulo);
    }
}
