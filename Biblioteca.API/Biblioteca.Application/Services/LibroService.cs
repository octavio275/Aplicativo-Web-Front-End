using AutoMapper;
using Biblioteca.Application.Services.Base;
using Biblioteca.Domain.DTOs.LibrosDTO;
using Biblioteca.Domain.Queries;
using Biblioteca.Domain.Services;
using System.Collections.Generic;

namespace Biblioteca.Application.Services
{
    public class LibroService : GenericsService, ILibroService
    {
        protected ILibroRepository Repository;

        public LibroService(ILibroRepository repository, IMapper mapper) : base(repository,mapper)
        {
            Repository = repository;
        }

        public List<LibroDTO> BuscarLibros(bool stock, string autor, string titulo)
        {
            var listaLibros = Repository.ObtenerLibros(stock, autor, titulo);

            return Mapper.Map<List<LibroDTO>>(listaLibros);
        }

    }
}
